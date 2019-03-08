namespace Utility
{
    public static class Common
    {
        public static bool IsNull(object tested) => tested == null ? true : false;

        public static bool CheckIfByteSet(int value, int valueToCheck) => !((value & valueToCheck).Equals(0));

        public static int SetByte(int value, int valueToSet) => value ^ valueToSet;

        public static int UnsetByte(int value, int valueToSet) => value & (~valueToSet);
    }
}