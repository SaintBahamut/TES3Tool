using System.Collections.Generic;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Optional Animation List creature uses
    /// </summary>
    public class KFFZ : Subrecord
    {
        public List<string> AnimationNames { get; set; }

        public KFFZ(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            AnimationNames = reader.ReadBytes<string>(base.Data, base.Size).Split('\0').ToList();
            AnimationNames.RemoveAll(x => x.Equals(string.Empty));
        }
    }
}
