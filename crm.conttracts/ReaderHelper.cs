using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data
{
    public static class ReaderHelper
    {
        public static List<T> ReadAll<T>(this IDataReader reader) where T : class, new()
        {
            List<T> result = new List<T>();
            while(reader.Read())
            {
                result.Add(reader.Read<T>());
            }
            return result;
        }

        public static T Read<T>(this IDataReader reader) where T : class, new()
        {
            T result = new T();
            Reflection.PropertyInfo[] properties = typeof(T).GetProperties();

            Dictionary<object, object> cache = new Dictionary<object, object>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string fieldName = reader.GetName(i);
                var property = EntityHelper.FindProperty(properties, fieldName);
                if (property != null)
                {
                    var convertedValue = EntityHelper.ConvertToTarget(reader[i], property.PropertyType, cache);
                    try
                    { 
                        property.SetValue(result, convertedValue);
                    }
                    catch(Exception e)
                    {
                        throw new CopyToException($"Ошибка установки свойства {property.Name} из поля {fieldName}. Устанавливаемое значение:{convertedValue}", e);
                    }

                }
            }

            return result;
        }

    }
}
