using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Group_Project.Models
{
    public class Question
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public List<string> answers { get; set; }
    }
}