using System;
using System.Collections.Generic;

namespace FC_B13.Models.DB
{
    public partial class Inventar
    {
        public int InventarId { get; set; }
        public string InventarName { get; set; }
        public int Count { get; set; }
        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
