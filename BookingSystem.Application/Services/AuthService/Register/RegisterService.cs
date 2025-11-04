using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using BookingSystem.Core.common;
using ChatApi.Application.Contract.Auth.Req;
using ChatApi.Application.Contract.Common;
using ChatApi.Application.Interfaces;
using ChatApi.Application.Interfaces.Auth;
using ChatApi.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ChatApi.Application.Services.AuthService.Register
{
    public class RegisterService : IRegister
    {
        private readonly UserManager<Infrastructure.Identity.ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterService(UserManager<Infrastructure.Identity.ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private async Task<bool> CheckUserIfExists(string Email)
        {
            var IsUserExist = await _userManager.FindByNameAsync(Email);
            return IsUserExist != null;
        }
      
              private async Task<(bool,string)> AddingRoleToUser(ApplicationUser user)
        {
               var addUserToRoleResult = await _userManager.AddToRoleAsync(user: user, role: Roles.User);
            if (addUserToRoleResult.Succeeded == false)
            {
                var errors = addUserToRoleResult.Errors.Select(e => e.Description);
                return (false, string.Join(",", errors));
            }
            return (true, "success");
        }

        private async Task<(bool,string)> CreateRoleIfNotExist()
        {
            if ((await _roleManager.RoleExistsAsync(Roles.User)) == false)
            {
                var roleResult = await _roleManager
                  .CreateAsync(new IdentityRole(Roles.User));

                if (roleResult.Succeeded == false)
                {
                    var roleErros = roleResult.Errors.Select(e => e.Description);
                    return( false , string.Join(" ,",roleErros));
                }

            }
            return (true,"");
        }
        public async Task<GeneralResponse<string>> Register(SingUp model)
        {
            string uploadedImagePath = string.Empty;
            if (model == null)
                return GeneralResponse<string>.Failure("Invalid user data", StatusCodes.Status400BadRequest);

            try
            {
                if (await CheckUserIfExists(model.Email))
                    return GeneralResponse<string>.Failure("User already exists", StatusCodes.Status409Conflict);

                var r = await CreateRoleIfNotExist();
                if (!r.Item1)
                {
                    return GeneralResponse<string>.Failure(r.Item2, StatusCodes.Status409Conflict);
                }
                
                ApplicationUser user = new()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return GeneralResponse<string>.Failure($"{string.Join(", ", result.Errors.Select(e => e.Description))}", StatusCodes.Status500InternalServerError);
                }
                var User = await AddingRoleToUser(user);
                if(!User.Item1)
                {
                    return GeneralResponse<string>.Failure(r.Item2, StatusCodes.Status500InternalServerError);
                }

                return GeneralResponse<string>.Success("", "User registered successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                if ( uploadedImagePath!= null && File.Exists(uploadedImagePath))
                    File.Delete(uploadedImagePath);
                return GeneralResponse<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }

        }
    }
}