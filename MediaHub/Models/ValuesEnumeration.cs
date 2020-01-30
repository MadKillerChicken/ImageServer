using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.Models
{
    public abstract class ValuesEnumeration<TValueType> : IComparable where TValueType : Enum
    {
        public TValueType ValueType { get; }

        public string Value => Values.First();

        public IReadOnlyCollection<string> Values { get; }

        protected ValuesEnumeration(TValueType valueType, string extension)
        {
            if (string.IsNullOrWhiteSpace(extension)) { throw new ArgumentNullException(nameof(extension)); }
            Values = new HashSet<string>() {
                extension.ToLower()
            };
            ValueType = valueType;
        }

        protected ValuesEnumeration(TValueType valueType, params string[] extensions)
        {
            if (!extensions.Any()) { throw new ArgumentOutOfRangeException(nameof(extensions)); }
            if (extensions.Any(e => string.IsNullOrEmpty(e))) {
                throw new ArgumentException("One or more extensions are empty.", nameof(extensions));
            }
            Values = new HashSet<string>(extensions.ToList().ConvertAll(e => e.ToLower()));
            ValueType = valueType;
        }

        public override string ToString() => Value;

        public static T GetByValue<T>(string value) where T : ValuesEnumeration<TValueType> =>
            GetAll<T>().FirstOrDefault(e => 
                e.Values.Contains(
                    value?.Trim()?.ToLower() ?? throw new ArgumentNullException(nameof(value))
                ));

        public static IEnumerable<T> GetAllWithType<T>(TValueType valueType) where T : ValuesEnumeration<TValueType>
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>().Where(e => e.ValueType.Equals(valueType));
        }

        public static IEnumerable<T> GetAll<T>() where T : ValuesEnumeration<TValueType>
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as ValuesEnumeration<TValueType>;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            unchecked {
                int hash = (int)2166136261;
                foreach(var value in Values) {
                    hash = hash * 486187739 + value.GetHashCode();
                }
                return hash;
            }
        }

        public int CompareTo(object other) => 
            Value.CompareTo(((ValuesEnumeration<TValueType>)other).Value);
    }
}
