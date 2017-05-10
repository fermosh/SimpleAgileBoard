using System;
using SimpleAgileBoard.Web.Models.ServiceModels;

namespace SimpleAgileBoard.Web.Models.DataModels
{
    public class BoardTask
    {
        public Guid Id{get;set;}
        public string Name{get;set;}
        public BoardTaskStatus Status{get;set;}
    }
}