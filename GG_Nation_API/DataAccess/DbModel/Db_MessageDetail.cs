using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.DbModel
{
    public partial class Db_MessageDetail
    {
        public int MessageDetailsId { get; set; }
        public int? MessagesId { get; set; }
        public int? MessageCount { get; set; }
        public string MessageDescription { get; set; }
        public DateTime? MessageDate { get; set; }
    }
}
