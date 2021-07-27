using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class PlayerDetail
    {
        public int DetailsId { get; set; }
        public int? PlayerCardUid { get; set; }
        public int? MessagesId { get; set; }
        public int? PlayerRating { get; set; }
        public string PlayerPlatform { get; set; }
    }
}
