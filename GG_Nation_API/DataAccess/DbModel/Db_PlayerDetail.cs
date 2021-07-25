using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.DbModel
{
    public partial class Db_PlayerDetail
    {
        public int DetailsId { get; set; }
        public int? PlayerCardUid { get; set; }
        public int? MessagesId { get; set; }
        public int? PlayerRating { get; set; }
        public string PlayerPlatform { get; set; }
    }
}
