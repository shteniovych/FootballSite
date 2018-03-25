using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FC_B13.Models.DB
{
    public partial class Coach
    {
        public Coach()
        {
            TeamCoach = new HashSet<TeamCoach>();
        }

        public int CoachId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirhday { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PositionInTeam { get; set; }
        public int ContractId { get; set; }

        public Contract Contract { get; set; }
        public ICollection<TeamCoach> TeamCoach { get; set; }
        [NotMapped]
        public string Teams { get; set; }
    }
}
