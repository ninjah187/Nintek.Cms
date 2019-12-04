using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms.Users.Models
{
    public class LoginViewModel
    {
        public LoginViewModel(bool loginError)
        {
            LoginError = loginError;
        }

        public bool LoginError { get; }
    }
}
