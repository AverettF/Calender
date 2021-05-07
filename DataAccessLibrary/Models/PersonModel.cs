using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class PersonModel
    {
        //userid|username|hashPassword|Groupid
        public string userid { get; set; }
        public string username { get; set; }
        public string hashPassword { get; set; }
        public string Groupid { get; set; }
    }
}
