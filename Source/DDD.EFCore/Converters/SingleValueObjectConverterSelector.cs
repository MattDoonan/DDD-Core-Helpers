using DDD.Core.Converters.SingleValueObjects;
using DDD.Core.Interfaces.Factories;
using DDD.Core.Interfaces.Values;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Converters;

internal class SingleValueObjectConverterSelector : ValueConverterSelector
{
    public SingleValueObjectConverterSelector(ValueConverterSelectorDependencies dependencies) : base(dependencies)
    {
    }
    
    public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type? providerClrType = null)
    {
        var converters = base.Select(modelClrType, providerClrType);
        foreach (var converter in converters)
        { 
            yield return converter;
        }

        var valueObjectConverterResult = CheckForSingleValueObject(modelClrType);
        if (valueObjectConverterResult.IsFailure)
        {
            yield break;
        }
        
        foreach (var converter in valueObjectConverterResult.Output)
        {
            yield return converter;
        }
    }

    private static InfraResult<IEnumerable<ValueConverterInfo>> CheckForSingleValueObject(Type modelClrType)
    {
        var isSingleValueResult = CheckIfSingleValue(modelClrType);
        if (isSingleValueResult.IsFailure)
        {
            return isSingleValueResult;
        }
        var infos = new List<ValueConverterInfo>();
        var convertableResult = CheckForConvertableValueObject(modelClrType);
        if (convertableResult.IsSuccessful)
        {
            infos.Add(convertableResult.Output);
        }
        var factoryResult = CheckForSimpleFactory(modelClrType);
        if (factoryResult.IsSuccessful)
        {
            infos.Add(factoryResult.Output);
        }
        return infos;
    }
    
    private static InfraResult<ValueConverterInfo> CheckForConvertableValueObject(Type modelClrType)
    {
        var convertableOpenType = typeof(IConvertable<,>);
        var convertableInterface = modelClrType.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == convertableOpenType);
        if (convertableInterface is null)
        {
            return InfraResult.Fail("the model is not IConvertable");
        }
        
        var valueType = convertableInterface.GetGenericArguments()[0];
        
        var converterType = typeof(ConvertableValueObjectConverter<,>)
            .MakeGenericType(modelClrType, valueType);
        
        return CreateConverterInfo(modelClrType, valueType, converterType);
    }
    
    private static InfraResult<ValueConverterInfo> CheckForSimpleFactory(Type modelClrType)
    {
        var factoryOpenType = typeof(ISingleValueObjectFactory<,>);
        var factoryInterface = modelClrType.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == factoryOpenType);
        if (factoryInterface is null)
        {
            return InfraResult.Fail("the model is not ISimpleValueObjectFactory");
        }
        
        var valueType = factoryInterface.GetGenericArguments()[0];
        
        var converterType = typeof(SingleValueObjectFactoryConverter<,>)
            .MakeGenericType(modelClrType, valueType);
        
        return CreateConverterInfo(modelClrType, valueType, converterType);
    }
    
    private static InfraResult<ValueConverterInfo> CreateConverterInfo(Type modelClrType, Type valueType, Type converterType)
    {
        return new ValueConverterInfo(
            modelClrType,
            valueType,
            Factory
        );
        ValueConverter Factory(ValueConverterInfo info) => (ValueConverter)Activator.CreateInstance(converterType, info.MappingHints)!;
    }
    
    private static InfraResult CheckIfSingleValue(Type modelClrType)
    {
        var singleValueOpenType = typeof(ISingleValue<>);
        var singleValueInterface = modelClrType.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == singleValueOpenType);
        return singleValueInterface is null 
            ? InfraResult.Fail("the model is not ISingleValue") 
            : InfraResult.Pass();
    }
}