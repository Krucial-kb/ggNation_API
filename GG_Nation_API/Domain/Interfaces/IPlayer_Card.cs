using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess.Interfaces
{
    public interface IPlayer_Card
    {
        //GET ALL
        Task<IEnumerable<PlayerCard>> GetAllAsync();
        //GET (ID) ASYNC
        Task<PlayerCard> GetByIDAsync(int PlayerID);
        //POST CALL
        void AddAsync(PlayerCard newPlayer);
        //PUT CALL
        Task<bool> UpdateAsync(PlayerCard updatePlayer, int id);
        //DELETE CALL
        void DeletePlayer(PlayerCard player);
        //Persists to Database
        Task<int> SaveChangesAsync();
        //From Db to Client (List)
        Task<IEnumerable<PlayerCard>> ToListAsync();

    }
}
