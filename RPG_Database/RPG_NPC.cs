using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPG_Database
{
    [Table("RPG_NPC")]
    public class RPG_NPC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NPCID { get; set; }

        public int? PositionX { get; set; }

        public int? PositionY { get; set; }

        public string SpritesheetPath { get; set; }

        public int? NPCCategoryID { get; set; }

        public RPG_NPCCategory NPCCategory { get; set; }

        public int? LocationID { get; set; }

        public RPG_Location Location { get; set; }

        public int? TeamID { get; set; }

        public RPG_Team Team { get; set; }
    }
}
