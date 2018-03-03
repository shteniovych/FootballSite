using System;
using System.Collections.Generic;

namespace FC_B13.Models.DB
{
    public partial class Personal
    {
        public Personal()
        {
            PersonalProfession = new HashSet<PersonalProfession>();
        }

        public int PersonalId { get; set; }
        public string Name { get; set; }
        public DateTime DataOfBirthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int ProfessionId { get; set; }
        public bool InForceContract { get; set; }
        public int ContractId { get; set; }
        public string IsManager { get; set; }

        public Contract Contract { get; set; }
        public ICollection<PersonalProfession> PersonalProfession { get; set; }
    }
}
