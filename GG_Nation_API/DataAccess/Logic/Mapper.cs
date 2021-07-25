using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Inner_Models;
using DataAccess.DbModel;

namespace DataAccess.Logic
{
    public static class Mapper
    {
        #region Player Card Map and UnMap
        // ! ***********************************
        // ! ********* Player Card *************
        // ! ***********************************
        public static Inner_PlayerCard MapPlayerCard(Db_PlayerCard playerCard)
        {
            return new Inner_PlayerCard
            {
                PlayerID = playerCard.PlayerId,
                PlayerCard_UID = playerCard.PlayerCardUid,
                Player_Name = playerCard.PlayerName,
                Player_Display_Name = playerCard.PlayerDisplayName,
                Player_Email = playerCard.PlayerEmail,
                Player_Password = playerCard.PlayerPassword,
                Player_Image = playerCard.PlayerImage
            };
        }

        public static Db_PlayerCard UnMapPlayerCard(Inner_PlayerCard playerCard)
        {
            return new Db_PlayerCard
            {
                PlayerId = playerCard.PlayerID,
                PlayerCardUid = playerCard.PlayerCard_UID,
                PlayerName = playerCard.Player_Name,
                PlayerDisplayName = playerCard.Player_Display_Name,
                PlayerEmail = playerCard.Player_Email,
                PlayerPassword = playerCard.Player_Password,
                PlayerImage = playerCard.Player_Image
            };
        }

        #endregion
    }
}
