using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPlayer_Card
    {
        //GET ALL
        Task<IEnumerable<Player>> GetAllAsync();
        //GET (ID) ASYNC
        Task<Player> GetByIDAsync(int PlayerID);
        //POST CALL
        void AddAsync(Player newPlayer);
        //PUT CALL
        Task<bool> UpdateAsync(Player updatePlayer, int id);
        //DELETE CALL
        void DeletePlayer(Player player);
        //Persists to Database
        Task<int> SaveChangesAsync();
        //From Db to Client (List)
        Task<IEnumerable<Player>> ToListAsync();

    }
}
