using System;
using System.Collections.Generic;
using B.Framework.Domain.Shared.User;

namespace B.Framework.Domain.User
{
    public interface IUserService
    {
         Tuple<string, bool> Authenticate(string username, string password);
         

       

    }
}