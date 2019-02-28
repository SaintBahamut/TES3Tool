using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.FLOR
{
    /// <summary>
    /// FormId of produced ingredient
    /// </summary>
    public class PFIG : Subrecord
    {
        public string IngredientProduced { get; set; }

        public PFIG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            IngredientProduced = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");
        }
    }
}
