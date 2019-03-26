using System.Collections.Generic;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Models
    /// NIF-files used by the creature
    /// </summary>
    public class NIFZ : Subrecord
    {
        public List<string> ModelNames { get; set; }

        public NIFZ(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ModelNames = reader.ReadBytes<string>(base.Data, base.Size).Split('\0').ToList();
            ModelNames.RemoveAll(x => x.Equals(string.Empty));
        }
    }
}
