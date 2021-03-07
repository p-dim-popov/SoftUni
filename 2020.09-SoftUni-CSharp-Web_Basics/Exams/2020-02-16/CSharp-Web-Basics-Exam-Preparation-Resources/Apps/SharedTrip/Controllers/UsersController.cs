using SharedTrip.Services;
using SharedTrip.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip.Controllers
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
            var validation = model.IsValid();
            if (!validation.isSuccessful)
            {
                return Error(validation.errorMessage);
            }

            if (!_userService.IsEmailAvailable(model.Email))
            {
                return Error("Email is taken");
            }

            if (!_userService.IsUsernameAvailable(model.Username))
            {
                return Error("Username is taken");
            }

            _userService.RegisterUser(model);

            return Redirect("/Users/Login");
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = _userService.GetUserId(username, password);
            if (userId is null)
            {
                return Error("Password or username is not correct");
            }

            SignIn(userId);
            return Redirect("/Trips/All");
        }
    }
}
