using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cybersport.Infrastructure.Interfaces;
using Cybersport.Infrastructure.Models;

namespace Cybersport.Services
{
    public class PlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Player> GetPlayerByIdAsync(Guid? id) => await _playerRepository.GetPlayerByIdAsync(id);

        public async Task<ICollection<Player>> GetAllPlayersAsync() => await _playerRepository.GetAllPlayersAsync();

        public async Task CreateAsync(Player player) => await _playerRepository.CreateAsync(player);

        public async Task DeleteAsync(Guid id) => await _playerRepository.DeleteAsync(id);

        public async Task EditAsync(Player player, Guid id) => await _playerRepository.EditAsync(player, id);
    }
}
