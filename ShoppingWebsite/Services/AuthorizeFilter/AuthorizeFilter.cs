using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingWebsite.Data;
using ShoppingWebsite.Pages.Register;

namespace ShoppingWebsite.Services.AuthorizeFilter;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class AuthorizeFilter : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public AuthorizeFilter(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var result = context.HttpContext.Request.Cookies.TryGetValue("role", out string role);

        if (!result)
        {
            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            return; // Not authenticated
        }

        var isAuthorized = _role.Equals(role);
        if (!isAuthorized)
        {
            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            return;
        }
    }
 
}