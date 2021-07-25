﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using DataAccess.Logic;
using Domain.Models;

namespace Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerCardController : ControllerBase
    {
        #region Depend Inject
        private readonly IPlayer_Card _playerRepository;
        private readonly ILogger<PlayerCardController> _logger;

        public PlayerCardController(IPlayer_Card playerRepository, ILogger<PlayerCardController> logger)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed PatientController");
        }
        #endregion

        // GET: api/<PlayerCardController>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetASync([FromQuery] string search = null)
        {
            return Ok();
        }

        // GET api/<PlayerCardController>/5
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Player>> GetByIdAsync(int id)
        {
            var playerByID = await _playerRepository.GetByIDAsync(id);

            if (playerByID == null)
            {
                return NotFound();
            }

            return Ok(playerByID);
        }

        // POST api/<PlayerCardController>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(int id, Player Player)
        {
            if (id != Player.PlayerID)
            {
                return BadRequest();
            }

            /*_context.Entry(customer).State = EntityState.Modified;*/
            if (!await _playerRepository.UpdateAsync(Player, id))
            {
                return NotFound();
                // if false, then modifying state failed
            }
            else
            {
                return NoContent();
                // successful put
            }
        }

        // PUT api/<PlayerCardController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutAsync(int id, Domain.Models.Player player)
        {
            if (id != player.PlayerID)
            {
                return BadRequest();
            }

            /*_context.Entry(employee).State = EntityState.Modified;*/
            if (!await _playerRepository.UpdateAsync(player, id))
            {
                return NotFound();
                // if false, then modifying state failed
            }
            else
            {
                return NoContent();
                // successful put
            }
        }

        // DELETE api/<PlayerCardController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerRepository.GetByIDAsync(id); // get this employee matching this id
                                                                         // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (player == null)
            {
                return NotFound();
            }

            _playerRepository.DeletePlayer(player);
            await _playerRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
