﻿using System.Net;

namespace APIWALKIM.Models
{
    public class BaseResponseModel
    {
        public string message;
        public HttpStatusCode httpStatus;
        //public enum HttpStatusCode; 


        public BaseResponseModel(string message, HttpStatusCode httpStatus)
        {
            this.message = message;
            this.httpStatus = httpStatus;
        }

        public BaseResponseModel()
        {
            
        }
    }
}
