namespace Utility
{
    public static class Common
    {
        public static bool IsNull(object tested) => tested == null ? true : false;

        /// <summary>
        /// list of codes
        /// https://docs.microsoft.com/pl-pl/dotnet/api/system.text.encodinginfo?view=netframework-4.8
        /// </summary>
        public static int TextEncodingCode = 1252;
    }
}