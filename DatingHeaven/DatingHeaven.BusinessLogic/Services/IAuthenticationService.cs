using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.BusinessLogic.ServicesImplementation.Auth;

namespace DatingHeaven.BusinessLogic.Services {
    public interface IAuthenticationService{
        LogInResult LogInViaNickName(string nickName, string password);

        LogInResult LogInViaEmail(string email, string password);

        void LogOut();
    }
}
