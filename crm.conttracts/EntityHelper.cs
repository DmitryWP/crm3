using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace System.Data
{
    public interface ICopiable
    {

    }

    public interface IUpdateFromContract<TTarget, TSource> where TTarget : class, new() where TSource : class
    {
        IUpdateFromContract<TTarget, TSource> Match(Func<TTarget, TSource, bool> predicate);
        IUpdateFromContract<TTarget, TSource> WhenRemoved(Action<TTarget> action);
        IUpdateFromContract<TTarget, TSource> WhenAdded(Action<TSource> action);
        void Update();
        void Update(Action<TTarget, TSource> action);
    }


    public static class EntityHelper
    {
        private static Dictionary<Type, System.Reflection.PropertyInfo[]> TypeFields = new Dictionary<Type, System.Reflection.PropertyInfo[]>();

        public static T CopyTo<T>(this ICopiable source) where T : class, new()
        {
            return EntityHelper.CopyTo<T>(source);
        }
        public static void UpdateFrom(this ICopiable target, object source)
        {
            EntityHelper.UpdateFrom(target, source);
        }

        public static IUpdateFromContract<TTarget, TSource> From<TTarget, TSource>(this ICollection<TTarget> target, ICollection<TSource> source) where TTarget : class, new() where TSource : class
        {
            return new UpdateFromContractImplementation<TTarget, TSource>()
            {
                Target = target,
                Source = source
            };
        }

        public static T CopyTo<T>(object source) where T : class, new()
        {
            if (source == null)
            {
                return null;
            }

            T result = new T();

            CopyTo(result, source, new Dictionary<object, object>());

            return result;

        }

        public static void UpdateFrom(object target, object source)
        {
            var targetFields = GetProperties(target.GetType());
            var sourceFields = GetProperties(source.GetType());

            foreach (var targetProperty in targetFields)
            {
                try
                {
                    //Если поле является свойством внешнего ключа
                    if (isForeignKey(targetProperty, target, out var navigationProperty) && navigationProperty != null)
                    {
                        var sourceNavigationProperty = FindProperty(sourceFields, navigationProperty.Name);
                        object sourceNavigationValue;
                        if (sourceNavigationProperty != null && (sourceNavigationValue = sourceNavigationProperty.GetValue(source)) != null)
                        {
                            //В контракте нашли объект, соответсвующий свойству навигации сущности
                            object keyValue = FindPrimaryKeyValue(sourceNavigationValue, navigationProperty.PropertyType);
                            object value = ConvertToTarget(keyValue, targetProperty.PropertyType, new Dictionary<object, object>());

                            targetProperty.SetValue(target, value);
                            navigationProperty?.SetValue(target, null);
                            continue;

                        }
                        else
                        {
                            //В контракте не нашли объект, соответствующий свойству навигации сущности - пойдем по основному сценарию ниже 
                        }
                    }

                    if ((targetProperty.PropertyType.IsClass || targetProperty.PropertyType.IsInterface) && (targetProperty.PropertyType != typeof(string)))
                    {
                        //дочерние классы и коллекции не изменяем - оставляем это дело вызывающей стороне
                        continue;
                    }

                    Reflection.PropertyInfo sourceProperty = FindProperty(sourceFields, targetProperty.Name);
                    if (sourceProperty == null)
                    {
                        continue;
                    }

                    object sourceValue = sourceProperty?.GetValue(source);
                    object targetOriginalValue = targetProperty.GetValue(target);

                    //Не присваиваем значение, если они и так равны
                    if (sourceValue == targetOriginalValue || sourceValue?.ToString() == targetOriginalValue?.ToString()) continue;

                    object targetValue = ConvertToTarget(sourceValue, targetProperty.PropertyType, new Dictionary<object, object>());
                    targetProperty.SetValue(target, targetValue);

                }
                catch (Exception e)
                {
                    throw new CopyToException($"UpdateFrom fieldName {targetProperty.Name}. Target: <{target}>. Source: <{source}> ", e);
                }
                //End foreach
            }
        }


        class UpdateFromContractImplementation<TTarget, TSource> : IUpdateFromContract<TTarget, TSource> where TTarget : class, new() where TSource : class
        {

            internal Func<TTarget, TSource, bool> Predicate { get; set; }

            internal Action<TTarget> WhenRemovedAction { get; set; }

            internal Action<TSource> WhenAddedAction { get; set; }

            internal ICollection<TTarget> Target { get; set; }

            internal ICollection<TSource> Source { get; set; }

            public IUpdateFromContract<TTarget, TSource> Match(Func<TTarget, TSource, bool> predicate)
            {
                this.Predicate = predicate;
                return this;
            }

            public IUpdateFromContract<TTarget, TSource> WhenRemoved(Action<TTarget> action)
            {
                this.WhenRemovedAction = action;
                return this;
            }
            public IUpdateFromContract<TTarget, TSource> WhenAdded(Action<TSource> action)
            {
                this.WhenAddedAction = action;
                return this;
            }

            private TSource Find(TTarget target)
            {
                Reflection.PropertyInfo targetKey = FindPrimaryKey(typeof(TTarget), null);
                Reflection.PropertyInfo sourceKey = FindPrimaryKey(typeof(TSource), typeof(TTarget));
                if (sourceKey != null && targetKey == null)
                {
                    //Если нашли ключ в источнике, но не нашли в целевом типе - бывает и такое, когда KeyAttribute указан у источника, а не у целевого.
                    //Просто поищем ключ в целевом объекте еще раз
                    targetKey = FindProperty(GetProperties(typeof(TTarget)), sourceKey.Name);
                }

                if (sourceKey == null) throw new CopyToException($"Не удалось найти первичный ключ в классе {typeof(TSource).FullName} и выражение сопоставления также не было указано методом Match");
                if (sourceKey == null) throw new CopyToException($"Не удалось найти первичный ключ в классе {typeof(TTarget).FullName} и выражение сопоставления также не было указано методом Match");
                object targetKeyValue = ConvertToTarget(targetKey.GetValue(target), sourceKey.PropertyType, new Dictionary<object, object>());
                return Source.SingleOrDefault(s => sourceKey.GetValue(s).Equals(targetKeyValue));
            }


            public void Update(Action<TTarget, TSource> action)
            {
                List<TSource> Matched = new List<TSource>();
                foreach (var item in Target.ToList())
                {
                    TSource matched;
                    if (Predicate != null)
                    {
                        matched = Source.SingleOrDefault(a => Predicate(item, a));
                    }
                    else
                    {
                        matched = Find(item);
                    }
                    if (matched == null)
                    {
                        if (WhenRemovedAction == null)
                        {
                            Target.Remove(item);
                        }
                        else
                        {
                            WhenRemovedAction(item);
                        }
                    }
                    else
                    {
                        Matched.Add(matched);
                        action(item, matched);
                    }
                }

                foreach (var item in Source)
                {
                    if (!Matched.Contains(item))
                    {
                        if (WhenAddedAction == null)
                        {
                            var newObject = new TTarget();
                            Target.Add(newObject);
                            action(newObject, item);
                        }
                        else
                        {
                            WhenAddedAction(item);
                        }
                    }
                }
            }

            public void Update()
            {
                Update((target, source) => EntityHelper.UpdateFrom(target, source));
            }
        }

        internal static object ConvertToTarget(object sourceValue, Type targetType, Dictionary<object, object> Cache)
        {
            try
            {

                if (sourceValue == null || sourceValue == DBNull.Value)
                {
                    if (!targetType.IsValueType || targetType.FullName.StartsWith("System.Nullable"))
                    {
                        return null;
                    }
                    else
                    {
                        throw new CopyToException($"Поле с типом {targetType.Name} не может содержать null");
                    }
                }

                Type sourceType = sourceValue.GetType();


                if (targetType == sourceType)
                {
                    return sourceValue;
                }

                if (targetType == typeof(string))
                {
                    //в строку через ToString()
                    return sourceValue.ToString();
                }

                if (targetType.IsClass || targetType.IsInterface)
                {
                    object convertedValue;
                    if (Cache.ContainsKey(sourceValue))
                    {
                        convertedValue = Cache[sourceValue];
                    }
                    else
                    {
                        convertedValue = ConvertToClass(targetType, sourceType, sourceValue, Cache);
                    }
                    return convertedValue;
                }

                if (targetType.IsGenericType && targetType.FullName.StartsWith("System.Nullable"))
                {
                    targetType = targetType.GenericTypeArguments[0];
                }


                if (targetType.IsEnum)
                {
                    return ConvertToEnum(targetType, sourceType, sourceValue);
                }

                if (targetType.IsValueType && sourceType.IsValueType)
                {
                    if (targetType == typeof(DateTime) && sourceType == typeof(DateTimeOffset))
                    {
                        return (((DateTimeOffset)sourceValue).DateTime);
                    }
                    if (targetType == typeof(DateTimeOffset) && sourceType == typeof(DateTime))
                    {
                        return new DateTimeOffset((DateTime)sourceValue);
                    }

                    object convertedValue = Convert.ChangeType(sourceValue, targetType);
                    return convertedValue;
                }

                Reflection.MethodInfo parseMethod;
                if (sourceType == typeof(string) && (parseMethod = FindParseMethod(targetType)) != null)
                {
                    return parseMethod.Invoke(null, new object[] { sourceValue });
                }
                return null;
            }
            catch (Exception e)
            {
                throw new InvalidCastException($"Не удалось конвертировать объект с типом {sourceValue.GetType().FullName} и значением {sourceValue.ToString()} в тип {targetType.FullName}", e);
            }
        }

        private static Reflection.MethodInfo FindParseMethod(Type objectType)
        {
            return objectType.GetMethod("Parse", Reflection.BindingFlags.Static | Reflection.BindingFlags.Public);
        }

        private static Reflection.PropertyInfo FindPrimaryKey(Type contractType, Type entityType)
        {
            var properties = GetProperties(contractType);
            //Проверяем, вдруг у контракта для первичного ключа задан атрибут
            Reflection.PropertyInfo keyProperty = properties.SingleOrDefault(pr => pr.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)));
            if (keyProperty == null)
            {
                //В контракте атрибут не задан. 
                if (entityType != null)
                {
                    //Поищем этот атрибут у свойств сущности
                    var entityProperties = GetProperties(entityType);
                    Reflection.PropertyInfo entityKeyProperty = entityProperties.SingleOrDefault(pr => pr.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)));
                    if (entityKeyProperty == null)
                    {
                        entityKeyProperty = entityProperties.SingleOrDefault(pi => pi.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                    }
                    if (entityKeyProperty != null)
                    {
                        keyProperty = FindProperty(properties, entityKeyProperty.Name);
                    }
                }
                else
                {
                    keyProperty = properties.SingleOrDefault(pi => pi.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                }
            }

            return keyProperty;
        }

        public static object FindPrimaryKeyValue(object contract, Type entityType)
        {
            Reflection.PropertyInfo keyProperty = FindPrimaryKey(contract.GetType(), entityType);
            return keyProperty?.GetValue(contract);
        }

        private static bool isForeignKey(Reflection.PropertyInfo targetField, object target, out Reflection.PropertyInfo navigationProperty)
        {
            navigationProperty = null;
            if (targetField.PropertyType.IsClass || targetField.PropertyType.IsInterface) return false;

            var properties = GetProperties(target.GetType());

            //Если для свойства внешнего ключа указан атрибут ForeignKey, то его имя указывает на свойство навигации
            string navigationPropertyName = targetField.GetCustomAttributes(true).OfType<ForeignKeyAttribute>().SingleOrDefault()?.Name;
            if (!string.IsNullOrWhiteSpace(navigationPropertyName))
            {
                navigationProperty = properties.SingleOrDefault(p => p.Name == navigationPropertyName);
                return true;
            }

            //Если для свойства навигации указан атрибут ForeignKey, то его имя указывает на свойство внешнего ключа, которое должно быть равно текущему свойству
            navigationProperty = properties.SingleOrDefault(pr =>
                pr.CustomAttributes.Any(ca => ca.AttributeType == typeof(ForeignKeyAttribute))
                && pr.GetCustomAttributes(true).OfType<ForeignKeyAttribute>().Any(at => at.Name == targetField.Name));
            if (navigationProperty != null)
            {
                return true;
            }

            //Атрибут ForeignKey не указан - попробуем найти свойство по соглашению имен.
            if (targetField.Name.EndsWith("id", StringComparison.InvariantCultureIgnoreCase))
            {
                string expectedName = targetField.Name.Substring(0, targetField.Name.Length - 2);
                if (expectedName.EndsWith("_")) expectedName = expectedName.Substring(0, expectedName.Length - 1);

                navigationProperty = FindProperty(properties, expectedName);
            }

            return navigationProperty != null;
        }


        private static void CopyTo(object Target, object Source, Dictionary<object, object> Cache)
        {
            Cache[Source] = Target;

            var sourceFields = GetProperties(Source.GetType());
            var targetFields = GetProperties(Target.GetType());

            foreach (var targetField in targetFields)
            {
                string fieldName = targetField.Name;
                Type targetType = targetField.PropertyType;
                try
                {
                    Reflection.PropertyInfo source;
                    object sourceValue = Source;
                    if (targetField.CustomAttributes.Any(a => a.AttributeType == typeof(CopyFromAttribute)))
                    {
                        source = FindProperty(targetField, ref sourceValue);
                    }
                    else
                    {
                        source = FindProperty(sourceFields, fieldName);
                        sourceValue = source?.GetValue(Source);
                    }
                    if (source == null) continue;

                    Type sourceType = source.PropertyType;
                    object convertedValue = ConvertToTarget(sourceValue, targetType, Cache);
                    targetField.SetValue(Target, convertedValue);
                }
                catch (Exception e)
                {
                    throw new CopyToException(fieldName, e);
                }
                //End foreach
            }
        }

        private static object ConvertToCollection(Type targetType, Type sourceType, object sourceValue, Dictionary<object, object> Cache)
        {
            System.Collections.IList convertedValue;
            Type elementType = null;

            #region Создаем коллекцию для конвертированных значений
            if (targetType.IsArray)
            {
                elementType = targetType.GetElementType();
            }
            else if (targetType.IsInterface)
            {
                if (targetType.IsGenericType)
                {
                    elementType = targetType.GenericTypeArguments[0];
                }
                else
                {
                    elementType = typeof(object);
                }
            }

            if (elementType != null)
            {
                convertedValue = (System.Collections.IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            }
            else
            {
                if (targetType.IsGenericType)
                {
                    elementType = targetType.GenericTypeArguments[0];
                }
                else
                {
                    elementType = typeof(object);
                }
                convertedValue = (System.Collections.IList)Activator.CreateInstance(targetType);
            }
            #endregion

            foreach (object listItem in (sourceValue as System.Collections.IEnumerable))
            {

                object convertedListValue;
                if (elementType == typeof(object))
                {
                    convertedListValue = listItem;
                }
                else
                {
                    if (Cache.ContainsKey(listItem))
                    {
                        convertedListValue = Cache[listItem];
                    }
                    else
                    {
                        convertedListValue = Activator.CreateInstance(elementType);
                        CopyTo(convertedListValue, listItem, Cache);
                    }
                }
                convertedValue.Add(convertedListValue);
            }

            #region Если целевой тип - массив, надо лист переделать в массив
            if (targetType.IsArray)
            {
                Array array = Array.CreateInstance(elementType, convertedValue.Count);
                convertedValue.CopyTo(array, 0);
                return array;
            }
            #endregion
            return convertedValue;

        }

        private static object ConvertToClass(Type targetType, Type sourceType, object sourceValue, Dictionary<object, object> Cache)
        {
            if (sourceValue == null) return null;
            if (sourceValue is System.Collections.IEnumerable
                && (targetType == typeof(Collections.IEnumerable)
                    || targetType.GetInterface("System.Collections.IEnumerable") != null))
            {
                return ConvertToCollection(targetType, sourceType, sourceValue, Cache);
            }

            object convertedValue = Activator.CreateInstance(targetType);
            CopyTo(convertedValue, sourceValue, Cache);

            return convertedValue;
        }




        private static object ConvertToEnum(Type targetType, Type sourceType, object value)
        {
            if (value == null)
            {
                return null;
            }

            if (sourceType.IsGenericType && sourceType.FullName.StartsWith("System.Nullable"))
            {
                sourceType = sourceType.GenericTypeArguments[0];
            }

            if (sourceType == typeof(string) || sourceType.IsEnum)
            {
                //Enum или строка в Enum через Parse(string)
                return Enum.Parse(targetType, value.ToString());
            }
            else if (sourceType.IsValueType && (sourceType.FullName.StartsWith("System.Int") || sourceType.FullName.StartsWith("System.UInt")))
            {
                //intXX в Enum через конвертацию
                return Enum.ToObject(targetType, Convert.ChangeType(value, targetType.GetEnumUnderlyingType()));
            }
            else
            {
                throw new CopyToException($"Полю с типом {targetType.FullName} не может быть присвоено значение с типом {sourceType.FullName}");
            }
        }


        public static System.Reflection.PropertyInfo[] GetProperties(Type type)
        {
            if (type.Namespace != null && type.Namespace.Contains("DynamicProxies"))
            {
                type = type.BaseType;
            }

            System.Reflection.PropertyInfo[] result;

            lock (TypeFields)
            {
                if (!TypeFields.TryGetValue(type, out result))
                {
                    result = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    TypeFields[type] = result;
                }
            }
            return result;
        }


        public static System.Reflection.PropertyInfo FindProperty(System.Reflection.PropertyInfo[] objectProperties, string propertyName)
        {
            var source = objectProperties.SingleOrDefault(pi => pi.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

            if (source == null)
            {
                source = objectProperties.SingleOrDefault(pi => pi.Name.Replace("_", "").Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
            }

            if (source == null)
            {
                source = objectProperties.SingleOrDefault(pi => pi.Name.Equals(propertyName.Replace("_", ""), StringComparison.InvariantCultureIgnoreCase));
            }

            if (source == null)
            {
                source = objectProperties.SingleOrDefault(pi => pi.GetCustomAttributes(true).OfType<CopyFromAttribute>().Any(a => a.getPath() == propertyName));
            }
            return source;
        }

        private static System.Reflection.PropertyInfo FindProperty(System.Reflection.PropertyInfo targetField, ref object localSource)
        {
            System.Reflection.PropertyInfo sourceProperty = null;
            foreach (var Attribute in targetField.GetCustomAttributes(true).OfType<CopyFromAttribute>())
            {
                sourceProperty = FindProperty(sourceProperty, Attribute.getPath().Split('.'), ref localSource);
                if (sourceProperty != null) break;
            }
            return sourceProperty;
        }

        private static System.Reflection.PropertyInfo FindProperty(System.Reflection.PropertyInfo sourceProperty, string[] propertyNames, ref object localSource)
        {
            if (propertyNames.Length == 0)
            {
                return sourceProperty;
            }

            var sourceProperties = GetProperties(localSource.GetType());
            var result = FindProperty(sourceProperties, propertyNames[0]);
            if (result == null) return null;

            if (localSource != null)
            {
                localSource = result.GetValue(localSource);
            }
            return FindProperty(result, propertyNames.Skip(1).ToArray(), ref localSource);
        }
    }



    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CopyFromAttribute : Attribute
    {
        private string path;
        public CopyFromAttribute(string Path)
        {
            path = Path;
        }

        public string getPath()
        {
            return path;
        }
    }
}
