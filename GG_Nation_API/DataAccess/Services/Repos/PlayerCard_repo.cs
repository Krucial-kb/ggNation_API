using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;
using DataAccess.Models;
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
        public void AddAsync(Domain.Models.Player newPlayer)
        {
            var mappedPlayer = Mapper.MapPlayerData(newPlayer);
            _context.Set<DataAccess.Models.Player>().Add(mappedPlayer);
        }

        public void DeletePlayer(Domain.Models.Player player)
        {
            var mappedEntity = Mapper.MapPlayerData(player);
            _context.Set<DataAccess.Models.Player>().Remove(mappedEntity);
        }

        public async Task<IEnumerable<Domain.Models.Player>> GetAllAsync()
        {
            var entities = await _context.Set<DataAccess.Models.Player>().ToListAsync();
            var mappedEntities = new List<Domain.Models.Player>();
            foreach (var entity in entities)
            {
                mappedEntities.Add(Mapper.MapPlayerDomain(entity));
            }
            return mappedEntities;
        }

        public async Task<Domain.Models.Player> GetByIDAsync(int PlayerID)
        {
            var data = await _context.Set<DataAccess.Models.Player>().FindAsync(PlayerID);

            return Mapper.MapPlayerDomain(data);
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Models.Player>> ToListAsync()
        {
            return await GetAllAsync();
        }

        public async Task<bool> UpdateAsync(Domain.Models.Player updatePlayer, int id)
        {
            var mappedEmployee = Mapper.MapPlayerData(updatePlayer);
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
