using TES4Lib.Base;

namespace TES4Lib.Records
{
    /// <summary>
    /// Not supported
    /// </summary>
    public class ROAD : Record
    {
        public ROAD(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}