using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ProductWithQRCode.Core.CustomModel
{
    public class AccountLogin
    {
        public string username { get; set; }
        public string password { get; set; }
        public int remember { get; set; }
    }
}
