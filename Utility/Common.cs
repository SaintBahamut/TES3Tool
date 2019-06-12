using System;
using System.Reflection;

namespace Utility
{
    public static class Common
    {
        public static bool IsNull(object tested) => tested == null ? true : false;

        public static T GetAttributeFromType<T>(PropertyInfo property) where T : Attribute
        {
            T Attrib = (T)property.GetCustomAttribute(typeof(T),true);
            if (IsNull(Attrib)) throw new Exception("No such attribute");
            return Attrib;
        }


        /// <summary>
        /// list of codes
        /// https://docs.microsoft.com/pl-pl/dotnet/api/system.text.encodinginfo?view=netframework-4.8
        /// </summary>
        public static int TextEncodingCode = 1252;
    }
}