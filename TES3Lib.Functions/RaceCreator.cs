using System;
using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums;
using TES3Lib.Enums.Flags;
using TES3Lib.Records;

namespace TES3Lib.Functions
{
    /// <summary>
    /// Create race template (Better Bodies)
    /// With body parts
    /// </summary>
    public static class RaceCreator
    {
        private static HashSet<BodyPart> bodyParts = new HashSet<BodyPart>
        {
            BodyPart.Ankle, BodyPart.Chest,BodyPart.Foot, BodyPart.Forearm,
            BodyPart.Groin, BodyPart.Hand, BodyPart.Knee, BodyPart.Neck,
            BodyPart.Upperarm, BodyPart.Upperleg, BodyPart.Wrist, BodyPart.Hair,
            BodyPart.Head
        };

        public static List<Record> CreateRace(CreatorConfig config)
        {
            var outputRecords = new List<Record>();

            if (!config.IsBodyPartsOnly)
            {
                var race = new RACE();
                race.NAME.EditorId = String.IsNullOrEmpty(config.EditorId) ? "\0" : $"{config.EditorId}\0";
                race.FNAM.Name = String.IsNullOrEmpty(config.EditorId) ? "\0" : $"{config.Name}\0";
                race.DESC.Description = String.IsNullOrEmpty(config.EditorId) ? "\0" : $"{config.Description}\0";
                outputRecords.Add(race);
            }

            if(config.IsMale)
                outputRecords.AddRange(CreateBodyParts(config, true, bodyParts));

            if (config.IsFemale)
                outputRecords.AddRange(CreateBodyParts(config, false, bodyParts));

            return outputRecords;
        }

        private static List<Record> CreateBodyParts(CreatorConfig config, bool IsMale, HashSet<BodyPart> partList)
        {
            var outputRecords = new List<Record>();
            var symbol = IsMale ? "m" : "f";
            var EditorIdTrimmed = config.EditorId.TrimEnd('\0');

            if (bodyParts.Contains(BodyPart.Hand))
            {
                var body = new BODY();
                body.BYDT.BodyPart = BodyPart.Hand;
                body.FNAM.Name = config.EditorId;
                body.MODL.ModelPath = $"{config.ModelFolderPath}_{symbol}_body1st.nif\0";
                body.BYDT.PartType = BodyPartType.Skin;
                body.NAME.EditorId = $"{config.EditorIdPrefix}b_n_{EditorIdTrimmed}_{symbol}_hand.1st\0";
                body.BYDT.Flags.Add(BodyPartFlag.Playable);
                if (!IsMale)
                {
                    body.BYDT.Flags.Add(BodyPartFlag.Female);
                }

                outputRecords.Add(body);
            }
        
            foreach (var part in bodyParts)
            {
                var body = new BODY();
                body.BYDT.BodyPart = part;
                body.FNAM.Name = config.EditorId;
                
                if(part.Equals(BodyPart.Head))
                {
                    body.NAME.EditorId = $"{config.EditorIdPrefix}b_n_{EditorIdTrimmed}_{symbol}_head01\0";
                    body.MODL.ModelPath = $"{config.ModelFolderPath}_{symbol}_head01.nif\0";
                }
                else if(part.Equals(BodyPart.Hair))
                {
                    body.NAME.EditorId = $"{config.EditorIdPrefix}b_n_{EditorIdTrimmed}_{symbol}_hair01\0";
                    body.MODL.ModelPath = $"{config.ModelFolderPath}_{symbol}_hair01.nif\0";
                }
                else
                {
                    body.NAME.EditorId = $"{config.EditorIdPrefix}b_n_{EditorIdTrimmed}_{symbol}_{part.ToString().ToLower()}\0";
                    body.MODL.ModelPath = $"{config.ModelFolderPath}_{symbol}_body.nif\0";
                }

                body.BYDT.PartType = BodyPartType.Skin;
               
                body.BYDT.Flags.Add(BodyPartFlag.Playable);
                if (!IsMale)
                {
                    body.BYDT.Flags.Add(BodyPartFlag.Female);
                }
                outputRecords.Add(body);
            }

            return outputRecords;
        }

        public class CreatorConfig
        {
            public string EditorId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string EditorIdPrefix { get; set; }

            /// <summary>
            /// Format of path: Race\\FilePrefix*
            /// Model type (head,hair,hand1st is added proceduraly)
            /// </summary>
            public string ModelFolderPath { get; set; }

            public bool IsBodyPartsOnly { get; set; }

            public bool IsMale { get; set; }

            public bool IsFemale { get; set; }

            public bool IsBeast { get; set; }

            public CreatorConfig()
            {
                EditorIdPrefix = String.Empty;
                IsBodyPartsOnly = false;
                IsMale = true;
                IsFemale = true;
                IsBeast = false;
            }
        }
    }
}
