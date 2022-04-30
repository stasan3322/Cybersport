using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cybersport.Infrastructure.Models;

namespace Cybersport.Infrastructure.Interfaces
{
    public interface ITournamentRepository
    {
        public Task<ICollection<Tournament>> GetAllTournamentsAsync();
        public Task<Tournament> GetTournamentByIdAsync(Guid? id);
        public Task CreateAsync(Tournament tournament);
        public Task EditAsync(Tournament tournament);
        public Task DeleteAsync(Guid id);
    }
}
