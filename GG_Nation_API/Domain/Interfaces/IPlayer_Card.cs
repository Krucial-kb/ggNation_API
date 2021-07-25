using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Inner_Models;

namespace Domain.Interfaces
{
    public interface IPlayer_Card
    {
        Task<List<Inner_PlayerCard>> GetAllPlayers(string search = null);
        Task<Inner_PlayerCard> GetPlayerByID(int PlayerID);
        Task<Inner_PlayerCard> CreateNewPlayer(Inner_PlayerCard newPlayer);
        Task UpdatePlayerCard(Inner_PlayerCard updatePlayer);
        Task DeletePlayer(int PlayerID);

    }
}
