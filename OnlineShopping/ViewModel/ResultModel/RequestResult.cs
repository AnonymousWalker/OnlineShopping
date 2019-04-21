using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.ViewModel.ResultModel
{
    public class RequestResult
    {
        public ResultCode ResultCode { get; set; } = ResultCode.Success;
        public bool HasError { get; set; } = false;
        public string Message { get; set; } = "";
        public string DataString { get; set; }
    }
}