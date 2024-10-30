using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MuoqaWeb.Areas.Identity.Pages.CustomAccount
{
    public class RegisterModel : PageModel
    {

        private readonly IConfiguration _config;
        public RegisterModel(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public string urlWeb { get; set; }
        public string Token { get;set; }
        public void OnGet()
        {
            urlWeb = _config["URLDependingOnEnvironment"];

            var token = HttpContext.RequestServices
            .GetService<Microsoft.AspNetCore.Antiforgery.IAntiforgery>()
            .GetAndStoreTokens(HttpContext);
            Token = token.RequestToken;
        }
    }
}
