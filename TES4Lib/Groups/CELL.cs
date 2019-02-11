using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Groups
{
    /// <summary>
    /// Interior cell group
    /// </summary>
    public class CELL : Group
    {
      
        public CELL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var name = reader.ReadBytes<string>(base.Data, 4);
            var size = reader.ReadBytes<int>(base.Data);
        }

        private void BuildCellGroup()
        {

        }

    }
}
