using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWebsite.Data;
using ShoppingWebsite.Services.ManageState;
using System.Diagnostics;

namespace ShoppingWebsite.Pages;

public class IndexModel : StateModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger) : base(db)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

}
