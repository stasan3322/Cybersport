using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cybersport.Infrastructure.Models;
using Cybersport.Services;

namespace Cybersport.Controllers
{
    [ApiController]
    [Route("TeamsApi")]
    public class TeamsController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamsController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        [Route("ViewAllTeams")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _teamService.GetAllTeamsAsync());
        }

        [HttpGet("TeamDetails/{id:guid}")]
        public async Task<IActionResult> Details([FromRoute] Guid? id)
        {
            bool result = await IsTeamValidAndExits(id);

            if (result)
            {
                return Ok(await _teamService.GetTeamByIdAsync(id));
            }
            return NotFound();
        }

        [HttpGet("CreateTeam")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost("CreateTeam")]
        public async Task<IActionResult> Create(Team team)
        {
            await _teamService.CreateAsync(team);
            return Ok(team);
        }

        [HttpGet("EditTeam/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            bool result = await IsTeamValidAndExits(id);

            if (result)
            {
                return Ok(await _teamService.GetTeamByIdAsync(id));
            }
            return NotFound();
        }

        [HttpPut("EditTeam/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, Team team)
        {
            await _teamService.EditAsync(team, id);
            return Ok(team);
        }

        [HttpGet("DeleteTeam/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            bool result = await IsTeamValidAndExits(id);

            if (result)
            {
                return Ok(await _teamService.GetTeamByIdAsync(id));
            }
            return NotFound();
        }

        [HttpDelete("DeleteTeam/{id:guid}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] Guid id)
        {
            await _teamService.DeleteAsync(id);
            return Ok($"Team with id = {id} was deleted succesfully");
        }

        private async Task<bool> IsTeamValidAndExits(Guid? id)
        {
            if (id == null ||
                await _teamService.GetTeamByIdAsync(id) == null)
            {
                return false;
            }

            return true;
        }
    }
}
