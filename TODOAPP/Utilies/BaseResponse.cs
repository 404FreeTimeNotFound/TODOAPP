using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOAPP.Utilies
{
    public class BaseResponse
    {
        public bool IsSuccess {get;set;}
        public object Data {get;set;}
        public List<string> Errors{get;set;}=new List<string>();

        public BaseResponse(object data= null!, bool isSuccess=true,List<string> errors= null!)
        {
            Data = data;
            IsSuccess = isSuccess;
            Errors = errors ?? new List<string>();
        }
    }
}