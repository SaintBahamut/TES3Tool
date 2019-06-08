using System;

namespace Utility.Attributes
{
    public class SizeInBytesAttribute : Attribute
    {
        public int TypeSize;

        public SizeInBytesAttribute(int typeSize)
        {
            TypeSize = typeSize;
        }
    }
}