using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Core.Services
{
    /// <summary>
    ///     Generic asynchronous service interface. All async services in the service tier MUST
    ///     implement this interface or a child interface.
    /// </summary>
    /// <remarks>
    ///     Specific types of services CAN further specialize this interface over particular types
    ///     of input or output types. They SHOULD NOT add methods to this interface.
    /// </remarks>
    /// <typeparam name="TInput">type of the service input</typeparam>
    /// <typeparam name="TOutput">type of the service output</typeparam>
    public interface IAsyncGenericService<in TInput, TOutput>
    {
        /// <summary>
        ///     This method executes the service.
        /// </summary>
        /// <param name="input">service input</param>
        /// <param name="cancellationToken">
        ///     Token to be used in case of cancelling the asynchronous task
        /// </param>
        /// <returns>An awaitable task with result of type <typeparamref name="TOutput"/></returns>
        Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken);
    }
}
