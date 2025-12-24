using DDD.Core.Interfaces.Factories;
using DDD.Core.Interfaces.Values;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;

namespace DDD.Core.Extensions;

internal static class TypeExtensions
{
    extension(Type type)
    {
        public bool IsSingleValueObject()
        {
            var singleValueOpenType = typeof(ISingleValue<>);
            var singleValueInterface = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == singleValueOpenType);
            return singleValueInterface is not null;
        }
        
        public InfraResult<Type> GetConvertibleInputType()
        {
            var convertableResult = GetConvertableInterface(type);
            if (convertableResult.IsFailure)
            {
                return convertableResult;
            }
            var convertableInterface = convertableResult.Output;
            return convertableInterface.GetInputArgument();
        }
        
        public InfraResult<Type> GetSimpleFactoryInputType()
        {
            var factoryResult = GetSimpleFactoryInterface(type);
            if (factoryResult.IsFailure)
            {
                return factoryResult;
            }
            var factoryInterface = factoryResult.Output;
            return factoryInterface.GetInputArgument();
        }
        
        private InfraResult<Type> GetSimpleFactoryInterface()
        {
            var factoryOpenType = typeof(ISingleValueObjectFactory<,>);
            var factoryInterface = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == factoryOpenType);
            return factoryInterface is null
                ? InfraResult.NotFound<Type>("the type does not implement ISingleValueObjectFactory<,>")
                : InfraResult.Pass(factoryInterface);
            
        }
        
        private InfraResult<Type> GetConvertableInterface()
        {
            var convertableOpenType = typeof(IConvertibleFactory<,>);
            var convertableInterface = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == convertableOpenType);
            return convertableInterface is null
                ? InfraResult.NotFound<Type>("the type does not implement IConvertibleFactory<,>")
                : InfraResult.Pass(convertableInterface);
        }

        private InfraResult<Type> GetInputArgument()
        {
            try
            {
                return InfraResult.Pass(type.GetGenericArguments()[0]);
            }
            catch (Exception e)
            {
                return InfraResult.NotFound<Type>($"failed to get generic arguments: {e.Message}");
            }
        }
    }
}