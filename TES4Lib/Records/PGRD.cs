using TES4Lib.Base;

namespace TES4Lib.Records
{
    /// <summary>
    /// Not supported
    /// </summary>
    public class PGRD : Record
    {
        public PGRD(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}