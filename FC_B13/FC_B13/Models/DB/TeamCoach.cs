using System;
using System.Collections.Generic;

namespace FC_B13.Models.DB
{
    public partial class TeamCoach
    {
        public int TeamId { get; set; }
        public int CoachId { get; set; }

        public Coach Coach { get; set; }
        public Team Team { get; set; }
    }
}
