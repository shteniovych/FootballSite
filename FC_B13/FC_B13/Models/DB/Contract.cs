using System;
using System.Collections.Generic;

namespace FC_B13.Models.DB
{
    public partial class Contract
    {
        public Contract()
        {
            Coach = new HashSet<Coach>();
            Personal = new HashSet<Personal>();
            Player = new HashSet<Player>();
        }

        public int ContractId { get; set; }
        public int Salary { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ConclusionTime { get; set; }
        public DateTime StartTime { get; set; }
        public bool? InForseContract { get; set; }

        public ICollection<Coach> Coach { get; set; }
        public ICollection<Personal> Personal { get; set; }
        public ICollection<Player> Player { get; set; }
    }
}
