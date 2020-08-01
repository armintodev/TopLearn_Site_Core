using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region Identity User
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddUser(User user);
        User LoginUser(LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string userName);
        void Update(User user);
        bool ActiveAccount(string activeCode);
        #endregion

        #region User Panel

        InformationUserViewModel GetUserInformation(string userName);
        SideBarUserPanelViewModel GetSideBarUserPanelData(string userName);
        EditProfileViewModel GetDataForEditProfileUser(string userName);
        void EditProfile(string userName,EditProfileViewModel profile);
        bool CompareOldPassword(string oldPassword, string userName);
        void ChangeUserPassword(string userName, string newPassword);

        #endregion
    }
}
