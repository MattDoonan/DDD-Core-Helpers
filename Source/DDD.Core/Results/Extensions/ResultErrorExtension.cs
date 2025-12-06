using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Extensions;

public static class ResultErrorExtension
{
    extension(IEnumerable<ResultError> errors)
    {
        public IEnumerable<string> ToErrorMessages()
        {
            return errors.Select(e => e.ToString());
        }

        public bool Contains(ResultLayer layer)
        {
            return errors.Select(e => e.ResultLayer).Contains(layer);
        }
        
        public bool Contains(FailureType type)
        {
            return errors.Select(e => e.FailureType).Contains(type);
        }

        public IEnumerable<ResultError> AddLayer(ResultLayer layer)
        {
            return errors.Select(e => e.AddLayer(layer));
        }
    }
}