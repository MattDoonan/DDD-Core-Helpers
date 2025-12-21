using DDD.Core.Converters.SingleValueObjects;
using DDD.Core.Extensions;
using DDD.Core.Results;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Converters;

/// <summary>
/// A custom value converter selector that handles single value objects.
/// </summary>
public class SingleValueObjectConverterSelector : ValueConverterSelector
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
        return modelClrType.IsSingleValueObject()
            ? InfraResult.Pass(CheckForFactories(modelClrType))
            : InfraResult.Fail<IEnumerable<ValueConverterInfo>>("the model is not single value object");
    }

    private static IEnumerable<ValueConverterInfo> CheckForFactories(Type modelClrType)
    {
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
        return infos.AsEnumerable();
    }
    
    private static InfraResult<ValueConverterInfo> CheckForConvertableValueObject(Type modelClrType)
    {
        var valueTypeResult = modelClrType.GetConvertibleInputType();
        if (valueTypeResult.IsFailure)
        {
            return valueTypeResult.ToTypedInfraResult<ValueConverterInfo>();
        }
        var valueType = valueTypeResult.Output;
        var converterType = typeof(ConvertableValueObjectConverter<,>)
            .MakeGenericType(modelClrType, valueType);
        return CreateConverterInfo(modelClrType, valueType, converterType);
    }
    
    private static InfraResult<ValueConverterInfo> CheckForSimpleFactory(Type modelClrType)
    {
        var valueTypeResult = modelClrType.GetSimpleFactoryInputType();
        if (valueTypeResult.IsFailure)
        {
            return valueTypeResult.ToTypedInfraResult<ValueConverterInfo>();
        }
        var valueType = valueTypeResult.Output;
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
}