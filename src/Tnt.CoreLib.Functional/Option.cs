using System;
using System.Text;

namespace Tnt.CoreLib.Functional
{
    public static class Option
    {
        public static Option<T> Some<T>(T value) => new Option<T>(true, value);
        public static Option<T> None<T>() => new Option<T>(false, default);

        public static T Else<T>(this Option<T> option, T elseValue) => option.IsSpecified ? option.Value : elseValue;
        public static T Else<T>(this Option<T> option, Func<T> elseFunc) => option.IsSpecified ? option.Value : elseFunc();
        public static void If<T>(this Option<T> option, Action<T> ifAction)
        {
            if (option.IsSpecified)
                ifAction(option.Value);
        }
    }

    public struct Option<T> : IEquatable<T>, IEquatable<Option<T>>
    {
        private readonly T _value;
        private readonly bool _specified;

        public bool IsSpecified => _specified;
        public T Value => _specified ? _value : throw new InvalidOperationException("Optional has no value specified.");

        internal Option(bool specified, T value)
        {
            _specified = specified;
            _value = value;
        }

        public static explicit operator T(Option<T> option) => option.Value;
        public static implicit operator Option<T>(T value) => new Option<T>(true, value);

        public static bool operator ==(Option<T> left, Option<T> right) => (!left.IsSpecified && !right.IsSpecified) ||
            left.IsSpecified && right.IsSpecified && left.Value.Equals(right.Value);

        public static bool operator !=(Option<T> left, Option<T> right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is Option<T> other) return this == other;
            if (_specified) return _value.Equals(obj);
            return false;
        }

        public override int GetHashCode()
        {
            if (_specified) return _value.GetHashCode();
            return 0;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Option<")
                .Append(typeof(T).Name)
                .Append(">(")
                .Append(_specified ? $"Some({_value}))" : "None())")
                .ToString();
        }

        bool IEquatable<T>.Equals(T other)
        {
            return Equals(other);
        }

        bool IEquatable<Option<T>>.Equals(Option<T> other)
        {
            return Equals(other);
        }
    }
}