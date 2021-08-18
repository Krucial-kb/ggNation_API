using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;



namespace DataAccess.Logic
{
    public static class Mapper
    {
        #region PlayerCard Card Map and UnMap
        // ! ***********************************
        // ! ********* PlayerCard Card *********
        // ! ***********************************
        public static DataAccess.Models.PlayerCard MapPlayerDomain(Domain.Models.PlayerCard player)
        {
            return player is null ? null : new DataAccess.Models.PlayerCard
            {
                PlayerId = player.PlayerID,
                UniqueId = player.UniqueId,
                FirstName = player.FirstName,
                LastName = player.LastName,
                PlayerDisplayName = player.Player_Display_Name,
                PlayerEmail = player.Player_Email,
                PlayerPassword = player.Player_Password,
                PlayerImage = player.Player_Image
            };
        }

        public static Domain.Models.PlayerCard MapPlayerData(DataAccess.Models.PlayerCard player)
        {
            return player is null ? null : new Domain.Models.PlayerCard
            {
                PlayerID = player.PlayerId,
                UniqueId = player.UniqueId,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Player_Display_Name = player.PlayerDisplayName,
                Player_Email = player.PlayerEmail,
                Player_Password = player.PlayerPassword,
                Player_Image = player.PlayerImage
            };
        }

        #endregion
    }
}
