using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FC_B13.Models.DB
{
    public partial class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirhday { get; set; }
        public string RoleOnTheField { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public long TransferPrise { get; set; }
        public string Citizenship { get; set; }
        public int Growth { get; set; }
        public int Weight { get; set; }
        public int TeamId { get; set; }
        public bool InForceContract { get; set; }
        public int ContractId { get; set; }

        public Contract Contract { get; set; }
        public Team Team { get; set; }
    }
}
