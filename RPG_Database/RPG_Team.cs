using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPG_Database
{
    [Table("RPG_Team")]
    public class RPG_Team
    {
        public RPG_Team()
        {
            this.Recruits = new List<RPG_Recruit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual List<RPG_Recruit> Recruits { get; set; }
    }
}
