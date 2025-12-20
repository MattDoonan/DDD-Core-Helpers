using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Extensions;

public static class ResultErrorExtension
{
    extension(IEnumerable<ResultError> errors)
    {
        /// <summary>
        /// Converts the collection of ResultError instances to their corresponding error messages.
        /// </summary>
        /// <returns>
        /// An enumerable of error message strings.
        /// </returns>
        public IEnumerable<string> ToErrorMessages()
        {
            return errors.Select(e => e.ToErrorMessage());
        }

        /// <summary>
        /// Checks if any errors exist at the specified result layer.
        /// </summary>
        /// <param name="layer">
        /// The result layer to check for errors.
        /// </param>
        /// <returns>
        /// True if any errors exist at the specified layer, false otherwise.
        /// </returns>
        public bool Contains(ResultLayer layer)
        {
            return errors.GetErrorsBy(layer).Any();
        }
        
        /// <summary>
        /// Checks if any errors exist of the specified status type.
        /// </summary>
        /// <param name="type">
        /// The <see cref="StatusType"/> to check for.
        /// </param>
        /// <returns>
        /// True if any errors exist of the specified type, false otherwise.
        /// </returns>
        public bool Contains(StatusType type)
        {
            return errors.GetErrorsBy(type).Any();
        }
        
        /// <summary>
        /// Checks if any errors exist of the specified failed operation status.
        /// </summary>
        /// <param name="operationStatus">
        /// The <see cref="FailedOperationStatus"/> to check for.
        /// </param>
        /// <returns>
        /// True if any errors exist of the specified operation status, false otherwise.
        /// </returns>
        public bool Contains(FailedOperationStatus operationStatus)
        {
            return errors.GetErrorsBy(operationStatus).Any();
        }

        /// <summary>
        /// Returns a new collection of ResultError instances with the specified layer.
        /// </summary>
        /// <param name="layer">
        /// The <see cref="ResultLayer"/> to set on each error.
        /// </param>
        /// <returns>
        /// An enumerable of <see cref="ResultError"/>s with the specified layer.
        /// </returns>
        public IEnumerable<ResultError> WithLayer(ResultLayer layer)
        {
            return errors.Select(e => e.WithLayer(layer));
        }
        
        
        /// <summary>
        /// Retrieves all errors that match the specified <see cref="FailedOperationStatus"/>
        /// </summary>
        /// <param name="operationStatus">
        /// The <see cref="FailedOperationStatus"/> to filter errors by.
        /// </param>
        /// <returns>
        /// An enumerable of <see cref="ResultError"/>s that match the specified <see cref="FailedOperationStatus"/> type.
        /// </returns>
        public IEnumerable<ResultError> GetErrorsBy(FailedOperationStatus operationStatus) 
            => GetErrorsBy(errors, e => e.IsFailureType(operationStatus));
        
        
        /// <summary>
        /// Retrieves all errors that match the specified <see cref="StatusType"/>
        /// </summary>
        /// <param name="statusType">
        /// The <see cref="StatusType"/> to filter errors by.
        /// </param>
        /// <returns>
        /// An enumerable of <see cref="ResultError"/>s that match the specified <see cref="StatusType"/>.
        /// </returns>
        public IEnumerable<ResultError> GetErrorsBy(StatusType statusType) 
            => GetErrorsBy(errors, e => e.IsFailureType(statusType));

        /// <summary>
        /// Retrieves all errors that occurred at the specified result layer.
        /// </summary>
        /// <param name="layer">
        /// The result layer to filter errors by.
        /// </param>
        /// <returns>
        /// An enumerable of <see cref="ResultError"/>s that occurred at the specified layer.
        /// </returns>
        public IEnumerable<ResultError> GetErrorsBy(ResultLayer layer) 
            => GetErrorsBy(errors, e => e.IsLayer(layer));

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