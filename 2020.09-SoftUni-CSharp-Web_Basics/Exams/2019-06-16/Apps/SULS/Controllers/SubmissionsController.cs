using System;
using System.Collections.Generic;
using System.Text;
using SULS.Models.Submissions;
using SULS.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    class SubmissionsController : Controller
    {
        private readonly SubmissionService _submissionService;
        private readonly ProblemService _problemService;

        public SubmissionsController(SubmissionService submissionService, ProblemService problemService)
        {
            _submissionService = submissionService;
            _problemService = problemService;
        }

        public HttpResponse Create(string id)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var problem = _problemService.GetProblemById(id);
            if (problem is null)
            {
                return Error("Problem does not exist!");
            }

            return View(new SubmissionCreateViewModel { ProblemId = id, Name = problem.Name });
        }

        [HttpPost]
        public HttpResponse Create(string problemId, string code)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var submission = new SubmissionCreateInputModel
            {
                Code = code
            };

            var validation = ValidationHelper.IsValid(submission);
            if (!validation.result)
            {
                return Error(validation.errorMessage);
            }

            _submissionService.CreateSubmission(GetUserId(), problemId, submission);

            return Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            _submissionService.DeleteSubmission(id);

            return Redirect("/");
        }
    }
}
