using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _1988216.MVC.Models;
using _1988216.MVC.Core;

namespace _1988216.MVC.Controllers
{
    public class C_Users
    {
        private Lib lib;
        private M_Users m_Users;

        public C_Users()
        {
            m_Users = new M_Users();
            lib = new Lib();
        }

        public bool Register(string username, string password)
        {
            // Kiểm tra tồn tại username??
            List<User> users = m_Users.GetUsers();
            foreach(User u in users)
            {
                if (u.Username == username)
                {
                    return false;
                }
            }

            return m_Users.Register(username, password);
        }

        public bool Login(string username, string password)
        {
            return m_Users.Login(username, password);
        }
    }
}