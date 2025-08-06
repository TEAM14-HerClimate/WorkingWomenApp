using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Data;
using WorkingWomenApp.Database;
using WorkingWomenApp.Database.Constants;
using WorkingWomenApp.Database.DTOs.UserDtos;
using WorkingWomenApp.Database.enums;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.Implementation
{
    public class SecurityService: ISecurityService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<SecurityRole> _roleManager;
        private readonly IConfiguration _configuration;
        
       


        public SecurityService(IUnitOfWork unitOfWork,  RoleManager<SecurityRole> roleManager,
            IConfiguration configuration, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _configuration = configuration;
         
            _db= db;
          
        }
        public async Task<SecurityRole> GetRoleById(Guid id)
        {
            var role= await _unitOfWork.UserRepository.Set<SecurityRole>().FindAsync(id);
            
                return role;
            
            
        }

       public async Task<(bool, string)> SaveSecurityRoleAsync(ISecurityRole model)
       {
           bool success = false;
           string error = null;
           try
           {
               

               if (model.IsNewRecord)//create
               {
                   var roleId = await _createRole(model.Name);
                   await UpdateRolePermissionsAsync(model);

                }
               else//update
               {
                 
                   var dbRole= await _unitOfWork.UserRepository.Set<SecurityRole>().Where(r => r.Id == model.Id).FirstOrDefaultAsync();

                  

                  if (dbRole.IsSuperRole())
                   {
                       throw new Exception("You CAN'T edit the Super Role!");
                   }

                   var beforeXML = dbRole.GenerateLogXML("Before Update");


                   if (dbRole.Name == Constants.HerClimateAdministrator)
                   {
                       throw new Exception("You CAN'T edit the HerClimate Admin Role Name!");
                   }
                
                   dbRole.Name = model.Name;
                   await _updateRole(dbRole);
                   await UpdateRolePermissionsAsync(model);

                }

              

                success = true;
           }
           catch (Exception e)
           {
               error = e.Message;
               throw new Exception(error);
            }
           return (success,error);
       }


        public async Task<SecurityRole> FetchOneSecurityRoleswithPermissionAsync(Guid id)
        {
            var role =  _unitOfWork.UserRepository.Set<SecurityRole>().Include(r=>r.Permissions).FirstOrDefault(r => r.Id==id);
           

            if (role == null)
            {
                throw new Exception("No user Roles found.");
            }

            return role;


        }

        public async Task<IEnumerable<SecurityRole>> FetchAllSecurityRolesAsync()
        {
            var roles = _unitOfWork.UserRepository.Set<SecurityRole>().Include(r => r.Permissions);
            return roles;
            
        }

        public async Task<IEnumerable<RolePermission>> FetchAllRolePermissions(Guid roleId)
        {
            var roles = await _unitOfWork.RolePermissionRepository.GetAllAsync(x => x.RoleId == roleId, ("Permission"));
             return roles;
        }


        public async Task SaveRolePermissionAsync(List<Guid> permissionIds, Guid roleId)
        {
          
            
            if (roleId != Guid.Empty)
            {
                var PermissionList = permissionIds;

                //var dbSet = _unitOfWork.PermissionRepository.GetAllAsync();
                var previousPermissions =  _unitOfWork.RolePermissionRepository.GetAllAsync(x => x.RoleId == roleId, ("Permission"));;

                var rolePermissions = new List<RolePermission>();

                //Save New Permissions
                foreach (var permission in PermissionList)
                {
                    var newRolePermission = new RolePermission
                    {
                        Id = new Guid(),
                        RoleId = roleId,
                        PermissionId = permission
                    };

                    rolePermissions.Add(newRolePermission);
                }

                rolePermissions.ForEach(r => r.RoleId = roleId);
                
                await _unitOfWork.RolePermissionRepository.UpdateRangeAsync(rolePermissions);
                await _unitOfWork.SaveChangesAsync();
                
            }
            
        }


       
        public virtual async Task<Guid> _createRole(string roleName)
        {
            var role = new SecurityRole(roleName);
            await _handleTaskAsync(_roleManager.CreateAsync(role));
            return role.Id;
        }

        public virtual async Task _updateRole(SecurityRole dbRole)
        {
            await _handleTaskAsync(_roleManager.UpdateAsync(dbRole));
        }
        private async Task _handleTaskAsync(Task<IdentityResult> task, Func<Task> callbackFtn = null)
        {
            var result = await task;

            if (result.Succeeded)
            {
                if (callbackFtn != null) await callbackFtn();
            }
            else
            {
                throw new Exception(string.Join(",", result.Errors.Select(r => $"{r.Code}: {r.Description}")));
            }
        }

        private  async Task UpdateRolePermissionsAsync(ISecurityRole model)
        {
           
                var PermissionList = model.GetPermissions();


                var dbSet = await _unitOfWork.RolePermissionRepository.GetAllAsync();

                
                var newPermissions = PermissionList.Where(r => r.Enabled).ToList();
                var previousPermissions = dbSet.Where(r => r.RoleId == model.Id).ToList();

                //Save New Permissions

                foreach (var permission in newPermissions)
                {
                    var dbPermission = previousPermissions.Where(r => r.PermissionId == permission.PermissionId).FirstOrDefault();

                    if (dbPermission == null)//New Permission
                    {
                        var newRole = new RolePermission
                        {
                            RoleId = model.Id,
                            PermissionId = permission.PermissionId
                        };
                        _unitOfWork.RolePermissionRepository.AddAsync(newRole);

                    }
                    else//Existing Permission
                    {
                        previousPermissions.Remove(dbPermission);
                        
                    }
                }

                // Delete Removed Role Permissions i.e. Remaining in List
              
                await _unitOfWork.RolePermissionRepository.DeleteRangeAsync(previousPermissions);

                await _unitOfWork.SaveChangesAsync();

        }
        public async Task<bool> DeleteRole(string id)
        {
            bool success = false;

            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    _unitOfWork.UserRepository.Set<SecurityRole>().Remove(role);
                }

                success = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }

            return success;
        }

        public async Task EnqueuePermissions(List<PermissionType> protectedActionAttributes)
        {
            await _UpdatePermissions(protectedActionAttributes);
   
        }



        public async Task _UpdatePermissions(List<PermissionType> protectedActionAttributes)
        {
            try
            {

               
                    var existingPermissions = await _unitOfWork.PermissionTypeRepository.GetAllAsync();

                //Loop through discovered protected actions

                await _unitOfWork.PermissionTypeRepository.DeleteRangeAsync(existingPermissions);
                await _unitOfWork.PermissionTypeRepository.UpdateRangeAsync(protectedActionAttributes);
                    //foreach (var discoveredItem in protectedActionAttributes.ToList())
                    //{
                    //    var existingItem = existingPermissions.Where(r => r.Module == discoveredItem.Module && r.SubModule == discoveredItem.SubModule
                    //        && r.SystemAction == discoveredItem.SystemAction).SingleOrDefault();

                    //    if (existingItem != null)//Already Exists
                    //    {
                            
                    //        await _unitOfWork.PermissionTypeRepository.DeleteAsync(existingItem);

                    //    }
                    //    else//New Action
                    //    {
                    //       await _unitOfWork.PermissionTypeRepository.AddAsync(discoveredItem);
                    //    }
                    //}

               

                    await _unitOfWork.SaveChangesAsync();



            }
            catch (Exception e)
            {
                //_repository.LogUserAction(_sessionService, SecurityModule.Security, SecuritySubModule.SecurityRoles, SecuritySystemAction.UpdatePermissions, 0, $"Role: SecurityService.UpdatePermissions {e}");
            }
        }


        public async Task<UserRoleMapping> FetchUserAndRolesAsync(Guid userId)
        {

            var userAndRoles = await
                _unitOfWork.UserRoleMappingRepository.GetAsync(r => r.UserId == userId, "SecurityRoles, Users");
             
          
            
            return userAndRoles;
        }

    }

    public class RolePermissionViewModel : ISecurityPermission
    {
        public Guid PermissionId { get; set; }
        public bool Enabled { get; set; }
        public SecurityModule Module { get; set; }
        public SecuritySubModule SubModule { get; set; }
        public SecuritySystemAction SystemAction { get; set; }
    }
}
