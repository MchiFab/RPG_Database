using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPG_Database
{
    [Table("RPG_Sorcerer")]
    public class RPG_Sorcerer
    {
        public RPG_Sorcerer()
        {
            this.BaseAttacks = new List<RPG_Attack>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SorcererID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? BaseHP { get; set; }

        public int? BasePAttack { get; set; }

        public int? BaseSAttack { get; set; }

        public int? BasePDefense { get; set; }

        public int? BaseSDefense { get; set; }

        public int? BaseSpeed { get; set; }

        public int? Stage { get; set; }

        public int? EvolutionLevel { get; set; }

        public string SpritesheetPath { get; set; }

        public int? TypeID { get; set; }

        public RPG_Type Type { get; set; }

        public List<RPG_Attack> BaseAttacks { get; set; }

        public int? LocationID { get; set; }

        public RPG_Location Location { get; set; }
    }
}
