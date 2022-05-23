using System;
using System.Reflection;

namespace TaskManager.Core.Common
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts a string value to a enumeration of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="value">Value to be converted</param>
        /// <returns>The enum value</returns>
        /// <exception cref="InvalidCastException">
        ///     Exception raised when the <typeparamref name="T"/> is not a type of enum.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Exception raised when the value is not defined in the possible enum values.
        /// </exception>
        public static T ToEnum<T>(this string value)
        {
            if (!typeof(T).GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException("The type of generic argument must be an enum.");
            }

            try
            {
                var result = (T)Enum.Parse(typeof(T), value, true);
                return result;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    $"'{value}' is not a valid value for type {typeof(T).FullName}");
            }
        }
    }
}
