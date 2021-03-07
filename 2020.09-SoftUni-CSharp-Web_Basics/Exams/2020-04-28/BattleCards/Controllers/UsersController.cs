using SUS.HTTP;
using SUS.MvcFramework;

namespace BattleCards.Controllers
{
    using Services;
    using ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return Redirect("/");
            }
            
            return View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (IsUserSignedIn())
            {
                return Redirect("/");
            }

            var userId = _userService.GetUserId(username, password);
            if (userId is null)
            {
                return Error("Invalid username or password!");
            }

            SignIn(userId);
            return Redirect("/Cards/All");
        }

        public HttpResponse Register()
            => IsUserSignedIn()
                ? Redirect("/")
                : View();


        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel user)
        {
            if (IsUserSignedIn())
            {
                return Redirect("/");
            }

            var (result, errorMessage) = ValidationHelper.IsValid(user);
            if (!result)
            {
                return Error(errorMessage);
            }

            if (!_userService.IsUsernameAvailable(user.Username))
            {
                return Error("Username is not available!");
            }

            if (!_userService.IsEmailAvailable(user.Email))
            {
                return Error("Email is not available!");
            }

            _userService.AddUser(user.Username, user.Email, user.Password);

            return Redirect("/Users/Login");
        }

        [HttpGet("/Logout")]
        public HttpResponse Logout()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/");
            }

            SignOut();
            return Redirect("/");
        }
    }
}
