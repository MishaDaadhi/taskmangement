using System.Diagnostics.CodeAnalysis;

namespace askManager.Core.Common
{

    /// <summary>
    ///     The only possible value for this type is null.
    /// </summary>
    /// <remarks>
    ///     Indicates a Null type, that is one that has no real type. This is generally used in
    ///     generic types for a service where it makes no sense to have a type. For instance, a
    ///     service that takes no input might have a generic input type of Null. Null cannot be
    ///     instantiated, and so the only value allowed for a Null type is null.
    /// </remarks>
    /// ReSharper disable once ConvertToStaticClass ReSharper disable once ClassNeverInstantiated.Global
    [ExcludeFromCodeCoverage]
    public sealed class Null
    {
        /// <summary>
        ///     No instantiation for you
        /// </summary>
        private Null()
        {
        }
    }
}