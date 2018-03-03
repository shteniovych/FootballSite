using System;
using System.Collections.Generic;

namespace FC_B13.Models.DB
{
    public partial class Profession
    {
        public Profession()
        {
            PersonalProfession = new HashSet<PersonalProfession>();
        }

        public int ProfessionId { get; set; }
        public string ProfessionName { get; set; }

        public ICollection<PersonalProfession> PersonalProfession { get; set; }
    }
}
