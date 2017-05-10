namespace SimpleAgileBoard.Web.Models
{
    using System.Collections.Generic;
    public class BoardViewModel
    {
        public IEnumerable<TaskViewModel> ToDo{get;set;}
        public IEnumerable<TaskViewModel> InProgress{get;set;}
        public IEnumerable<TaskViewModel> Done{get;set;}
    }
}