using System;
using System.Collections.Generic;
using TES3Lib.Base;
using TES3Oblivion.Records.SIPostProcessing.Definitions;

namespace TES3Oblivion.SIPostProcessing.Definitions
{
    public static class EquipementItemsMap
    {
        /// <summary>
        /// EditorId:ProcessingMethod
        /// </summary>
        public static Dictionary<string, Action<IEquipement>> ProcessItem = new Dictionary<string, Action<IEquipement>>();

        static EquipementItemsMap()
        {
            ProcessItem.Add("SESylsDress\0", x=>EquipementProcessing.SESylsDress(x));
        }
    }
}
