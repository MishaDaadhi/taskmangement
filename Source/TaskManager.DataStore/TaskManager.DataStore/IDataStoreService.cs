using TaskManager.Core.Services;

namespace TaskManager.DataStore
{
    /// <summary>
    ///     Marker interface for Data store services.
    /// </summary>
    /// <typeparam name="TInput">Service input.</typeparam>
    /// <typeparam name="TOutput">Service output.</typeparam>
    public interface IDataStoreService<in TInput, TOutput> : IAsyncGenericService<TInput, TOutput>
    {
        // No op
    }
}
