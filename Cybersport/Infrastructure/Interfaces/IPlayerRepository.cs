using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cybersport.Infrastructure.Models;

namespace Cybersport.Infrastructure.Interfaces
{
    public interface IPlayerRepository
    {
        public Task<ICollection<Player>> GetAllPlayersAsync();
        public Task<Player> GetPlayerByIdAsync(Guid? id);
        public Task CreateAsync(Player player);
        public Task EditAsync(Player player, Guid id);
        public Task DeleteAsync(Guid id);
    }
}
