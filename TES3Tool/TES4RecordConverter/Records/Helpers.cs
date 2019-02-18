using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES3Tool.TES4RecordConverter.Records
{
    internal static class Helpers
    {
        internal static int GetTES4DeletedRecordFlag(int recordFlags)
        {
            return recordFlags & 0x20;
        }

        internal static int GetTES4CantWaitRecordFlag(int recordFlags)
        {
            return recordFlags & 0x080000;
        }

        internal static int GetTES4IgnoredRecordFlag(int recordFlags)
        {
            return recordFlags & 0x01000;
        }

        /// <summary>
        /// For TES4.Records.CELL.Flag
        /// </summary>
        /// <param name="recordFlags"></param>
        /// <returns></returns>
        internal static int GetTES4HasWaterCellFlag(int recordFlags)
        {
            return recordFlags & 0x02;
        }

        /// <summary>
        /// For TES4.Records.CELL.Flag
        /// </summary>
        /// <param name="recordFlags"></param>
        /// <returns></returns>
        internal static int GetTES4BehavesLikeExteriorCellFlag(int recordFlags)
        {
            return recordFlags & 0x80;
        }

        internal static bool IsNull(object tested) => tested == null ? true : false;
    }
}
