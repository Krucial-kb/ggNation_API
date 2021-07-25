using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using DataAccess.Models;

namespace DataAccess.Logic
{
    public static class Mapper
    {
        #region Player Card Map and UnMap
        // ! ***********************************
        // ! ********* Player Card *************
        // ! ***********************************
        public static Domain.Models.Player MapPlayerDomain(DataAccess.Models.Player player)
        {
            return new Domain.Models.Player
            {
                PlayerID = player.PlayerId,
                PlayerCard_UID = player.PlayerCardUid,
                Player_Name = player.PlayerName,
                Player_Display_Name = player.PlayerDisplayName,
                Player_Email = player.PlayerEmail,
                Player_Password = player.PlayerPassword,
                Player_Image = player.PlayerImage
            };
        }

        public static DataAccess.Models.Player MapPlayerData(Domain.Models.Player player)
        {
            return new Models.Player
            {
                PlayerId = player.PlayerID,
                PlayerCardUid = player.PlayerCard_UID,
                PlayerName = player.Player_Name,
                PlayerDisplayName = player.Player_Display_Name,
                PlayerEmail = player.Player_Email,
                PlayerPassword = player.Player_Password,
                PlayerImage = player.Player_Image
            };
        }

        #endregion
    }
}
