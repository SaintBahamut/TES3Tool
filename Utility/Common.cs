namespace Utility
{
    public static class Common
    {
        public static bool IsNull(object tested) => tested == null ? true : false;

        public static int SetByte(int value, int valueToSet) => value ^ valueToSet;

        public static int UnsetByte(int value, int valueToSet) => value & (~valueToSet);
    }
}