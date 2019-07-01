using TES3Lib.Base;
using Utility;
using Utility.Attributes;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// AI Follow Package
    /// </summary>
    public class AI_F : Subrecord, IAIPackage
    {
        public float DestinationX { get; set; }

        public float DestinationY { get; set; }

        public float DestinationZ { get; set; }

        public short Duration { get; set; }

        /// <summary>
        /// Always 32 bytes, if EditorId is less, then its padded
        /// with memory junk after null terminator
        /// </summary>
        [SizeInBytes(32)]
        public string TargetEditorId { get; set; }

        /// <summary>
        /// Unknown (0100?)
        /// </summary>
        public short Unknown { get; set; }

        public AI_F()
        {
            DestinationX = 0x7F7FFFFF;
            DestinationY = 0x7F7FFFFF;
            DestinationZ = 0x7F7FFFFF;
            Unknown = 1;
        }

        public AI_F(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            DestinationX = reader.ReadBytes<float>(base.Data);
            DestinationY = reader.ReadBytes<float>(base.Data);
            DestinationZ = reader.ReadBytes<float>(base.Data);
            Duration = reader.ReadBytes<short>(base.Data);
            TargetEditorId = reader.ReadBytes<string>(base.Data, 32);
            Unknown = reader.ReadBytes<short>(base.Data);
        }
    }
}