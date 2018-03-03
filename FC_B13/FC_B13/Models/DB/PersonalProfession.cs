using System;
using System.Collections.Generic;

namespace FC_B13.Models.DB
{
    public partial class PersonalProfession
    {
        public int PersonalId { get; set; }
        public int ProfessionId { get; set; }

        public Personal Personal { get; set; }
        public Profession Profession { get; set; }
    }
}
