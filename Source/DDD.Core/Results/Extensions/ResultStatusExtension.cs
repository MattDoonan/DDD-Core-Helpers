using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Extensions;

public static class ResultStatusExtension
{
    extension<TResult>(IEnumerable<TResult> results) 
        where TResult : IResultStatus
    {
        public bool AllSuccessful()
        {
            return results.All(r => r.IsSuccessful);
        }

        public IEnumerable<ResultError> GetErrors()
        {
            return results.SelectMany(r => r.Errors);
        }

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
        public TResult AggregateTo<TResult>(FailureType primaryFailureType = FailureType.Generic)
            where TResult: IResultStatus, IResultFactory<TResult>
        {
            var isAllSuccessful = results.AllSuccessful();
            return isAllSuccessful
                ? TResult.Pass()
                : CreateFailureResult<TResult>(results, primaryFailureType);
        }

        private TResult CreateFailureResult<TResult>(FailureType primaryFailureType = FailureType.Generic)
            where TResult: IResultStatus, IResultFactory<TResult>
        {
            var result = TResult.Fail($"Not all {nameof(TResult)} were successful");
            result.SetPrimaryFailure(primaryFailureType);
            result.AddErrors(results.GetErrors());
            return result;
        }
    }
}