using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PlayerCard
    {
        public int PlayerID { get; set; }
        public int PlayerCard_UID { get; set; }
        public string Player_Name { get; set; }
        public string Player_Display_Name { get; set; }
        public string Player_Email { get; set; }
        public string Player_Password { get; set; }
        public byte[] Player_Image { get; set; }
    }
}
