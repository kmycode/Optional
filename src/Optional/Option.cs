using System;

namespace Optional
{
    /// <summary>
    /// Provides a set of functions for creating optional values.
    /// </summary>
    public static class Option
    {
        /// <summary>
        /// Wraps an existing value in an Option&lt;T&gt; instance.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>An optional containing the specified value.</returns>
        public static Option<T> Some<T>(T value) => new Option<T>(value, true);

        /// <summary>
        /// Wraps an existing value in an Option&lt;T, TException&gt; instance.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>An optional containing the specified value.</returns>
        public static Option<T, TException> Some<T, TException>(T value) =>
            new Option<T, TException>(value, default(TException), true);

        /// <summary>
        /// Wraps an existing value in an Optional&lt;T, TException&gt; instance.
        /// If an exception is thrown, returns none with exception object.
        /// </summary>
        /// <param name="value">The value getter to be wrapped.</param>
        /// <returns>An optional containing the specified value.</returns>
        public static Option<T, TException> SomeOrException<T, TException>(Func<T> value)
            where TException : Exception
        {
            try
            {
                return value().Some<T, TException>();
            }
            catch (TException e)
            {
                return None<T, TException>(e);
            }
        }

        /// <summary>
        /// Wraps an existing value in an Optional&lt;T, TException&gt; instance.
        /// If an exception is thrown, returns none with exception object.
        /// </summary>
        /// <param name="value">The value getter to be wrapped.</param>
        /// <returns>An optional containing the specified value.</returns>
        public static Option<T, Exception> SomeOrException<T>(Func<T> value) =>
            SomeOrException<T, Exception>(value);

        /// <summary>
        /// Wraps an existing value in an Optional&lt;T, TException&gt; instance.
        /// If an exception is thrown, returns none with exception object.
        /// If value is null, returns none and has no exception.
        /// </summary>
        /// <param name="value">The value getter to be wrapped.</param>
        /// <returns>An optional containing the specified value.</returns>
        public static Option<T, Option<TException>> SomeNotNullOrException<T, TException>(Func<T> value)
            where TException : Exception
        {
            try
            {
                return value().SomeNotNull(None<TException>());
            }
            catch (TException e)
            {
                return None<T, Option<TException>>(e.Some());
            }
        }

        /// <summary>
        /// Wraps an existing value in an Optional&lt;T, TException&gt; instance.
        /// If an exception is thrown, returns none with exception object.
        /// If value is null, returns none and has no exception.
        /// </summary>
        /// <param name="value">The value getter to be wrapped.</param>
        /// <returns>An optional containing the specified value.</returns>
        public static Option<T, Option<Exception>> SomeNotNullOrException<T>(Func<T> value) =>
            SomeNotNullOrException<T, Exception>(value);

        /// <summary>
        /// Creates an empty Option&lt;T&gt; instance.
        /// </summary>
        /// <returns>An empty optional.</returns>
        public static Option<T> None<T>() => new Option<T>(default(T), false);

        /// <summary>
        /// Creates an empty Option&lt;T, TException&gt; instance, 
        /// with a specified exceptional value.
        /// </summary>
        /// <param name="exception">The exceptional value.</param>
        /// <returns>An empty optional.</returns>
        public static Option<T, TException> None<T, TException>(TException exception) =>
            new Option<T, TException>(default(T), exception, false);
    }
}
