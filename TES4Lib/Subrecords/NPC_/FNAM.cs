using System.Diagnostics;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// Unknown, but is a fixed value for every race. 
    /// Does not get updated unless you visit the face tab in the editor.
    /// </summary>
    [DebuggerDisplay("faceRace: {faceRace}")]
    public class FNAM : Subrecord
    {
        public ushort faceRace { get; set; }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            faceRace = reader.ReadBytes<ushort>(base.Data);
        }
    }
}
