using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model.DTO;
using ShoppingWebsite.Model;
using ShoppingWebsite.Pages.Profile;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Profile;
    
public class IndexModel : StateModel
{
    private readonly IMapper _mapper;

    [BindProperty]
    public ProfileDTO ProfileDTO { get; set; }

    public IndexModel(ApplicationDbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
    }

    public IActionResult OnGet(int id)
    {
        if (!IsAuthenticated)
        {
            return Redirect("/Login/Index");
        }
            
        if (Account.AccountID.Equals(id))
        {
            ProfileDTO = _mapper.Map<Account, ProfileDTO>(Account);
            Customer customer = _db.Customers.SingleOrDefault(profile => profile.CustomerID.Equals(id));
            if (customer != null)
            {
                ProfileDTO = _mapper.Map<Customer, ProfileDTO>(customer, ProfileDTO);
            }
        }

        return Page();
    }
    public IActionResult OnPost(int id)
    {
        if (Account.AccountID.Equals(id))
        {
            // update fullname of Account table
            Account.FullName = ProfileDTO.FullName;
            _db.Accounts.Update(Account);

            Customer customer = _mapper.Map<ProfileDTO, Customer>(ProfileDTO);

            Customer existCustomer = _db.Customers.SingleOrDefault(profile => profile.CustomerID.Equals(id));
            // insert if not exist yet, update if exist
            if (existCustomer != null)
            {
                _db.Customers.Update(customer);
            } else
            {
                _db.Customers.Add(customer);
            }

            _db.SaveChanges();
        }

        return Page();
    }
}