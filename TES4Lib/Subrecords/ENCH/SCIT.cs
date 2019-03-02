using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES4Lib.Subrecords.ENCH
{
    /// <summary>
    /// Holds script effect data (optional)
    /// There will be one SCIT subrecord for each script effect added to the enchantment.
    /// It will be followed by a matching FULL subrecord and preceded by the usual
    /// EFID and EFIT subrecords. The length of the subrecord is almost always 16 bytes
    /// with values of 4 bytes (1 instance) and 12 bytes (2 instances) also observed.
    /// Presumably shorter SCIT subrecords can use default values for the missing fields.
    /// </summary>
    public class SCIT
    {
    }
}
