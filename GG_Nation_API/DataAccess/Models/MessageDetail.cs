using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class MessageDetail
    {
        public int MessageDetailsId { get; set; }
        public int? MessagesId { get; set; }
        public int? MessageCount { get; set; }
        public string MessageDescription { get; set; }
        public DateTime? MessageDate { get; set; }
    }
}
