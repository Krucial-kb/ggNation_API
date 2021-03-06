using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Domain.Interfaces;
using DataAccess.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repos
{
    public class PlayerCard_repo : IPlayer_Card
    {
        #region Dependency Injection
        
        private readonly ggNationContext _context;
        private readonly ILogger<PlayerCard_repo> _logger;


        public PlayerCard_repo(ggNationContext _ctx, ILogger<PlayerCard_repo> logger)
        {
            _context = _ctx;
            _logger = logger;
        }
        #endregion
        public void PostPlayerAsync(Domain.Models.PlayerCard newPlayer)
        {
            var mappedPlayer = Mapper.MapPlayerDomain(newPlayer);
            _context.Set<DataAccess.Models.PlayerCard>().Add(mappedPlayer);
        }

        public void DeletePlayer(Domain.Models.PlayerCard player)
        {
            var mappedEntity = Mapper.MapPlayerDomain(player);
            _context.Set<DataAccess.Models.PlayerCard>().Remove(mappedEntity);
        }

        public async Task<IEnumerable<Domain.Models.PlayerCard>> GetAllAsync()
        {
            var players = await _context.PlayerCards.ToListAsync();

            var result = players.Select(Mapper.MapPlayerData);

            return result;
        }

        public async Task<Domain.Models.PlayerCard> GetByIDAsync(int PlayerID)
        {
            var player = await _context.PlayerCards.FirstOrDefaultAsync(u => u.PlayerId == PlayerID);
            if (player == null)
            {
                _logger.LogInformation($"User with id {PlayerID} not found.");
                return null;
            }
            _logger.LogInformation($"Fetched user with id {PlayerID}.");
            return Mapper.MapPlayerData(player);
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Models.PlayerCard>> ToListAsync()
        {
            return await GetAllAsync();
        }

        public async Task<bool> UpdateAsync(Domain.Models.PlayerCard updatePlayer, int id)
        {
            var mappedEmployee = Mapper.MapPlayerDomain(updatePlayer);
            /*_context.Entry(employee).State = EntityState.Modified;*/
            _context.Entry(mappedEmployee).State = EntityState.Modified;

            try
            {
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (mappedEmployee == null)
                {
                    return false;
                    // employee not found
                }
                else
                {
                    throw;
                }
            }
            return true;
            // it worked, so return true
        }




    }
}
