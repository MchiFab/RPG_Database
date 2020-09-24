﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPG_Database
{
    [Table("RPG_Recruit")]
    public class RPG_Recruit
    {
        public RPG_Recruit()
        {
            this.Attacks = new List<RPG_Attack>();
            this.Team = new List<RPG_Team>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecruitID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? HP { get; set; }

        public int? PAttack { get; set; }

        public int? SAttack { get; set; }

        public int? PDefense { get; set; }

        public int? SDefense { get; set; }

        public int? Speed { get; set; }

        public int? Level { get; set; }

        //public virtual List<RPG_Recruit> Recruits { get; set; }

        public int? SorcererID { get; set; }

        public RPG_Sorcerer Sorcerer { get; set; }

        public virtual List<RPG_Attack> Attacks { get; set; }

        public virtual List<RPG_Team> Team { get; set; }
    }
}