﻿using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;
public class AuthenticateService : IAuthenticate
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> Authenticate(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
        return result.Succeeded;
    }
    public async Task<bool> RegisterUser(string username, string password)
    {
        var applicationUser = new ApplicationUser
        {
            UserName = username,
            Email = username
        };

        var result = await _userManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(applicationUser, isPersistent: false);
            return true;
        }
        return result.Succeeded;
    }
    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}
