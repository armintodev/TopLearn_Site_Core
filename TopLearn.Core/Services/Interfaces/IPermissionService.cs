using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.DataLayer.Entities.Permissions;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Roles

        List<Role> GetRoles();
        int AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        Role GetRoleById(int roleId);
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditRolesUser(int userId, List<int> rolesId);

        #endregion

        #region Permission

        List<Permission> GetAllPermission();
        void AddPermissionsToRole(int roleId, List<int> permissions);
        List<int>PermissionsRole(int roleId);
        void UpdatePermissionsRole(int roleId, List<int> permissions);

        #endregion
    }
}
