using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_ProductWithQRCode.Core.Entity;
using Web_ProductWithQRCode.Core.Helper;
using Web_ProductWithQRCode.Core.Models;

namespace Web_ProductWithQRCode.Controllers.Admin
{
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            int check = new SessionHelper().checkSession(Constants.USERS, Constants.ACTION_VIEW);
            if (check > 0)
            {
                JArray result = new JArray();
                CategoriesModel model = new CategoriesModel();
                var resultQuery = model.GetAll(id);
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

        [HttpPost]
        public HttpResponseMessage Post(Categories value, int id)
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
                bool resultQuery = false;
                CategoriesModel model = new CategoriesModel();
                if (id == 1)
                    resultQuery = model.Insert(value);
                else resultQuery = model.Update(value);
                if (resultQuery)
                {
                    JArray result = new JArray();
                    var data = model.GetAll((int)value.Type);
                    result = JArray.FromObject(data);
                    var exception = JObject.FromObject(new { err = Constants.PROCESS_OK, data = result });
                    return Request.CreateResponse(HttpStatusCode.OK, exception);
                }
                else
                {
                    string message = (id == 1)?Constants.INSERT_FAIL : Constants.UPDATE_FAIL;
                    JObject exception = JObject.FromObject(new { err = Constants.PROCESS_FAILED, msg = message });
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

        [HttpPost]
        public HttpResponseMessage UpdateMultiRecords([FromBody]JObject value)
        {
            int action = Constants.ACTION_EDIT;
            int check = new SessionHelper().checkSession(Constants.USERS, action);
            if (check > 0)
            {
                CategoriesModel model = new CategoriesModel();
                JArray list = (JArray)value["list"];
                int[] idList = new int[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    idList[i] = (int)list[i];
                }

                bool resultQuery = model.UpdateMultiRecords(idList);
                if (resultQuery)
                {
                    JArray result = new JArray();
                    var data = model.GetAll((int)value["type"]);
                    result = JArray.FromObject(data);
                    var exception = JObject.FromObject(new { err = Constants.PROCESS_OK, data = result });
                    return Request.CreateResponse(HttpStatusCode.OK, exception);
                }
                else
                {
                    JObject exception = JObject.FromObject(new { err = Constants.PROCESS_FAILED, msg = Constants.UPDATE_FAIL });
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
        public HttpResponseMessage Delete(int id)
        {
            int check = new SessionHelper().checkSession(Constants.USERS, Constants.ACTION_DELETE);
            if (check > 0)
            {
                CategoriesModel model = new CategoriesModel();
                int resultQuery = model.Delete(id);
                if (resultQuery != Constants.PROCESS_FAILED)
                {
                    JArray result = new JArray();
                    var data = model.GetAll(resultQuery);
                    result = JArray.FromObject(data);
                    var exception = JObject.FromObject(new { err = Constants.PROCESS_OK, data = result });
                    return Request.CreateResponse(HttpStatusCode.OK, exception);
                }
                else
                {
                    JObject exception = JObject.FromObject(new { err = Constants.PROCESS_FAILED, msg = Constants.DELETE_FAIL });
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
