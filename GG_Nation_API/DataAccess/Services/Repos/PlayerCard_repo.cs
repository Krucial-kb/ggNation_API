using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Inner_Models;
using Domain.Interfaces;
using DataAccess.DbModel;
using DataAccess.Logic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos
{
    public class PlayerCard_repo : IPlayer_Card
    {
        #region Dependency Injection
        
        private readonly ggNationContext _context;

        public PlayerCard_repo(ggNationContext context)
        {
            this._context = context;
        }

        #endregion

        /// <summary>
        /// Creates new player/adds to the DB
        /// </summary>
        /// <param name="newPlayer"></param>
        /// <returns></returns>
        public async Task<Inner_PlayerCard> CreateNewPlayer(Inner_PlayerCard newPlayer)
        {

            var newPlayerCard = Mapper.UnMapPlayerCard(newPlayer);
            _context.PlayerCards.Add(newPlayerCard);
            await Save();

            return Mapper.MapPlayerCard(newPlayerCard);
        }

        /// <summary>
        /// Deletes player from the DB
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns></returns>
        public async Task DeletePlayer(int PlayerID)
        {
            //Finds the selected player to stage for deletion.
            var Player = await _context.PlayerCards.FindAsync(PlayerID);

            if (Player is null)
            {
                return;
            }

            //Removes Player from DB
            _context.PlayerCards.Remove(Player);
            await Save();
        }

        /// <summary>
        /// Returns all the players in the DB by name.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<List<Inner_PlayerCard>> GetAllPlayers(string search = null)
        {
            var allPlayers = await _context.PlayerCards.ToListAsync();

            if (search == null)
            {
                return allPlayers.Select(Mapper.MapPlayerCard).ToList();
            }
                            return (allPlayers.FindAll(p =>
                   p.PlayerName.ToLower().Contains(search.ToLower())
                  )).Select(Mapper.MapPlayerCard).ToList();
        }

        /// <summary>
        /// Returns the player by ID or by search.ke
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns></returns>
        public async Task<Inner_PlayerCard> GetPlayerByID(int PlayerID)
        {
            var playerByID = await _context.PlayerCards.FirstOrDefaultAsync(a => a.PlayerId == PlayerID);

            return Mapper.MapPlayerCard(playerByID);
        }

        public async Task UpdatePlayerCard(Inner_PlayerCard updatePlayer)
        {
            Db_PlayerCard currEntity = await _context.PlayerCards.FindAsync(updatePlayer.PlayerID);
            Db_PlayerCard newEntity = Mapper.UnMapPlayerCard(updatePlayer);

            _context.Entry(currEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
