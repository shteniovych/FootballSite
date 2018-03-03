using System;
using System.Collections.Generic;

namespace FC_B13.Models.DB
{
    public partial class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirhday { get; set; }
        public string RoleOnTheField { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Citizenship { get; set; }
        public int Growth { get; set; }
        public int Weight { get; set; }
        public int TeamId { get; set; }
        public int ContractId { get; set; }

        public Contract Contract { get; set; }
        public Team Team { get; set; }
    }
}
