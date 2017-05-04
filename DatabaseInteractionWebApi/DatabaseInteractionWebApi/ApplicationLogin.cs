using DatabaseAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseInteractionWebApi
{
    public class ApplicationLogin
    {
        public static bool Login(string username, string password)
        {
            using (DatabaseAccessor accessor = new DatabaseAccessor())
            {
                return accessor.tblAuthetications.Any(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}