using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES3Lib.Base;
using TES3Lib.Records;
using TES3Lib.Subrecords.ARMO;
using TES3Lib.Subrecords.LEVI;
using TES3Lib.Subrecords.Shared;
using TES3Oblivion.Records.SIPostProcessing.Definitions;
using static TES3Oblivion.Helpers;
using static Utility.Common;

namespace TES3Oblivion.SIPostProcessing
{
    static class EquipementSplitter
    {
        /// <summary>
        /// Splits order knight armor leveled list and adds new parts to npcs
        /// </summary>
        internal static void SELL0NPCOrderKnightArmor100()
        {

            LEVI leveled = ConvertedRecords["LEVI"].Find(x => x.EditorId.Equals("SELL0NPCOrderKnightArmor100\0")).Record as LEVI;
            if (IsNull(leveled))
                throw new Exception("No such LEVI");

            var itemReferencesBag = new ConcurrentBag<ARMO>();
            Parallel.ForEach(leveled.ITEM, (item) =>
            {
                itemReferencesBag.Add(ConvertedRecords["ARMO"].Find(x => x.EditorId.Equals(item.INAM.ItemEditorId)).Record as ARMO);
            });
            var itemReferences = itemReferencesBag.ToList();

            if (itemReferences.Count == 0)
                throw new Exception("No records found");

            var partitions = new Dictionary<string, LEVI>();
            var names = new Dictionary<string, string>();
            var processingMap = new Dictionary<string, Action<IEquipement>>();
            {
                processingMap.Add("Helm", EquipementProcessing.SEOrderKnightHelm);
                processingMap.Add("PauldL", EquipementProcessing.SEOrderKnightPauldronLeft);
                processingMap.Add("PauldR", EquipementProcessing.SEOrderKnightPauldronRight);
                processingMap.Add("GloveL", EquipementProcessing.SEOrderKnightGloveLeft);
                processingMap.Add("GloveR", EquipementProcessing.SEOrderKnightGloveRight);
                processingMap.Add("Greaves", EquipementProcessing.SEOrderKnightGreaves);
                processingMap.Add("Boots", EquipementProcessing.SEOrderKnightBoots);

                partitions.Add("Helm", CloneLeveledList(leveled));
                partitions.Add("PauldL", CloneLeveledList(leveled));
                partitions.Add("PauldR", CloneLeveledList(leveled));
                partitions.Add("GloveL", CloneLeveledList(leveled));
                partitions.Add("GloveR", CloneLeveledList(leveled));
                partitions.Add("Greaves", CloneLeveledList(leveled));
                partitions.Add("Boots", CloneLeveledList(leveled));


                names.Add("Helm", "Helm");
                names.Add("PauldL", "Left Pauldron");
                names.Add("PauldR", "Right Pauldron");
                names.Add("GloveL", "Left Glove");
                names.Add("GloveR", "Right Glove");
                names.Add("Greaves", "Greaves");
                names.Add("Boots", "Boots");
            }

            foreach (var key in partitions.Keys)
            {
                partitions[key].NAME.EditorId = partitions[key].NAME.EditorId.Replace("Armor", key);
                foreach (var item in partitions[key].ITEM)
                {
                    ARMO referenceItem = itemReferences.ToList().Find(x => x.NAME.EditorId.Equals(item.INAM.ItemEditorId));
                    var newItem = CloneArmor(referenceItem);

                    newItem.NAME.EditorId = newItem.NAME.EditorId.Replace("Armor", key);
                    newItem.FNAM.Name = newItem.FNAM.Name.Replace("Armor", names[key]);
                    processingMap[key].Invoke(newItem);

                    ConvertedRecords["ARMO"].Add(new ConvertedRecordData("generated", "ARMO", newItem.NAME.EditorId, newItem));
                    item.INAM.ItemEditorId = newItem.NAME.EditorId;
                }
                ConvertedRecords["LEVI"].Add(new ConvertedRecordData("generated", "LEVI", partitions[key].NAME.EditorId, partitions[key]));
            }

            foreach (var item in itemReferences)
            {
                EquipementProcessing.SEOrderKnightCuirass(item);
            }


            Parallel.ForEach(ConvertedRecords["NPC_"], ( npc) =>
            {
                bool itemPresent = false;
                foreach (var item in (npc.Record as NPC_).NPCO )
                {
                   
                    if(item.ItemId.Contains(leveled.NAME.EditorId))
                    {
                        itemPresent = true;
                        break;
                    }
                }
                if(itemPresent) (npc.Record as NPC_).NPCO.AddRange(partitions.Values.Select(x => new NPCO { Count = 1, ItemId = x.NAME.EditorId }));
            });

        }

        static LEVI CloneLeveledList(LEVI source)
        {
            var clone = new LEVI();
            clone.NAME.EditorId = source.NAME.EditorId;
            clone.DATA.Flag = source.DATA.Flag;
            clone.NNAM.ChanceNone = source.NNAM.ChanceNone;
            clone.INDX.ItemCount = source.INDX.ItemCount;
            foreach (var item in source.ITEM)
            {
                clone.ITEM.Add((new INAM { ItemEditorId = item.INAM.ItemEditorId }, new INTV { PCLevelOfPrevious = item.INTV.PCLevelOfPrevious }));
            }
            return clone;
        }

        static ARMO CloneArmor(ARMO source)
        {
            var clone = new ARMO
            {
                NAME = new NAME { EditorId = source.NAME.EditorId },
                MODL = new MODL { ModelPath = source.MODL.ModelPath },
                FNAM = new FNAM { Name = source.FNAM.Name },
                AODT = new AODT
                {
                    Type = source.AODT.Type,
                    Weight = source.AODT.Weight,
                    Value = source.AODT.Value,
                    Health = source.AODT.Health,
                    EnchancmentPoints = source.AODT.EnchancmentPoints,
                    ArmorRating = source.AODT.ArmorRating
                },
                ITEX = new ITEX { IconPath = source.ITEX.IconPath },
                ENAM = source.ENAM!=null ? new ENAM { EnchantmentId = source.ENAM.EnchantmentId } : null
            };

            return clone;
        }
    }
}
