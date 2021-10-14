using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using priberam.Models.DTO;

namespace priberam.Services
{
    public class AccountService: AccountServiceInterface
    {
        private readonly IdentityServiceInterface _identityService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(IdentityServiceInterface identityService, UserManager<ApplicationUser> userManager)
        {
            _identityService = identityService;
            _userManager = userManager;
        }

        public async Task<bool> Create(AccountUser model) {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result.Succeeded;
        }

        public async Task<List<AccountUser>> List()
        {
            var result = _userManager.Users.Select(elm => new AccountUser { Email = elm.Email, Password = "*****" }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<AccountUser> Select(AccountUser model)
        {
            var filter = _userManager.Users.Where(elm => elm.Email == model.Email).ToList();
            var result = filter.Count() > 0 ? new AccountUser { Email = filter[0].Email, Password = "*****" } : null;
            return await Task.FromResult(result);
        }

        public async Task<bool> Delete(AccountUser model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> Update(AccountUser model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}

/*
    https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio
    https://docs.microsoft.com/en-us/aspnet/core/security/authentication/add-user-data?view=aspnetcore-5.0&tabs=visual-studio#test-create-view-download-delete-custom-user-data
*/