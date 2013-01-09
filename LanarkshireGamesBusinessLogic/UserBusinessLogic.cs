using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanarkshireGamers.ViewModel;
using LanarkshireGamersData;
using LanarkshireGamersData.Model;
using LanarkshireGamesBusinessLogic.Helpers;

namespace LanarkshireGamesBusinessLogic
{
    //http://amirrajan.net/Blog/asp-mvc-and-facebook-single-sign-on

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
            if (!UserExists(registerationInfo.UserName))
            {
                User u = BusinessLogicHelper.ConvertRegistrationModelToUser(registerationInfo);

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

        public EditModel GetUser(string username)
        {
            if (UserExists(username))
            {
                EditModel user = BusinessLogicHelper.ConvertUserToEditModel(LanarkshireGamersRepo.Instance.GetUserByUsername(username));
                return user;
            }

            return null;
        }

        public string GetPassword(string username)
        {
            if (UserExists(username))
            {
                User u = LanarkshireGamersRepo.Instance.GetUserByUsername(username);
                return u.Password;
            }

            return string.Empty;
        }

        public bool UpdateUserDetails(string username, EditModel editModel)
        {
            //get user
            if (UserExists(username))
            {
                User u=LanarkshireGamersRepo.Instance.GetUserByUsername(username);

                u.EmailAddress = editModel.Email;
                u.FacebookDetails.email = editModel.FacebookUserName;
                u.Firstname = editModel.Firstname;
                u.GeekUserName = editModel.GeekUserName;
                u.Lastname = editModel.Lastname;

                LanarkshireGamersRepo.Instance.UpdateUser(u);
                return true;
            }

            return false;
        }

        public bool UpdateUserPassword(string username, ChangePasswordModel passwordModel)
        {
            if (UserExists(username))
            {
                User u = LanarkshireGamersRepo.Instance.GetUserByUsername(username);
                u.Password = passwordModel.NewPassword;
                LanarkshireGamersRepo.Instance.UpdateUser(u);
                return true;
            }

            return false;
        }


        public bool UserExists(string username)
        {
            return ((LanarkshireGamersRepo.Instance.GetUserByUsername(username)!=null) ? true : false); 
        }
    }
}
