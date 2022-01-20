using System;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Extensions
{
    /// <summary>
    ///     this class contains extension methods related to reflection
    /// </summary>
    public class ReflectionExtensions
    {
        /// <summary>
        ///     create object based on T
        /// </summary>
        public T CreateInstance<T>()
            where T : class, new()
        {
            var typeOf1 = typeof(T);
            var value = Activator.CreateInstance(typeOf1);
            return (T)value;
        }

        /// <summary>
        ///     get property info list
        /// </summary>
        public PropertyInfo[] GetSetterPropertyList<T>(bool isFilteringSetterOnly)
        {
            var values = GetPropertyList<T>();

            //check if we want to get the setter properties only
            //then filter the properties with setter only
            if (!isFilteringSetterOnly)
            {
                return values;
            }

            values = values.Where(a => a.CanWrite).ToArray();
            return values;
        }

        /// <summary>
        ///     get all properties of certain type
        /// </summary>
        public PropertyInfo[] GetPropertyList<T>()
        {
            var typeOf1 = typeof(T);
            var values = typeOf1.GetProperties();
            return values;
        }
    }
}