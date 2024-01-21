using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public struct ErrorData
    {
        public int code { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public string timestamp { get; set; }
    }
    public class ErrorModel 
    {
        public bool success { get; set; }
        public ErrorData error { get; set; }
    }
    public class ReceviedErrorModel
    {
        public ErrorData error { get; set; }
    }
    public class UniscoException : Exception
    {
        public ErrorData Error { get; set; }
        public UniscoException(string message, int code, int status)
        {
            Error = new ErrorData { code = code, message = message, status = status, timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff") };
        }
        public UniscoException(string message, int code)
        {
            Error = new ErrorData { code = code, message = message, status = 400, timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff") };
        }
        public UniscoException(string message)
        {
            Error = new ErrorData { message = message, status = 400, timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff") };
        }

        public ErrorModel GetErrorModel()
        {

            return new ErrorModel { error = Error, success = false };
        }
    }
    public struct SuccessModel
    {
        public bool success { get; set; }
        public object data { get; set; }

        public SuccessModel(object data)
        {
            this.data = data;
            this.success = true;
        }
    }
    public struct SuccessBooleanModel
    {
        public bool success { get; set; }
        public TrueResult data { get; set; }
        public SuccessBooleanModel(bool res)
        {
            this.data = new TrueResult { result = res };
            this.success = true;
        }
    }
    public class TrueResult
    {
        public bool result { get; set; }
    }
}
