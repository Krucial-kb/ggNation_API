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

namespace DataAccess.Repos
{
    public class PlayerCard_repo : IPlayer_Card
    {
        #region Dependency Injection
        
        private readonly ggNationContext _context;
        

        public PlayerCard_repo(IServiceProvider service)
        {
            _context = service.CreateScope().ServiceProvider.GetRequiredService<ggNationContext>();
        }
        #endregion
        public void AddAsync(Domain.Models.PlayerCard newPlayer)
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
            var entities = await _context.Set<DataAccess.Models.PlayerCard>().ToListAsync();
            var mappedEntities = new List<Domain.Models.PlayerCard>();
            foreach (var entity in entities)
            {
                mappedEntities.Add(Mapper.MapPlayerData(entity));
            }
            return mappedEntities;
        }

        public async Task<Domain.Models.PlayerCard> GetByIDAsync(int PlayerID)
        {
            var data = await _context.Set<DataAccess.Models.PlayerCard>().FindAsync(PlayerID);

            return Mapper.MapPlayerData(data);
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
