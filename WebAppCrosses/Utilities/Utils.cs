using System;
using System.Linq;
using System.Reflection;

namespace WebAppCrosses
{
    public static class Utils
    {
        public static void CopyModeltoEntity(object a, object b)
        {
            foreach (PropertyInfo propA in a.GetType().GetProperties())
            {
                PropertyInfo propB = b.GetType().GetProperty(propA.Name);
                propB.SetValue(b, propA.GetValue(a, null), null);
            }
        }

        // Для получения атрибута [key]
        public static PropertyInfo PropertyByAtt<Type>(this object item) where Type : Attribute
        {
            return item.GetType().GetProperties()
                .FirstOrDefault(pt => pt.GetCustomAttributes(typeof(Type), true)
                .Count() > 0);
        }
    }
}