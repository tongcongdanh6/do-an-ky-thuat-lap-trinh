using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1988216.MVC.Core
{
    public class Lib
    {
        public bool isValidUrl()
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