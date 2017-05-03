using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Web_ProductWithQRCode.Core.Entity;
using Web_ProductWithQRCode.Core.Helper;
using Web_ProductWithQRCode.Core.Models;

namespace Demo20170404.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            int check = new SessionHelper().checkSession(Constants.USERS, Constants.ACTION_VIEW);
            if (check > 0)
            {
                JArray result = new JArray();
                UsersModel model = new UsersModel();
                var resultQuery = model.getAll();
                result = JArray.FromObject(resultQuery);
                var exception = JObject.FromObject(new { err = 1, data = result });
                return Request.CreateResponse(HttpStatusCode.OK, exception);
            }
            else
            {
                string message = (check == Constants.PERMISSION_LOGIN_CODE) ? Constants.PERMISSION_LOGIN_MSG : Constants.PERMISSION_DENIED_MSG;
                JObject exception = JObject.FromObject(new { err = check, msg = message });
                return Request.CreateResponse(HttpStatusCode.OK, exception);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetListGroupsUser()
        {
            var session = HttpContext.Current.Session[Constants.USER_SESSION];
            if (session == null)
            {
                var exception = JObject.FromObject(new { err = Constants.PERMISSION_DENIED_CODE, msg = Constants.PERMISSION_DENIED_MSG });
                return Request.CreateResponse(HttpStatusCode.OK, exception);
            }
            JArray result = new JArray();
            dynamic resultQuery = new JArray();
            UsersModel model = new UsersModel();
            resultQuery = model.getUserType();
            if (resultQuery != null)
            {
                result = JArray.FromObject(resultQuery);
                var exception = JObject.FromObject(new { err = Constants.PROCESS_OK, data = result });
                return Request.CreateResponse(HttpStatusCode.OK, exception);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage ResetPass([FromBody]JObject value)
        {
            JObject result = new JObject();
            dynamic resultQuery = new JObject();
            UsersModel model = new UsersModel();
            resultQuery = model.changePass((string)value["username"], (string)value["oldpass"], (string)value["newpass"]);
            if (resultQuery != null)
            {
                result = JArray.FromObject(resultQuery);
                var exception = JObject.FromObject(new { err = Constants.PROCESS_OK, data = result });
                return Request.CreateResponse(HttpStatusCode.OK, exception);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Post(Users value, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            int action = 0;
            if (id == 1) action = Constants.ACTION_ADD;
            else action = Constants.ACTION_EDIT;
            int check = new SessionHelper().checkSession(Constants.USERS, action);
            if (check > 0)
            {
                dynamic resultQuery = null;
                UsersModel model = new UsersModel();
                if (id == 1)
                    resultQuery = model.insert(value);
                else resultQuery = model.update(value);
                if (resultQuery.ErrorCode == 1)
                {
                    JArray result = new JArray();
                    var data = model.getAll();
                    result = JArray.FromObject(data);
                    var exception = JObject.FromObject(new { err = Constants.PROCESS_OK, data = result });
                    return Request.CreateResponse(HttpStatusCode.OK, exception);
                }
                else
                {
                    JObject exception = JObject.FromObject(new { err = Constants.PROCESS_FAILED, msg = resultQuery.ErrorMessage });
                    return Request.CreateResponse(HttpStatusCode.OK, exception);
                }
            }
            else
            {
                string message = (check == Constants.PERMISSION_LOGIN_CODE) ? Constants.PERMISSION_LOGIN_MSG : Constants.PERMISSION_DENIED_MSG;
                JObject exception = JObject.FromObject(new { err = check, msg = message });
                return Request.CreateResponse(HttpStatusCode.OK, exception);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            int check = new SessionHelper().checkSession(Constants.USERS, Constants.ACTION_DELETE);
            if (check > 0)
            {
                dynamic resultQuery = null;
                UsersModel model = new UsersModel();
                resultQuery = model.delete(id);
                if (resultQuery.ErrorCode == 1)
                {
                    JArray result = new JArray();
                    var data = model.getAll();
                    result = JArray.FromObject(data);
                    var exception = JObject.FromObject(new { err = Constants.PROCESS_OK, data = result });
                    return Request.CreateResponse(HttpStatusCode.OK, exception);
                }
                else
                {
                    JObject exception = JObject.FromObject(new { err = Constants.PROCESS_FAILED, msg = resultQuery.ErrorMessage });
                    return Request.CreateResponse(HttpStatusCode.OK, exception);
                }
            }
            else
            {
                string message = (check == Constants.PERMISSION_LOGIN_CODE) ? Constants.PERMISSION_LOGIN_MSG : Constants.PERMISSION_DENIED_MSG;
                JObject exception = JObject.FromObject(new { err = check, msg = message });
                return Request.CreateResponse(HttpStatusCode.OK, exception);
            }
        }
    }
}
