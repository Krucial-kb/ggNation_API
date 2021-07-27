using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class PlayerCard
    {
        public int PlayerId { get; set; }
        public int PlayerCardUid { get; set; }
        public string PlayerName { get; set; }
        public string PlayerDisplayName { get; set; }
        public string PlayerEmail { get; set; }
        public string PlayerPassword { get; set; }
        public byte[] PlayerImage { get; set; }
    }
}
