using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanarkshireGamers.Models.View;
using LanarkshireGamersData;
using LanarkshireGamersData.Model;

namespace LanarkshireGamesBusinessLogic
{
    public class UserBusinessLogic
    {
        public enum UserRegistrationInfo
        {
            Success=0,
            Failed,
            AlreadyRegistered,
        }

        public UserBusinessLogic()
        {
        }

        public UserRegistrationInfo RegisterUser(RegisterModel registerationInfo)
        {
            if (LanarkshireGamersRepo.Instance.GetUserByUsername(registerationInfo.UserName) == null)
            {
                User u = new User
                {
                    UserName = registerationInfo.UserName,
                    Firstname = registerationInfo.Firstname,
                    Lastname = registerationInfo.Lastname,
                    Password = registerationInfo.Password,
                    EmailAddress = registerationInfo.Email,
                    GeekUserName = registerationInfo.GeekUserName,
                    FacebookUserName = registerationInfo.FacebookUserName
                };

                if (LanarkshireGamersRepo.Instance.AddUser(u))
                    return UserRegistrationInfo.Success;
                else
                    return UserRegistrationInfo.Failed;
            }
            else
            {
                return UserRegistrationInfo.AlreadyRegistered;
            }
        }
    }
}
