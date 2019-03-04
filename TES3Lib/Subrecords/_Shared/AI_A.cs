using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// AI Activate Package
    /// </summary>
    public class AI_A : Subrecord
    {
        //32 characters target for activation
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