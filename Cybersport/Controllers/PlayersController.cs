using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cybersport.Infrastructure.Models;
using Cybersport.Services;
using System.Web.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ActionNameAttribute = Microsoft.AspNetCore.Mvc.ActionNameAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;

namespace Cybersport.Controllers
{
    [ApiController]
    [Route("PlayersApi")]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayersController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        [Route("ViewAllPlayers")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _playerService.GetAllPlayersAsync());
        }

        [HttpGet("PlayerDetails/{id:guid}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            bool result = await IsPlayerValidAndExits(id);

            if (result)
            {
                var player = await _playerService.GetPlayerByIdAsync(id);
                return Ok(player);
            }
            return BadRequest();
        }

        [HttpGet("CreatePlayer")]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        [HttpPost("CreatePlayer")]
        public async Task<IActionResult> Create(Player player)
        {
            await _playerService.CreateAsync(player);
            return Ok(player);
        }

        [HttpGet("EditPlayer/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            bool result = await IsPlayerValidAndExits(id);

            if (result)
            {
                var player = await _playerService.GetPlayerByIdAsync(id);
                return Ok(player);
            }
            return NotFound();
        }

        [HttpPut("EditPlayer/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, Player player)
        {
            await _playerService.EditAsync(player, id);
            return Ok(player);
        }

        [HttpGet("DeletePlayer/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            bool result = await IsPlayerValidAndExits(id);

            if (result)
            {
                return Ok(await _playerService.GetPlayerByIdAsync(id));
            }
            return BadRequest();
        }

        [HttpDelete("DeletePlayer/{id:guid}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] Guid id)
        {
            await _playerService.DeleteAsync(id);
            return Ok($"User with id {id} was successfully deleted.");
        }

        private async Task<bool> IsPlayerValidAndExits(Guid? id)
        {
            if (id == null ||
                await _playerService.GetPlayerByIdAsync(id) == null)
            {
                return false;
            }
            
            return true;
        }
    }
}
