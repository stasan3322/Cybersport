using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Cybersport.Infrastructure.Models;
using Cybersport.Services;

namespace Cybersport.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly TournamentService _tournamentService;
        private readonly TeamService _teamService;

        public TournamentsController(TournamentService tournamentService, TeamService teamService)
        {
            _tournamentService = tournamentService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(InitializeFinalDateTime(await _tournamentService.GetAllTournamentsAsync()));
        }

        [HttpGet("TournamentDetails/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            bool result = await IsTournamentValidAndExits(id);

            if (result)
            {
                return View(await _tournamentService.GetTournamentByIdAsync(id));
            }
            return NotFound();
        }

        [HttpGet("CreateTournament")]
        public async Task<IActionResult> Create()
        {
            await FillViewDataFieldWithoutTournament();
            return View();
        }

        [HttpPost("CreateTournament")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TournamentName,StartDate,EndDate,Organizer,HostCity,TeamWinnerId")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                await _tournamentService.CreateAsync(tournament);
                return RedirectToAction(nameof(Index));
            }
            await FillViewDataFieldWithTournament(tournament);
            return View(tournament);
        }

        [HttpGet("EditTournament/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            bool result = await IsTournamentValidAndExits(id);

            if (result)
            {
                var tournament = await _tournamentService.GetTournamentByIdAsync(id);
                await FillViewDataFieldWithTournament(tournament);
                return View(tournament);
            }
            return NotFound();
        }

        [HttpPost("EditTournament/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TournamentName,StartDate,EndDate,Organizer,HostCity,TeamWinnerId")] Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _tournamentService.EditAsync(tournament);
                return RedirectToAction(nameof(Index));
            }
            await FillViewDataFieldWithTournament(tournament);
            return View(tournament);
        }

        [HttpGet("DeleteTournament/{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            bool result = await IsTournamentValidAndExits(id);

            if (result)
            {
                return View(await _tournamentService.GetTournamentByIdAsync(id));
            }
            return NotFound();
        }

        [HttpPost("DeleteTournament/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _tournamentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IsTournamentValidAndExits(Guid? id)
        {
            if (id == null ||
                await _tournamentService.GetTournamentByIdAsync(id) == null)
            {
                return false;
            }

            return true;
        }

        private ICollection<Tournament> InitializeFinalDateTime(ICollection<Tournament> tournaments)
        {
            foreach (var item in tournaments)
            {
                if (item.StartDate.Year == item.EndDate.Year)
                {
                    if (item.StartDate.Month == item.EndDate.Month)
                    {
                        item.FinalDateTime = item.StartDate.Day.ToString() + " - " + item.EndDate.Date.ToString("d MMMM yyyy");
                    }
                    else
                    {
                        item.FinalDateTime = item.StartDate.Date.ToString("d MMMM") + " - " + item.EndDate.Date.ToString("d MMMM yyyy");
                    }
                }
                else
                {
                    item.FinalDateTime = item.StartDate.Date.ToString("d MMMM yyyy") + " - " + item.EndDate.Date.ToString("d MMMM yyyy");
                }
            }
            return tournaments;
        }

        private async Task FillViewDataFieldWithTournament(Tournament tournament)
        {
            ViewData["TeamWinnerId"] = new SelectList(await _teamService.GetAllTeamsAsync(), "Id", "TeamName", tournament.TeamWinnerId);
        }

        private async Task FillViewDataFieldWithoutTournament()
        {
            ViewData["TeamWinnerId"] = new SelectList(await _teamService.GetAllTeamsAsync(), "Id", "TeamName");
        }
    }
}
