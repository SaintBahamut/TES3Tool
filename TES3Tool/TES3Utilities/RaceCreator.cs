using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES3Lib.Base;
using TES3Lib.Enums;
using TES3Lib.Records;
using TES3Lib.Subrecords.RACE;
using TES3Lib.Subrecords.Shared;

namespace Tes3Tool.TES3Utilities
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
            BodyPart.Upperarm, BodyPart.Upperleg, BodyPart.Wrist
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
                outputRecords.AddRange(CreateBodyParts(config, config.IsMale, bodyParts));

            if (config.IsFemale)
                outputRecords.AddRange(CreateBodyParts(config, config.IsMale, bodyParts));

            return outputRecords;
        }

        private static List<Record> CreateBodyParts(CreatorConfig config, bool IsMale, HashSet<BodyPart> partList)
        {
            var outputRecords = new List<Record>();
            var symbol = IsMale ? "m" : "f";

            if (bodyParts.Contains(BodyPart.Hand))
            {
                var body = new BODY();
                body.FNAM.Name = config.Name;
                body.MODL.ModelPath = $"{config.ModelFolderPath}_{symbol}1st.nif";
                body.BYDT.PartType = BodyPartType.Skin;
                body.NAME.EditorId = $"b_n_{config.EditorId}_{symbol}_hand.1st\0";
                outputRecords.Add(body);
            }
        
            foreach (var part in bodyParts)
            {
                var body = new BODY();

                body.FNAM.Name = config.Name;
                body.MODL.ModelPath = $"{config.ModelFolderPath}_{symbol}.nif";
                body.BYDT.PartType = BodyPartType.Skin;
                body.NAME.EditorId = $"b_n_{config.EditorId}_{symbol}_{part.ToString().ToLower()}\0";
                outputRecords.Add(body);
            }

            return outputRecords;
        }

        public class CreatorConfig
        {
            public string EditorId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

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
                IsBodyPartsOnly = false;
                IsMale = true;
                IsFemale = true;
                IsBeast = false;
            }
        }
    }
}
