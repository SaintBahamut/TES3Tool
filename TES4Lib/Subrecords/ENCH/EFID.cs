using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES4Lib.Subrecords.ENCH
{
    /// <summary>
    /// Holds an effect name (optional)
    /// There will be an EFID for each effect added to the enchantment.
    /// It seems to be always followed by a matching EFIT subrecord.
    /// Its size seems fixed at 4 bytes.
    /// </summary>
    public class EFID
    {
    }
}
