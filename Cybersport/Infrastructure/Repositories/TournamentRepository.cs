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
    public class TournamentRepository : ITournamentRepository
    {
        private const string TournamentNotExistsInDatabaseErrorMessage =
            "Cannot edit Tournament due Tournament doesn't exists in database.";

        private readonly ApplicationContext _context;

        public TournamentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Tournament>> GetAllTournamentsAsync() => await _context.Tournaments.Include(x => x.TeamWinner)
            .ToListAsync();

        public async Task<Tournament> GetTournamentByIdAsync(Guid? id) => await _context.Tournaments.Include(x => x.TeamWinner)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Tournament tournament)
        {
            tournament.Id = Guid.NewGuid();
            _context.Add(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tournament = await GetTournamentByIdAsync(id);
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Tournament tournament)
        {
            try
            {
                _context.Update(tournament);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(tournament.Id))
                {
                    throw new ArgumentException(TournamentNotExistsInDatabaseErrorMessage);
                }
                else
                {
                    throw;
                }
            }
        }

        private bool TournamentExists(Guid id)
        {
            return _context.Tournaments.Any(e => e.Id == id);
        }
    }
}
