using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ProductWithQRCode.Core.CustomModel
{
    public class AccountSession
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int GroupID { get; set; }
        public int Status { get; set; }
    }
}
