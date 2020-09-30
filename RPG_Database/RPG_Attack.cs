using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPG_Database
{
    [Table("RPG_Attack")]
    public class RPG_Attack
    {
        public RPG_Attack()
        {
            this.Sorcerers = new List<RPG_Sorcerer>();
            this.Recruits = new List<RPG_Recruit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttackID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Damage { get; set; }

        [StringLength(1)]
        public string DamageClass { get; set; }

        public int? Accuracy { get; set; }

        public int? LearnLevel { get; set; }

        public int? TypeID { get; set; }

        public RPG_Type Type { get; set; }

        public List<RPG_Sorcerer> Sorcerers { get; set; }

        public List<RPG_Recruit> Recruits { get; set; }
    }
}
