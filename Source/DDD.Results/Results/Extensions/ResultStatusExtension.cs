using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Extensions;

public static class ResultStatusExtension
{
    extension<TResult>(IEnumerable<TResult> results) 
        where TResult : IResultStatus
    {
        /// <summary>
        /// Checks if all results in the collection are successful.
        /// </summary>
        /// <returns>
        /// True if all results are successful; otherwise, false.
        /// </returns>
        public bool AllSuccessful()
        {
            return results.All(r => r.IsSuccessful);
        }

        /// <summary>
        /// Retrieves all errors from the collection of results.
        /// </summary>
        /// <returns>
        /// An enumerable of <see cref="ResultError"/> containing all errors from the results.
        /// </returns>
        public IEnumerable<ResultError> GetErrors()
        {
            return results.SelectMany(r => r.Errors);
        }

        /// <summary>
        /// Retrieves all error messages from the collection of results.
        /// </summary>
        /// <returns>
        /// An enumerable of strings containing all error messages from the results.
        /// </returns>
        public IEnumerable<string> GetErrorMessages()
        {
            return results.SelectMany(r => r.ErrorMessages);
        }
    }

    public static bool AnyFailure<TResult>(this IEnumerable<TResult> results)
        where TResult : IResultFailure
    {
        return results.Any(r => r.IsFailure);
    }
    
    extension(IResultStatus[] results)
    {
        /// <summary>
        /// Aggregates multiple <see cref="IResultStatus"/> into a single TResult.
        /// If all results are successful, returns a successful TResult.
        /// If any result is a failure, returns a failed TResult with the specified primary failure type.
        /// </summary>
        /// <param name="primaryFailureType">
        /// The primary failure type to set if any result is a failure.
        /// </param>
        /// <typeparam name="TResult">
        /// The type of the aggregated result, must implement <see cref="IResultStatus"/> and <see cref="IResultFactory{TResult}"/>.
        /// </typeparam>
        /// <returns>
        /// An aggregated <typeparamref name="TResult"/> representing the overall success or failure.
        /// </returns>
        public TResult AggregateTo<TResult>(FailedOperationStatus? primaryFailureType = null)
            where TResult: IResultStatus, IResultFactory<TResult>
        {
            var isAllSuccessful = results.AllSuccessful();
            return isAllSuccessful
                ? TResult.Pass()
                : CreateFailureResult<TResult>(results, primaryFailureType);
        }

        private TResult CreateFailureResult<TResult>(FailedOperationStatus? primaryFailureType)
            where TResult: IResultStatus, IResultFactory<TResult>
        {
            
            var result = TResult.Fail($"Not all {nameof(TResult)} were successful");
            result.SetPrimaryStatus(primaryFailureType ?? OperationStatus.Failure());
            result.AddErrors(results.GetErrors());
            return result;
        }
    }
}