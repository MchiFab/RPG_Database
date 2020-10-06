using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

/// <summary>
/// Class used as database context to modify the database in the main program
/// </summary>
namespace RPG_Database
{
    public class DB_Manager : DbContext
    {
        /// <summary>
        /// Parameter in base sets name of database
        /// </summary>
        public DB_Manager() : base("RPG_DB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DB_Manager>());
            Database.Initialize(false);
            Fill();
        }

        /// <summary>
        /// Can override default database built, used to customize joining tables
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RPG_Sorcerer>()
                .HasMany<RPG_Attack>(s => s.BaseAttacks)
                .WithMany(a => a.Sorcerers)
                .Map(sa =>
                {
                    sa.MapLeftKey("SorcererRefID");
                    sa.MapRightKey("AttackRefID");
                    sa.ToTable("RPG_SorcererAttack");
                });

            modelBuilder.Entity<RPG_Recruit>()
                .HasMany<RPG_Attack>(r => r.Attacks)
                .WithMany(a => a.Recruits)
                .Map(ra =>
                {
                    ra.MapLeftKey("RecruitRefID");
                    ra.MapRightKey("AttackRefID");
                    ra.ToTable("RPG_RecruitAttack");
                });

            modelBuilder.Entity<RPG_Team>()
                .HasMany<RPG_Recruit>(t => t.Recruits)
                .WithMany(r => r.Team)
                .Map(tr =>
                {
                    tr.MapLeftKey("TeamRefID");
                    tr.MapRightKey("RecruitRefID");
                    tr.ToTable("RPG_TeamRecruit");
                });
        }

        public DbSet<RPG_Attack> Attacks { get; set; }
        public DbSet<RPG_Location> Locations { get; set; }
        public DbSet<RPG_NPC> NPCs { get; set; }
        public DbSet<RPG_NPCCategory> NPCCategories { get; set; }
        public DbSet<RPG_Recruit> Recruits { get; set; }
        public DbSet<RPG_Sorcerer> Sorcerers { get; set; }
        public DbSet<RPG_Type> Types { get; set; }
        public DbSet<RPG_Team> Teams { get; set; }

        /// <summary>
        /// Takes data from fill.txt and adds it to the database,
        /// also performs the mapping via many-to-many relationship tables
        /// </summary>
        private void Fill()
        {
            string table = "";

            using (System.IO.StreamReader reader = new System.IO.StreamReader("Files/fill.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line[0] == '+')
                    {
                        table = line.Substring(1);
                        continue;
                    }

                    string[] entries = line.Split(';');

                    switch (table)
                    {
                        case "Attack":
                            RPG_Attack attack = new RPG_Attack()
                            {
                                Name = entries[0],
                                Damage = ParseNullableInt(entries[1]),
                                Accuracy = ParseNullableInt(entries[2]),
                                LearnLevel = ParseNullableInt(entries[3]),
                                TypeID = ParseNullableInt(entries[4])
                            };
                            this.Attacks.Add(attack);
                            this.SaveChanges();
                            break;
                        case "Location":
                            RPG_Location location = new RPG_Location()
                            {
                                Name = entries[0],
                                TilesetPath = entries[1]
                            };
                            this.Locations.Add(location);
                            this.SaveChanges();
                            break;
                        case "NPC":
                            RPG_NPC npc = new RPG_NPC()
                            {
                                PositionX = ParseNullableInt(entries[0]),
                                PositionY = ParseNullableInt(entries[1]),
                                SpritesheetPath = entries[2],
                                NPCCategoryID = ParseNullableInt(entries[3]),
                                LocationID = ParseNullableInt(entries[4]),
                                TeamID = ParseNullableInt(entries[5])
                            };
                            this.NPCs.Add(npc);
                            this.SaveChanges();
                            break;
                        case "NPCCategory":
                            RPG_NPCCategory cat = new RPG_NPCCategory()
                            {
                                CategoryName = entries[0]
                            };
                            this.NPCCategories.Add(cat);
                            this.SaveChanges();
                            break;
                        case "Recruit":
                            RPG_Recruit recruit = new RPG_Recruit()
                            {
                                Name = entries[0],
                                HP = ParseNullableInt(entries[1]),
                                Attack = ParseNullableInt(entries[2]),
                                Defense = ParseNullableInt(entries[3]),
                                Speed = ParseNullableInt(entries[4]),
                                Level = ParseNullableInt(entries[5]),
                                SorcererID = ParseNullableInt(entries[6])
                            };
                            this.Recruits.Add(recruit);
                            this.SaveChanges();
                            break;
                        case "Sorcerer":
                            RPG_Sorcerer sorcerer = new RPG_Sorcerer()
                            {
                                Name = entries[0],
                                BaseHP = ParseNullableInt(entries[1]),
                                BaseAttack = ParseNullableInt(entries[2]),
                                BaseDefense = ParseNullableInt(entries[3]),
                                BaseSpeed = ParseNullableInt(entries[4]),
                                Stage = ParseNullableInt(entries[5]),
                                EvolutionLevel = ParseNullableInt(entries[6]),
                                SpritesheetPath = entries[7],
                                TypeID = ParseNullableInt(entries[8]),
                                LocationID = ParseNullableInt(entries[9])
                            };
                            this.Sorcerers.Add(sorcerer);
                            this.SaveChanges();
                            break;
                        case "Team":
                            RPG_Team team = new RPG_Team()
                            {
                                Name = entries[0]
                            };
                            this.Teams.Add(team);
                            this.SaveChanges();
                            break;
                        case "Type":
                            RPG_Type type = new RPG_Type()
                            {
                                Name = entries[0]
                            };
                            this.Types.Add(type);
                            this.SaveChanges();
                            break;
                        case "RecruitAttack":
                            int recruitID = int.Parse(entries[0]);

                            RPG_Recruit r = (from search in this.Recruits
                                             where search.RecruitID == recruitID
                                             select search).FirstOrDefault();

                            int attackID = int.Parse(entries[1]);

                            RPG_Attack addAttack = (from search in this.Attacks
                                                where search.AttackID == attackID
                                                select search).FirstOrDefault();

                            r.Attacks.Add(addAttack);
                            this.SaveChanges();
                            break;
                        case "TeamRecruit":
                            int teamID = int.Parse(entries[0]);

                            RPG_Team t = (from search in this.Teams
                                          where search.TeamID == teamID
                                          select search).FirstOrDefault();

                            int recruitID2 = int.Parse(entries[1]);

                            RPG_Recruit addRecruit = (from search in this.Recruits
                                                      where search.RecruitID == recruitID2
                                                      select search).FirstOrDefault();

                            t.Recruits.Add(addRecruit);
                            this.SaveChanges();
                            break;
                    }
                }

                this.SaveChanges();
            }
        }

        private int? ParseNullableInt(string toParse)
        {
            int i;
            if (int.TryParse(toParse, out i))
                return i;
            return null;
        }
    }
}
