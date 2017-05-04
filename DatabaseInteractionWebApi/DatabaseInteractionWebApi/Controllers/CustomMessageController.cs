using DatabaseAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseInteractionWebApi.Controllers
{
    public class CustomMessageController : ApiController
    {
        [HttpGet]       
        public HttpResponseMessage LoadAll(string forAllCMF = "All")
        {
            using (DatabaseAccessor accessor = new DatabaseAccessor())
            {
                switch(forAllCMF.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, accessor.tblCustomerMessages.ToList());
                    case "0":
                        return Request.CreateResponse(HttpStatusCode.OK, accessor.tblCustomerMessages.Where(m=>m.colIsForAllCMF.Value == 0).ToList());
                    case "1":
                        return Request.CreateResponse(HttpStatusCode.OK, accessor.tblCustomerMessages.Where(m => m.colIsForAllCMF.Value == 1).ToList());
                    default:                    
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please enter etiher all, 0 or 1");
                }                
            }
        }
        [HttpGet]
        public HttpResponseMessage LoadById(int id)
        {
            try
            {
                using (DatabaseAccessor accessor = new DatabaseAccessor())
                {
                    var customer = accessor.tblCustomerMessages.FirstOrDefault(m => m.ID == id);
                    if (customer != null)
                        return Request.CreateResponse(HttpStatusCode.OK, customer);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Message found with ID:" + id.ToString());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post([FromBody]tblCustomerMessage objMessage)
        {
            try
            {
                using (DatabaseAccessor accessor = new DatabaseAccessor())
                {
                    accessor.tblCustomerMessages.Add(objMessage);
                    accessor.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, objMessage);
                    message.Headers.Location = new Uri(Request.RequestUri + objMessage.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (DatabaseAccessor accessor = new DatabaseAccessor())
                {
                    var objMessage = accessor.tblCustomerMessages.FirstOrDefault(m => m.ID == id);
                    if (objMessage != null)
                    {
                        accessor.tblCustomerMessages.Remove(objMessage);
                        accessor.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Message with Id: " + id.ToString() + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Message with Id: " + id.ToString() + " Not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put([FromUri]int id,[FromBody] tblCustomerMessage message)
        {
            try
            {
                using (DatabaseAccessor accessor = new DatabaseAccessor())
                {
                    var objTempMessage = accessor.tblCustomerMessages.FirstOrDefault(m => m.ID == id);
                    if(objTempMessage == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Message with id: " + id + " Not Found");
                    }
                    else
                    {
                        objTempMessage.colEffectiveDate = message.colEffectiveDate;
                        objTempMessage.colEndDate = message.colEndDate;
                        objTempMessage.colFKDMVId = message.colFKDMVId;
                        objTempMessage.colIsForAllCMF = message.colIsForAllCMF;
                        objTempMessage.colMessage = message.colMessage;
                        objTempMessage.CreateDate = message.CreateDate;
                        objTempMessage.LastUpdateDate = message.LastUpdateDate;
                        objTempMessage.IsDeleted = message.IsDeleted;

                        accessor.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Message with id:" + id + " Updated");
                    }

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
