using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cybersport.Infrastructure.Interfaces;
using Cybersport.Infrastructure.Models;

namespace Cybersport.Services
{
    public class TournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentService(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<Tournament> GetTournamentByIdAsync(Guid? id) => await _tournamentRepository.GetTournamentByIdAsync(id);

        public async Task<ICollection<Tournament>> GetAllTournamentsAsync() => await _tournamentRepository.GetAllTournamentsAsync();

        public async Task CreateAsync(Tournament tournament) => await _tournamentRepository.CreateAsync(tournament);

        public async Task DeleteAsync(Guid id) => await _tournamentRepository.DeleteAsync(id);

        public async Task EditAsync(Tournament tournament) => await _tournamentRepository.EditAsync(tournament);
    }
}
