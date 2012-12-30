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
                FacebookDetails = new FacebookUser { email = model.Email },
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
                FacebookUserName = u.FacebookDetails.email
            };

            return m;
        }

        public static GameViewModel ConvertGametoGameViewModel(Game g)
        {
            GameViewModel gvm = new GameViewModel
            {
                Name=g.Name,
                Description=g.Description,
                GeekID=g.GeekID,
                MaxNumberOfPlayers=g.MaxNumberOfPlayers,
                MinNumberOfPlayers=g.MinNumberOfPlayers,
                PlayTime=g.PlayTime,
                ThumbNailURL=g.ThumbNailURL
            };

            return gvm;
        }

        public static Game ConvertGameViewModelToGame(GameViewModel gvm)
        {
            Game g = new Game
            {
                Name = gvm.Name,
                Description = gvm.Description,
                GeekID = gvm.GeekID,
                MaxNumberOfPlayers = gvm.MaxNumberOfPlayers,
                MinNumberOfPlayers = gvm.MinNumberOfPlayers,
                ThumbNailURL = gvm.ThumbNailURL,
                PlayTime = gvm.PlayTime
            };

            return g;
        }

        public static SearchGameViewModel ConvertGeekGameSearchToGameViewModel(BoardGameGeekSearchResult bgg)
        {
            SearchGameViewModel gvm = new SearchGameViewModel
            {
                GeekID=bgg.GeekID,
                Name=bgg.Name,
                Selected=false
            };

            return gvm;
        }
    }
}
