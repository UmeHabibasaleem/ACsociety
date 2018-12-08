using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocietyOfAC.Models
{
    public class RequestClass
    {
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Team { get; set; }
    }
    public class HoldingClass
    {

        public List<RequestClass> RQLIST = new List<RequestClass>();

        public List<RequestClass> RQLIST1
        {
            get
            {
                return RQLIST;
            }

            set
            {
                RQLIST = value;
            }
        }
    }
    public class Logout
    {
        public static int logout { get; set; }
    }
}