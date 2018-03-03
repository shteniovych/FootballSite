using System;
using System.Collections.Generic;

namespace FC_B13.Models.DB
{
    public partial class Team
    {
        public Team()
        {
            Inventar = new HashSet<Inventar>();
            Player = new HashSet<Player>();
            TeamCoach = new HashSet<TeamCoach>();
        }

        public int TeamId { get; set; }
        public string NameTeam { get; set; }

        public ICollection<Inventar> Inventar { get; set; }
        public ICollection<Player> Player { get; set; }
        public ICollection<TeamCoach> TeamCoach { get; set; }
    }
}
