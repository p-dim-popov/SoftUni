using SULS.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProblemService _problemService;

        public HomeController(ProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
                return View(_problemService.GetAll(), "/IndexLoggedIn");
            }

            return View();
        }
    }
}