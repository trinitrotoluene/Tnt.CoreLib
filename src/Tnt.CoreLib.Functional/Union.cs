using System;
using System.Linq;
using System.Text;

namespace Tnt.CoreLib.Functional
{
    public abstract class Union : IEquatable<Union>
    {
        protected readonly Type[] _valueTypes;
        protected readonly Type _valueType;
        protected readonly object _value;

        internal object Value => _value;
        internal Type ValueType => _valueType;
        internal Type[] ValueTypes => _valueTypes;

        internal Union(Type[] valueTypes, object value)
        {
            _valueTypes = valueTypes;
            _value = value;
            _valueType = value.GetType();
        }

        protected void InternalMatch(int valueIndex, params Action<object>[] matchDelegates)
        {
            matchDelegates[valueIndex](_value);
        }

        public bool Equals(Union other)
        {
            return other.ValueTypes.Equals(ValueTypes) &&
                    other.ValueType.Equals(ValueType) &&
                    other.Value.Equals(Value);
        }

        public override bool Equals(object obj)
        {
            return (obj is Union other) ?
                Equals(other) : 
                false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Union<")
                .Append(string.Join(", ", _valueTypes.Select(x => x.Name)))
                .Append(">(")
                .Append(_valueType.Name)
                .Append("=")
                .Append(_value)
                .Append(")");
            return sb.ToString();
        }

        public static Union<T1, T2> Of<T1, T2>(T1 value) => new Union<T1, T2>(value);
        public static Union<T1, T2> Of<T1, T2>(T2 value) => new Union<T1, T2>(value);

        public static Union<T1, T2, T3> Of<T1, T2, T3>(T1 value) => new Union<T1, T2, T3>(value);
        public static Union<T1, T2, T3> Of<T1, T2, T3>(T2 value) => new Union<T1, T2, T3>(value);
        public static Union<T1, T2, T3> Of<T1, T2, T3>(T3 value) => new Union<T1, T2, T3>(value);

        public static Union<T1, T2, T3, T4> Of<T1, T2, T3, T4>(T1 value) => new Union<T1, T2, T3, T4>(value);
        public static Union<T1, T2, T3, T4> Of<T1, T2, T3, T4>(T2 value) => new Union<T1, T2, T3, T4>(value);
        public static Union<T1, T2, T3, T4> Of<T1, T2, T3, T4>(T3 value) => new Union<T1, T2, T3, T4>(value);
        public static Union<T1, T2, T3, T4> Of<T1, T2, T3, T4>(T4 value) => new Union<T1, T2, T3, T4>(value);

        public static Union<T1, T2, T3, T4, T5> Of<T1, T2, T3, T4, T5>(T1 value) => new Union<T1, T2, T3, T4, T5>(value);
        public static Union<T1, T2, T3, T4, T5> Of<T1, T2, T3, T4, T5>(T2 value) => new Union<T1, T2, T3, T4, T5>(value);
        public static Union<T1, T2, T3, T4, T5> Of<T1, T2, T3, T4, T5>(T3 value) => new Union<T1, T2, T3, T4, T5>(value);
        public static Union<T1, T2, T3, T4, T5> Of<T1, T2, T3, T4, T5>(T4 value) => new Union<T1, T2, T3, T4, T5>(value);
        public static Union<T1, T2, T3, T4, T5> Of<T1, T2, T3, T4, T5>(T5 value) => new Union<T1, T2, T3, T4, T5>(value);

        public static Union<T1, T2, T3, T4, T5, T6> Of<T1, T2, T3, T4, T5, T6>(T1 value) => new Union<T1, T2, T3, T4, T5, T6>(value);
        public static Union<T1, T2, T3, T4, T5, T6> Of<T1, T2, T3, T4, T5, T6>(T2 value) => new Union<T1, T2, T3, T4, T5, T6>(value);
        public static Union<T1, T2, T3, T4, T5, T6> Of<T1, T2, T3, T4, T5, T6>(T3 value) => new Union<T1, T2, T3, T4, T5, T6>(value);
        public static Union<T1, T2, T3, T4, T5, T6> Of<T1, T2, T3, T4, T5, T6>(T4 value) => new Union<T1, T2, T3, T4, T5, T6>(value);
        public static Union<T1, T2, T3, T4, T5, T6> Of<T1, T2, T3, T4, T5, T6>(T5 value) => new Union<T1, T2, T3, T4, T5, T6>(value);
        public static Union<T1, T2, T3, T4, T5, T6> Of<T1, T2, T3, T4, T5, T6>(T6 value) => new Union<T1, T2, T3, T4, T5, T6>(value);

        public static bool operator ==(Union left, Union right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Union left, Union right)
        {
            return !left.Equals(right);
        }
    }

    public sealed class Union<T1, T2> : Union
    {
        private readonly static Type[] s_valueTypes = new[] { typeof(T1), typeof(T2) };
        internal Union(T1 value) : base(s_valueTypes, value) { }
        internal Union(T2 value) : base(s_valueTypes, value) { }

        public void Match(Action<T1> t1, Action<T2> t2)
        {
            var valueIndex = Array.IndexOf(s_valueTypes, _valueType);
            InternalMatch(valueIndex, x => t1((T1)x), x => t2((T2)x));
        }
    }

    public sealed class Union<T1, T2, T3> : Union
    {
        private readonly static Type[] s_valueTypes = new[] { typeof(T1), typeof(T2), typeof(T3) };
        internal Union(T1 value) : base(s_valueTypes, value) { }
        internal Union(T2 value) : base(s_valueTypes, value) { }
        internal Union(T3 value) : base(s_valueTypes, value) { }

        public void Match(Action<T1> t1, Action<T2> t2, Action<T3> t3)
        {
            var valueIndex = Array.IndexOf(s_valueTypes, _valueType);
            InternalMatch(valueIndex, x => t1((T1)x), x => t2((T2)x), x => t3((T3)x));
        }
    }

    public sealed class Union<T1, T2, T3, T4> : Union
    {
        private readonly static Type[] s_valueTypes = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };
        internal Union(T1 value) : base(s_valueTypes, value) { }
        internal Union(T2 value) : base(s_valueTypes, value) { }
        internal Union(T3 value) : base(s_valueTypes, value) { }
        internal Union(T4 value) : base(s_valueTypes, value) { }

        public void Match(Action<T1> t1, Action<T2> t2, Action<T3> t3, Action<T4> t4)
        {
            var valueIndex = Array.IndexOf(s_valueTypes, _valueType);
            InternalMatch(valueIndex, x => t1((T1)x), x => t2((T2)x), x => t3((T3)x), x => t4((T4)x));
        }
    }

    public sealed class Union<T1, T2, T3, T4, T5> : Union
    {
        private readonly static Type[] s_valueTypes = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };
        internal Union(T1 value) : base(s_valueTypes, value) { }
        internal Union(T2 value) : base(s_valueTypes, value) { }
        internal Union(T3 value) : base(s_valueTypes, value) { }
        internal Union(T4 value) : base(s_valueTypes, value) { }
        internal Union(T5 value) : base(s_valueTypes, value) { }

        public void Match(Action<T1> t1, Action<T2> t2, Action<T3> t3, Action<T4> t4, Action<T5> t5)
        {
            var valueIndex = Array.IndexOf(s_valueTypes, _valueType);
            InternalMatch(valueIndex, x => t1((T1)x), x => t2((T2)x), x => t3((T3)x), x => t4((T4)x), x => t5((T5)x));
        }
    }

    public sealed class Union<T1, T2, T3, T4, T5, T6> : Union
    {
        private readonly static Type[] s_valueTypes = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };
        internal Union(T1 value) : base(s_valueTypes, value) { }
        internal Union(T2 value) : base(s_valueTypes, value) { }
        internal Union(T3 value) : base(s_valueTypes, value) { }
        internal Union(T4 value) : base(s_valueTypes, value) { }
        internal Union(T5 value) : base(s_valueTypes, value) { }
        internal Union(T6 value) : base(s_valueTypes, value) { }

        public void Match(Action<T1> t1, Action<T2> t2, Action<T3> t3, Action<T4> t4, Action<T5> t5, Action<T6> t6)
        {
            var valueIndex = Array.IndexOf(s_valueTypes, _valueType);
            InternalMatch(valueIndex, x => t1((T1)x), x => t2((T2)x), x => t3((T3)x), x => t4((T4)x), x => t5((T5)x), x => t6((T6)x));
        }
    }
}