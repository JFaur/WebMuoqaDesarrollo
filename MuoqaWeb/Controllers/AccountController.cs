using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using MuoqaBackend.ToBD;
using MuoqaBackend.Validations;
using MuoqaIdentidades;
using Serilog;
namespace MuoqaWeb.Controllers
{
    [Route("Account")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ValidateSession _validateSession;
        public AccountController(IMemoryCache memoryCache, ValidateSession validateSession)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _validateSession = validateSession ?? throw new ArgumentNullException(nameof(validateSession));
        }

        private string cacheKey = "loggedInUser";
        string urlOK = "/";
        [HttpPost("RegisterUser")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RegisterUser([FromBody]Account data)
        {
            Console.WriteLine("LLego a la funcion");
            string urlError = Url.Action("InvalidSession", "Account");
            try
            {
                if (data.UserName == "UsuarioDefault")
                    return new JsonResult(new { errorMessage = "Ese usuario ya existe" });
                if (data == null)
                    return new JsonResult(new { errorMessage = "Hay campos vacios" });
                string checkUserAndMail = await _validateSession.CheckUserAndMail(data.UserName, data.Email);
                switch (checkUserAndMail)
                {
                    case "user":
                        return new JsonResult(new { errorMessage = "El usuario ya existe" });
                    case "email":
                        return new JsonResult(new { errorMessage = "El mail ya existe" });
                    case "false":
                        return new JsonResult(new { urlReturn = urlError, errorMessage = "Hubo un error por favor intentelo mas tarde" });
                    default:
                        break;
                }
                var validator = new ValidateAccount();
                ValidationResult check = validator.Validate(data);
                string pass = BCrypt.Net.BCrypt.HashPassword(data.UserPassword);//Encripta la contraseña
                if (!check.IsValid)
                {
                    foreach (var error in check.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                        Log.Error($"Error: {error.ErrorMessage}");
                        return new JsonResult(new { errorMessage = error.ErrorMessage });
                    }
                }
                bool checkInsert = await _validateSession.UploadNewUser(data);
                if (checkInsert)
                {
                    AccountLogin loggin = new AccountLogin { UserName = data.UserName };
                    //Guardar el usuario logeado en cache. [clave para buscar el usuario, datos, tiempo de logeo]
                    _memoryCache.Set(cacheKey, loggin, TimeSpan.FromMinutes(30));
                    Console.WriteLine($"AccountController: {urlOK}");
                    return new JsonResult(new { urlReturn = urlOK });
                }
                else
                {
                    return new JsonResult(new { urlReturn = urlError, errorMessage = "No se pudo cargar el registro intentelo mas tarde" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Log.Error($"Error: {ex.Message}"); 
                return new JsonResult(new { urlReturn = urlError, errorMessage = "No se pudo cargar el registro intentelo mas tarde" });
            }
        }

        [HttpPost("LoginUser")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> LoginUser([FromBody] AccountLogin data)
        {
            Console.WriteLine("AccountController: Llego a la funcion");
            string urlError = Url.Action("InvalidSession", "Account");
            Console.WriteLine($"AccountController: {urlError}");
            try
            {
                if (data == null)
                    return new JsonResult(new { errorMessage = "Hay campos vacios" });
                if (data.UserName == "UsuarioDefault")
                    return new JsonResult(new { errorMessage = "Ese usuario no se puede logear" });
                if (!await _validateSession.CheckUser(data.UserName))
                    return new JsonResult(new { errorMessage = "Ese usuario no existe" });

                string pwd = BCrypt.Net.BCrypt.HashPassword(data.UserPassword);

                bool checkPwd = await _validateSession.ComparePwd(data.UserName, pwd);
                if (checkPwd)
                {
                    AccountLogin loggin = new AccountLogin { UserName = data.UserName };
                    _memoryCache.Set(cacheKey, loggin, TimeSpan.FromMinutes(30));
                    Console.WriteLine($"AccountController: {urlOK}");
                    return new JsonResult(new { urlReturn = urlOK });
                }
                else
                {
                    return new JsonResult(new { errorMessage = "Contraseña incorrecta" });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message} A");
                Log.Error($"Error: {ex.Message} A");
                return new JsonResult(new { urlReturn = urlError, errorMessage = "No se pudo cargar el registro intentelo mas tarde" });
            }
        }

        [HttpPost("UnLogUser")]
        [ValidateAntiForgeryToken]
        public JsonResult UnLogUser()
        {
            Console.WriteLine("AccountController: LLega al deslogeo");
            _memoryCache.Remove(cacheKey);
            return new JsonResult(new { urlReturn = urlOK });
        }
    }
}
