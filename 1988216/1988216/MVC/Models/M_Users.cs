using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace _1988216.MVC.Models
{
    public class M_Users
    {

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/UsersData.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                r.Close();
                dynamic data = JsonConvert.DeserializeObject(json);
                foreach (var jitem in data)
                {
                    User u = new User();
                    u.Username = jitem["Username"];
                    u.Password = jitem["Password"];

                    users.Add(u);
                }
                return users;
            }
        }
        public bool Register(string username, string password)
        {
            List<User> users = GetUsers();

            User newUser = new User();
            newUser.Username = username;
            newUser.Password = password;

            users.Add(newUser);


            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(users.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/UsersData.json");

                StreamWriter file = new StreamWriter(filePath);
                file.WriteLine(json);
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Login(string username, string password)
        {
            List<User> users = GetUsers();
            User u = users.Find(user => user.Username == username);
            if(u == null)
            {
                return false;
            }
            else
            {
                if(u.Password == password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}