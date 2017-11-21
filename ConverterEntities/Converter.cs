using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConverterEntities
{
    public static class Converter
    {
        /// <summary>
        /// Метод для преобразования классов со схожими сигнатурами свойств. 
        /// Метод получает список свойств класса, который необходимо вернуть, затем в цикле перебирет эти свойства. Для каждого свойства ищется 
        /// свойство с таким же именем в базовом(исходном) классе. Если свойство найдено и типы свойст сопадают, то значение совйства исходного класса
        /// помещается в свойство результирующего объекта
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T Convert<T, U>(U entity) where T : new()
        {
            T result = new T();

            // Получаем список свойств результирующего объекта
            var properties = typeof(T).GetProperties(BindingFlags.FlattenHierarchy |
                          BindingFlags.Instance |
                          BindingFlags.Public);

            // запускаем цикл по коллекции свойств
            foreach (var property in properties)
            {
                // Получаем свойство исходго класса с таким же именем
                var entityProperty = typeof(U).GetProperty(property.Name);

                // Если свойство не найдено или типы свойств совпадают, то переходим на сл. итерацию
                if (entityProperty == null || property.PropertyType != entityProperty.PropertyType) continue;

                // Присваиваем значение свойства
                var value = entityProperty.GetValue(entity, null);

                property.SetValue(result, value);
            }

            return result;
        }
    }
}
