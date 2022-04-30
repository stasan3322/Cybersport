using System;
using System.Collections.Generic;

namespace Cybersport.Infrastructure.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
