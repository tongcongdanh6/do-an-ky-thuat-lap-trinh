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

        public DateTime convertStringToDateTimeWithOperator(string op, string str)
        {
            string timestamp = str.Replace(op, "");
            string[] splitTime = timestamp.Split('/');

            // Convert array of String to array of Int
            int[] splitIntTime = Array.ConvertAll(splitTime, int.Parse);

            return new DateTime(splitIntTime[2], splitIntTime[1], splitIntTime[0]);
        }

        public string simplifyText(string text, int limitLen)
        {
            string resText = String.Empty;
            if (text.Length > limitLen)
                resText = text.Substring(0, limitLen) + " ...";
            else
                resText = text;

            return resText;
        }
    }
}