using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Cybersport.Infrastructure.Enums;

namespace Cybersport.Infrastructure.Models
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public string TournamentName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [NotMapped]
        public string FinalDateTime { get; set; }
        public Organizer Organizer { get; set; }
        public string HostCity { get; set; }

        public Guid TeamWinnerId { get; set; }
        public Team TeamWinner { get; set; }
    }
}
