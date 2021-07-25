using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ggNationAPI.Transfer_Models;
using Domain.Inner_Models;

namespace Domain.Logic
{
    public class Mapper2
    {

        #region
        // ! ***********************************
        // ! ********* Player Card *************
        // ! ***********************************
        public static Transfer_PlayerCard MapPlayerCard(Inner_PlayerCard playerCard)
        {
            return new Transfer_PlayerCard
            {
                PlayerID = playerCard.PlayerID,
                PlayerCard_UID = playerCard.PlayerCard_UID,
                Player_Name = playerCard.Player_Name,
                Player_Display_Name = playerCard.Player_Display_Name,
                Player_Email = playerCard.Player_Email,
                Player_Password = playerCard.Player_Password,
                Player_Image = playerCard.Player_Image
            };
        }
        #endregion
    }
}
