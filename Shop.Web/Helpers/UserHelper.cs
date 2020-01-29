using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using Shop.Web.Models;

namespace Shop.Web.Helpers
{
    /*
        CLASS SUMMARY: This class is essentially a hub for seperate core functionalities pertaining to managing
        users in our app. At the time of this note, those functionalities are user CRUD, user lookup,  
        user login/logout, user validation, and user permissions via role assignment.
        
        Key concept: *Dependency Injection*
    */
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        //CONSTRUCTOR: These are all dependency injections
        public UserHelper(
            UserManager<User> userManager, //Provides the APIs for managing users
            SignInManager<User> signInManager, //Provides the APIs for user sign in
            RoleManager<IdentityRole> roleManager) //Provides the APIs for managing roles
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        //creates a user via UserManager injection
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        //Assigns a user a role, or rather, adds a user to a list of users
        //with that role
        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await this.userManager.AddToRoleAsync(user, roleName);
        }

        //Change user password WITH validation 
        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        //Creates a role if it does not already exist
        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }

        }


        //User lookup by email
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }

        //Boolean which checks whether a has the specified role
        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await this.userManager.IsInRoleAsync(user, roleName);
        }

        //*A CORE CONTROLLER METHOD* 
        //This method attempts to sign in with current information input
        //by the user in the LoginViewModel.
        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await this.signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false); //Toggles lockout in the case of incorrect credentials
        }

        //Logs the user out
        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        //Updates a user's info and data
        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await this.userManager.UpdateAsync(user);

        }

        //Password validation
        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await this.signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);

        }
    }
}
