using TES3Lib.Base;
using Utility;
using Utility.Attributes;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// AI Activate Package
    /// </summary>
    public class AI_A : Subrecord, IAIPackage
    {
        //32 characters target for activation
        [SizeInBytes(32)]
        public string TargetName { get; set; }

        public byte Unknown { get; set; }

        public AI_A()
        {
        }

        public AI_A(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            TargetName = reader.ReadBytes<string>(base.Data, 32);
            Unknown = reader.ReadBytes<byte>(base.Data);
        }
    }
}