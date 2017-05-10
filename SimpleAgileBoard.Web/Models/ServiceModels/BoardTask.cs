using System;

namespace SimpleAgileBoard.Web.Models.ServiceModels
{
    public class BoardTask
    {
        public Guid Id{get;set;}
        public string Name{get;set;}
        public BoardTaskStatus Status{get;set;}
    }
}