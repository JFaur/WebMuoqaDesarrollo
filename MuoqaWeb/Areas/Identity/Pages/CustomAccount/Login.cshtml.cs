using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace MuoqaWeb.Areas.Identity.Pages.CustomAccount
{
    public class LoginModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _config;
        public LoginModel(IMemoryCache memoryCache, IConfiguration config)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public string urlWeb { get; set; }
        public string Token { get; set; }
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
