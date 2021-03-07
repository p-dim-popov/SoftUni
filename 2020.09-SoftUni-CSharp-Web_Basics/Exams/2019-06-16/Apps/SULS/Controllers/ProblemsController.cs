using System;
using System.Collections.Generic;
using System.Text;
using SULS.Models.Problems;
using SULS.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    class ProblemsController : Controller
    {
        private readonly ProblemService _problemService;

        public ProblemsController(ProblemService problemService)
        {
            _problemService = problemService;
        }
        public HttpResponse Create()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            return View();
        }

        public HttpResponse Details(string id)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            return View(_problemService.GetProblemDetails(id));
        }

        [HttpPost]
        public HttpResponse Create(ProblemCreateInputModel model)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var validation = ValidationHelper.IsValid(model);
            if (!validation.result)
            {
                return Error(validation.errorMessage);
            }

            _problemService.CreateProblem(model);

            return Redirect("/");
        }
    }
}
