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
    public class TeamRepository : ITeamRepository
    {
        private const string TeamNotExistsInDatabaseErrorMessage =
            "Cannot edit team due team doesn't exists in database.";

        private readonly ApplicationContext _context;

        public TeamRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Team>> GetAllTeamsAsync() => await _context.Teams.Include(x => x.Players).ToListAsync();

        public async Task<Team> GetTeamByIdAsync(Guid? id) => await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Team team)
        {
            team.Id = Guid.NewGuid();
            _context.Add(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var team = await GetTeamByIdAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Team team, Guid id)
        {
            try
            {
                team.Id = id;
                _context.Update(team);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(team.Id))
                {
                    throw new ArgumentException(TeamNotExistsInDatabaseErrorMessage);
                }
                else
                {
                    throw;
                }
            }
        }

        private bool TeamExists(Guid id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
