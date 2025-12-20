using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOAPP.Models
{
    public class ItemData
    {
        public int id{get;set;}
        public string title{get;set;}=string.Empty;
        public string description{get;set;}=string.Empty;
        public bool done{get;set;}
        

    }
}