using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Extensions;

public static class ResultErrorExtension
{
    extension(IEnumerable<ResultError> errors)
    {
        public IEnumerable<string> ToErrorMessages()
        {
            return errors.Select(e => e.ToErrorMessage());
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
    
    
    extension(IEnumerable<ResultError> errors)
    {
        /// <summary>
        /// Retrieves all errors that match the specified failure type.
        /// </summary>
        /// <param name="failureType">
        /// The failure type to filter errors by.
        /// </param>
        /// <returns>
        /// An enumerable of <see cref="ResultError"/>s that match the specified failure type.
        /// </returns>
        public IEnumerable<ResultError> GetErrorsBy(FailureType failureType) => GetErrorsBy(errors, e => e.IsFailureType(failureType));

        /// <summary>
        /// Retrieves all errors that occurred at the specified result layer.
        /// </summary>
        /// <param name="layer">
        /// The result layer to filter errors by.
        /// </param>
        /// <returns>
        /// An enumerable of <see cref="ResultError"/>s that occurred at the specified layer.
        /// </returns>
        public IEnumerable<ResultError> GetErrorsBy(ResultLayer layer) => GetErrorsBy(errors, e => e.IsLayer(layer));

        /// <summary>
        /// Retrieves all errors of the specified type.
        /// </summary>
        /// <typeparam name="TError">
        /// The type inside <see cref="ResultError"/> to filter by.
        /// </typeparam>
        /// <returns>
        /// An enumerable of <see cref="ResultError"/>s with the specified type.
        /// </returns>
        public IEnumerable<ResultError> GetErrorsOfType<TError>() => GetErrorsBy(errors, e => e.IsOfType<TError>());

        /// <summary>
        /// Retrieves all errors that match the provided predicate.
        /// </summary>
        /// <param name="predicate">
        /// The predicate function to filter errors.
        /// </param>
        /// <returns>
        /// An enumerable of <see cref="ResultError"/>s that match the predicate.
        /// </returns>
        public IEnumerable<ResultError> GetErrorsBy(Func<ResultError, bool> predicate) => errors.Where(predicate);
    }
}