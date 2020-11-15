using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1988216.MVC.Controllers
{
    public class C_home
    {
        public bool check()
        {
            return false;
        }

        public bool checkUrl()
        {
            bool valid = true;
            if (HttpContext.Current.Request.Url.AbsolutePath != "/")
            {
                valid = false;
            }

            return valid;
        }
    }
}