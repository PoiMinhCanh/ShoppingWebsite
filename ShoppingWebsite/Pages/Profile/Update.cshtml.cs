using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Model.DTO;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Profile;

public class UpdateModel : StateModel
{
    private readonly IMapper _mapper;

    [BindProperty]
    public ProfileDTO ProfileDTO { get; set; }

    public UpdateModel(ApplicationDbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
    }

    public void OnGet(int id)
    {
        Account account = _db.Accounts.SingleOrDefault(account => account.AccountID.Equals(id));
        ProfileDTO = _mapper.Map<Account, ProfileDTO>(account);
    }
  
}
