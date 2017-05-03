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
    public class ProductsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            int check = new SessionHelper().checkSession(Constants.USERS, Constants.ACTION_VIEW);
            if (check > 0)
            {
                JArray result = new JArray();
                ProductsModel model = new ProductsModel();
                var resultQuery = model.GetAll();
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

        public HttpResponseMessage GetById(int id)
        {
            int check = new SessionHelper().checkSession(Constants.USERS, Constants.ACTION_VIEW);
            if (check > 0)
            {
                ProductsModel model = new ProductsModel();
                var resultQuery = model.GetById(id);
                if (resultQuery != null)
                {
                    JObject result = JObject.FromObject(resultQuery);
                    var exception = JObject.FromObject(new { err = Constants.PROCESS_OK, data = result });
                    return Request.CreateResponse(HttpStatusCode.OK, exception);
                }
                else
                {
                    JObject exception = JObject.FromObject(new { err = Constants.PROCESS_FAILED, msg = Constants.PROCESS_FAILED });
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
        public HttpResponseMessage Post(Products value, int id)
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
                ProductsModel model = new ProductsModel();
                if (id == 1)
                    resultQuery = model.insert(value);
                else resultQuery = model.update(value);
                if (resultQuery)
                {
                    string message = (id == 1)?Constants.INSERT_SUCCESS:Constants.UPDATE_SUCCESS;
                    var exception = JObject.FromObject(new { err = Constants.PROCESS_OK, msg = message });
                    return Request.CreateResponse(HttpStatusCode.OK, exception);
                }
                else
                {
                    string message = (id == 1)?Constants.INSERT_FAIL:Constants.UPDATE_FAIL;
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

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            int check = new SessionHelper().checkSession(Constants.USERS, Constants.ACTION_DELETE);
            if (check > 0)
            {
                ProductsModel model = new ProductsModel();
                bool resultQuery = model.delete(id);
                if (resultQuery)
                {
                    JArray result = new JArray();
                    var data = model.GetAll();
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
