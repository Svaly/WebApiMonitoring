using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Framework.Patterns.Validation
{
    [DebuggerStepThrough]
    public static class Guard
    {
        public static void NotNull<T>(Expression<Func<T>> reference, T value)
        {
            if (value == null) throw new GuardException(GetParameterName(reference), "Parameter cannot be null.");
        }

        public static void NotNull<T>(this T value, Expression<Func<T>> reference)
            where T : class
        {
            if (value == null) throw new GuardException(GetParameterName(reference), "Parameter cannot be null.");
        }

        public static void NotNullOrEmpty(Expression<Func<string>> reference, string value)
        {
            NotNull(reference, value);
            if (value == string.Empty) throw new GuardException(GetParameterName(reference), "Parameter cannot be empty.");
        }

        public static void NotNullOrEmpty(this string value, Expression<Func<string>> reference)
        {
            NotNull(reference, value);
            if (value == string.Empty) throw new GuardException(GetParameterName(reference), "Parameter cannot be empty.");
        }

        public static void NotNullOrEmpty(Expression<Func<Guid?>> reference, Guid? value)
        {
            NotNull(reference, value);
            if (value == Guid.Empty) throw new GuardException(GetParameterName(reference), "Parameter cannot be empty.");
        }

        public static void NotNullOrEmpty(this Guid? value, Expression<Func<Guid?>> reference)
        {
            NotNull(reference, value);
            if (value == Guid.Empty) throw new GuardException(GetParameterName(reference), "Parameter cannot be empty.");
        }

        public static void NotEmpty(Expression<Func<Guid>> reference, Guid value)
        {
            if (value == Guid.Empty) throw new GuardException(GetParameterName(reference), "Parameter cannot be empty.");
        }

        public static void NotEmpty(this Guid value, Expression<Func<Guid>> reference)
        {
            if (value == Guid.Empty) throw new GuardException(GetParameterName(reference), "Parameter cannot be empty.");
        }

        public static void IsValid<T>(Expression<Func<T>> reference, T value, Func<T, bool> validate, string message)
        {
            if (!validate?.Invoke(value) ?? default(bool)) throw new GuardException(GetParameterName(reference), message);
        }

        public static void IsValid<T>(this T value, Expression<Func<T>> reference, Func<T, bool> validate, string message)
        {
            if (!validate?.Invoke(value) ?? default(bool)) throw new GuardException(GetParameterName(reference), message);
        }

        public static void IsValid<T>(
            Expression<Func<T>> reference,
            T value,
            Func<T, bool> validate,
            string format,
            params object[] args)
        {
            if (!validate?.Invoke(value) ?? default(bool))
                throw new GuardException(GetParameterName(reference), string.Format(format, args));
        }

        public static void IsValid<T>(
            this T value,
            Expression<Func<T>> reference,
            Func<T, bool> validate,
            string format,
            params object[] args)
        {
            if (!validate?.Invoke(value) ?? default(bool))
                throw new GuardException(GetParameterName(reference), string.Format(format, args));
        }

        public static void NotNull<T>(Expression<Func<T>> reference, T value, string format, params object[] args)
        {
            if (value == null) throw new GuardException(string.Format(format, args), GetParameterName(reference));
        }

        public static void NotNullOrEmpty(Expression<Func<string>> reference, string value, string format, params object[] args)
        {
            NotNull(reference, value, format, args);
            if (value.Length == 0) throw new GuardException(GetParameterName(reference), string.Format(format, args));
        }

        public static void NotNullOrEmpty(this string value, Expression<Func<string>> reference, string message)
        {
            NotNull(reference, value, message);
            if (value.Length == 0) throw new GuardException(GetParameterName(reference), message);
        }

        private static string GetParameterName(Expression reference)
        {
            var lambda = reference as LambdaExpression;
            var member = lambda.Body as MemberExpression;

            return member.Member.Name;
        }
    }
}