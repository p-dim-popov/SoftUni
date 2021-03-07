using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SULS.Data;
using SULS.Data.Models;
using SULS.Models.Problems;
using SULS.Models.Submissions;

namespace SULS.Services
{
    public class ProblemService
    {
        private readonly SULSContext _dbContext;

        public ProblemService(
            SULSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string CreateProblem(ProblemCreateInputModel model)
        {
            var problem = new Problem
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Points = model.Points
            };

            _dbContext.Problems.Add(problem);
            _dbContext.SaveChanges();

            return problem.Id;
        }

        public IEnumerable<ProblemHomeViewModel> GetAll()
        {
            return _dbContext.Problems
                .Select(p => new ProblemHomeViewModel
                {
                    Count = p.Submissions.Count,
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
        }

        public Problem GetProblemById(string problemId)
        {
            return _dbContext.Problems.FirstOrDefault(p => p.Id == problemId);
        }

        public ProblemDetailsViewModel GetProblemDetails(string id)
        {
            var problem = _dbContext.Problems
                .Where(p => p.Id == id)
                .Select(p => new {p.Name, p.Points})
                .FirstOrDefault();

            return new ProblemDetailsViewModel
            {
                Name = problem?.Name,
                MaxPoints = problem?.Points ?? 0,
                Submissions = _dbContext.Submissions
                    .Where(s => s.Problem.Id == id)
                    .Select(s => new SubmissionDetailsViewModel
                    {
                        AchievedResult = s.AchievedResult,
                        CreatedOn = s.CreatedOn,
                        SubmissionId = s.Id,
                        Username = s.User.Username
                    })
                    .ToList()
        };
        }
    }
}
