using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proteus.Application.ViewModels.Identity.Account;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

namespace Proteus.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {   

        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public LoginViewModel Input { get; set; }
        public LoginModel()
        {
        }

        public IActionResult OnGet(string returnUrl = "")
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            ReturnUrl = returnUrl;
            return Page();
        }
        public IActionResult OnPost(string returnUrl = null)
        {
            returnUrl =  returnUrl ?? Url.Content("~/");

            if (Input.DODAccept == false)
            {
                ModelState.AddModelError(string.Empty, "You MUST accept the DoD statement before you can use this website.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                return RedirectToPage("./login2");
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}

