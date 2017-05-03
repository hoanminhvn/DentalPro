using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ProductWithQRCode.Core.Entity;

namespace Web_ProductWithQRCode.Core.Helper
{
    public class BitField
    {
        public bool check(int _maTaiKhoan, int permission, int action)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                var valuePermiss = db.Permissions.SingleOrDefault(x => x.GroupID == _maTaiKhoan && x.FunctionalID == permission);
                if (valuePermiss != null)
                {
                    if (valuePermiss.ValueRights > 0 && action > 0)
                        return ((valuePermiss.ValueRights & action) > 0);
                    return false;
                }  
                return false;
            }
        }
    }
}
