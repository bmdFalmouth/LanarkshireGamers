using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanarkshireGamersData.Model;
using LanarkshireGamers.ViewModel;

namespace LanarkshireGamesBusinessLogic.Helpers
{
    class BusinessLogicHelper
    {
        public static User ConvertRegistrationModelToUser(RegisterModel model)
        {
            User u = new User
            {
                UserName = model.UserName,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Password = model.Password,
                EmailAddress = model.Email,
                GeekUserName = model.GeekUserName,
                FacebookUserName = model.FacebookUserName
            };

            return u;
        }

        public static EditModel ConvertUserToEditModel(User u)
        {
            EditModel m = new EditModel
            {
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Email = u.EmailAddress,
                GeekUserName = u.GeekUserName,
                FacebookUserName = u.FacebookUserName
            };

            return m;
        }
    }
}
