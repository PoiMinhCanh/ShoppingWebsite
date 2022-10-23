using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWebsite.Data;
using ShoppingWebsite.Model;
using ShoppingWebsite.Model.DTO;
using ShoppingWebsite.Model.Enum;
using ShoppingWebsite.Services.ManageState;

namespace ShoppingWebsite.Pages.Register;

public class IndexModel : StateModel
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    [BindProperty]
    public CreateAccountDTO CreateAccountDTO { get; set; }

    public IndexModel(ApplicationDbContext db, IMapper mapper, IConfiguration configuration) : base(db)
    {
        _mapper = mapper;
        _configuration = configuration;
    }

    public IActionResult OnGet()
    {
        if (IsAuthenticated)
        {
            return Redirect("/");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            Account existAccount = _db.Accounts.SingleOrDefault(account => account.UserName.Equals(CreateAccountDTO.UserName));
            if (existAccount != null)
            {
                ErrorMessages.Add("Username is not available. Please use another username!");
                return Page();
            }

            var account = _mapper.Map<CreateAccountDTO, Account>(CreateAccountDTO);
            string secretKey = _configuration["SecretKey"];

            // check secretkey correct or not
            // if correct, become admin
            if (CreateAccountDTO.SecretKey == null || !CreateAccountDTO.SecretKey.Equals(secretKey))
            {
                account.Type = AccountType.Member;
            }
            else account.Type = AccountType.Staff;

            _db.Accounts.Add(account);
            _db.SaveChanges();

            TempData["success"] = $"You have successfully registered an account with username={account.UserName}";

            return Redirect("/Login");
        }
        return Page();
    }
}