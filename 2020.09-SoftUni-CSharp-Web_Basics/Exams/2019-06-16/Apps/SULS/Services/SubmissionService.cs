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
    public class SubmissionService
    {
        private readonly SULSContext _dbContext;

        public SubmissionService(SULSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string CreateSubmission(string userId, string problemId, SubmissionCreateInputModel model)
        {
            var submission = new Submission
            {
                Code = model.Code,
                AchievedResult = new Random().Next(0, _dbContext.Problems
                    .Where(p => p.Id == problemId)
                    .Select(p => p.Points)
                    .FirstOrDefault()),
                CreatedOn = DateTime.Now,
                User = _dbContext.Users.FirstOrDefault(u => u.Id == userId),
                Problem = _dbContext.Problems.FirstOrDefault(p => p.Id == problemId),
                Id = Guid.NewGuid().ToString(),
            };

            _dbContext.Submissions.Add(submission);
            _dbContext.SaveChanges();

            return submission.Id;
        }

        public void DeleteSubmission(string id)
        {
            var submission = _dbContext.Submissions
                .FirstOrDefault(s => s.Id == id);
            if (submission is null)
            {
                return;
            }

            _dbContext.Submissions.Remove(submission);
            _dbContext.SaveChanges();
        }
    }
}
