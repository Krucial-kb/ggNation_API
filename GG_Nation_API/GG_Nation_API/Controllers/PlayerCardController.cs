using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Inner_Models;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using ggNationAPI.Transfer_Models;
using Microsoft.AspNetCore.Http;
using DataAccess.Logic;
using Domain.Logic;


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
        [ProducesResponseType(typeof(List<Transfer_PlayerCard>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetASync([FromQuery] string search = null)
        {
            List<Transfer_PlayerCard> playerAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all Players...");
                playerAll = (await _playerRepository.GetAllPlayers()).Select(Mapper2.MapPlayerCard).ToList();
            }
            else
            {
                _logger.LogInformation($"Retriveing Players with the name {search}...");
                playerAll = (await _playerRepository.GetAllPlayers(search)).Select(Mapper2.MapPlayerCard).ToList();
            }
            try 
            {
                _logger.LogInformation($"Sending {playerAll.Count} Players...");
                return Ok(playerAll);
            }
            catch(Exception e)
            {
                _logger.LogWarning($"Error! {e.Message}.");
                return StatusCode(500);
            }
        }

        // GET api/<PlayerCardController>/5
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_PlayerCard), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_PlayerCard>> GetByIdAsync(int id)
        {
            var currentUser = HttpContext.User;
            _logger.LogInformation($"Retrieving Players with id {id}.");
            if (await _playerRepository.GetPlayerByID(id) is Inner_PlayerCard player)
            {
                var transformedPatient = Mapper2.MapPlayerCard(player);
                return Ok(transformedPatient);
            }
            _logger.LogInformation($"No Players found with id {id}.");
            return NotFound();
        }

        // POST api/<PlayerCardController>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_PlayerCard), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(Transfer_PlayerCard Player)
        {
            try
            {
                _logger.LogInformation($"Adding new patient.");
                Inner_PlayerCard transformedPlayer = new Inner_PlayerCard
                {
                    PlayerID = Player.PlayerID,
                    PlayerCard_UID = Player.PlayerCard_UID,
                    Player_Name = Player.Player_Name,
                    Player_Display_Name = Player.Player_Display_Name,
                    Player_Email = Player.Player_Email,
                    Player_Password = Player.Player_Password,
                    Player_Image = Player.Player_Image
                };
                await _playerRepository.CreateNewPlayer(transformedPlayer);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = Player.PlayerID}, Player);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PlayerCardController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Transfer_PlayerCard player)
        {
            _logger.LogInformation($"Editing Player with id {id}.");
            var entity = await _playerRepository.GetPlayerByID(id);
            if (entity is Inner_PlayerCard)
            {
                entity.PlayerID = player.PlayerID;
                entity.PlayerCard_UID = player.PlayerCard_UID;
                entity.Player_Name = player.Player_Name;
                entity.Player_Display_Name = player.Player_Display_Name;
                entity.Player_Email = player.Player_Email;
                entity.Player_Password = player.Player_Password;
                entity.Player_Image = player.Player_Image;


                await _playerRepository.UpdatePlayerCard(entity);
                return NoContent();
            }
            _logger.LogInformation($"No patients found with id {id}.");
            return NotFound();
        }

        // DELETE api/<PlayerCardController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting Player with id {id}.");
            if (await _playerRepository.GetPlayerByID(id) is Inner_PlayerCard player)
            {
                await _playerRepository.DeletePlayer(player.PlayerID);
                return NoContent();
            }
            _logger.LogInformation($"No Player found with id {id}.");
            return NotFound();
        }
    }
}
