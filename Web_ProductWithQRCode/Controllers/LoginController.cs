using Web_ProductWithQRCode.Core.CustomModel;
using Web_ProductWithQRCode.Core.Helper;
using Web_ProductWithQRCode.Core.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Web_ProductWithQRCode.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PostToLogin(AccountLogin account)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            else
            {
                var model = new UsersModel();
                var resultLogin = model.Login(account.username, account.password);
                if (resultLogin.ErrorCode == 1)
                {
                    var accountSession = model.getByUsernamePassword(account.username, account.password);
                    accountSession.Username = account.username;
                    accountSession.Password = account.password;
                    HttpContext.Current.Session.Add(Constants.USER_SESSION, accountSession);
                }
                result = JObject.FromObject(resultLogin);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpPost]
        public HttpResponseMessage PostToChangePass([FromBody]JObject value)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            else
            {
                var model = new UsersModel();
                var resultLogin = model.changePass((string)value["username"], (string)value["oldpass"], (string)value["newpass"]);
                if (resultLogin.ErrorCode == 1)
                {
                    HttpContext.Current.Session[Constants.USER_SESSION] = null;
                    result = JObject.FromObject(resultLogin);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                result = JObject.FromObject(resultLogin);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpPost]
        public HttpResponseMessage PostToReset([FromBody]JObject value)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            else
            {
                var model = new UsersModel();
                var resultLogin = model.resetPass((string)value["username"], (string)value["birthday"]);
                if (resultLogin.ErrorCode == 1)
                {
                    HttpContext.Current.Session[Constants.USER_SESSION] = null;
                    result = JObject.FromObject(resultLogin);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                result = JObject.FromObject(resultLogin);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            else
            {
                var model = new UsersModel();
                var resultLogin = (AccountSession)HttpContext.Current.Session[Constants.USER_SESSION];
                if (resultLogin != null)
                {
                    result = JObject.FromObject(resultLogin);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    result = JObject.FromObject(new { err = -1, message = "Không có thông tin nhân viên đăng nhập" });
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
        }
    }
}
