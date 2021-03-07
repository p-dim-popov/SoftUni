using SULS.Models.Users;
using SULS.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.App.Controllers
{
    public class UsersController: Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        public HttpResponse Login()
        {
            return View();
        }

        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel model)
        {
            if (IsUserSignedIn())
            {
                return Redirect("/");
            }

            var validation = ValidationHelper.IsValid(model);
            if (!validation.result)
            {
                return Error(validation.errorMessage);
            }

            if (!_userService.IsUsernameAvailable(model.Username))
            {
                return Error("Username is not available!");
            }

            if (!_userService.IsEmailAvailable(model.Email))
            {
                return Error("Email is not available!");
            }

            _userService.RegisterUser(model);

            return Redirect("/Users/Login");
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

            return Redirect("/");
        }

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