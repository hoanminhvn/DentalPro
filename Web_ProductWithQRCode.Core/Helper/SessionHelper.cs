using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ProductWithQRCode.Core.Models;
using System.Web;
using Web_ProductWithQRCode.Core.CustomModel;

namespace Web_ProductWithQRCode.Core.Helper
{
    public class SessionHelper
    {
        public int checkSession(int permission, int action)
        {
            var session = (AccountSession)HttpContext.Current.Session[Constants.USER_SESSION];
            if (session != null)
            {
                var model = new UsersModel();
                var resultLogin = model.Login(session.Username, session.Password);
                if (resultLogin.ErrorCode != 1) return Constants.PERMISSION_LOGIN_CODE;
                //else
                //{
                //    bool check_permiss = new BitField().check(session.ID, permission, action);
                //    if (check_permiss) return Constants.PROCESS_OK;
                //    return Constants.PERMISSION_DENIED_CODE;
                //}
                else return Constants.PROCESS_OK;
            }
            if (session == null)
            {
                return Constants.PERMISSION_LOGIN_CODE;
            }
            else return Constants.PROCESS_OK;
        }
    }
}
