using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cybersport.Infrastructure.Interfaces;
using Cybersport.Infrastructure.Models;

namespace Cybersport.Services
{
    public class TeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Team> GetTeamByIdAsync(Guid? id) => await _teamRepository.GetTeamByIdAsync(id);

        public async Task<ICollection<Team>> GetAllTeamsAsync() => await _teamRepository.GetAllTeamsAsync();

        public async Task CreateAsync(Team team) => await _teamRepository.CreateAsync(team);

        public async Task DeleteAsync(Guid id) => await _teamRepository.DeleteAsync(id);

        public async Task EditAsync(Team team, Guid id) => await _teamRepository.EditAsync(team, id);
    }
}
