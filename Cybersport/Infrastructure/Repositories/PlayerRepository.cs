using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cybersport.Data;
using Cybersport.Infrastructure.Interfaces;
using Cybersport.Infrastructure.Models;

namespace Cybersport.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private const string PlayerNotExistsInDatabaseErrorMessage =
            "Cannot edit player due player doesn't exists in database.";

        private readonly ApplicationContext _context;

        public PlayerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Player>> GetAllPlayersAsync() => await _context.Players.Include(x => x.Team)
            .ToListAsync();

        public async Task<Player> GetPlayerByIdAsync(Guid? id) => await _context.Players.Include(x => x.Team)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Player player)
        {
            _context.Add(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var player = await GetPlayerByIdAsync(id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Player player, Guid id)
        {
            try
            {
                player.Id = id;
                _context.Players.Update(player);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(player.Id))
                {
                    throw new ArgumentException(PlayerNotExistsInDatabaseErrorMessage);
                }
                else
                {
                    throw;
                }
            }
        }

        private bool PlayerExists(Guid id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
