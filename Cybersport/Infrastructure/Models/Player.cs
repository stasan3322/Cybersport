using System;

using Cybersport.Infrastructure.Enums;

namespace Cybersport.Infrastructure.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Country Country { get; set; }

        public Guid TeamId { get; set; }
        public Team Team { get; set; }
    }
}
