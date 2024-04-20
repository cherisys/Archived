using Api.Global;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private IRepository<Question> Repo;

        public QuestionsController(IRepository<Question> _repo)
        {
            Repo = _repo;
        }

        [HttpGet]
        [Route("api/Questions")]
        public async Task<IActionResult> Get()
        {
            var QuestionList = await Repo.GetAll();
            var Qns = (from x in QuestionList
                            select new {
                            x.Id,
                            x.QuestionText,
                            x.ImageName,
                            Options = new[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                         })
                        .OrderBy(y => Guid.NewGuid()).Take(10).ToArray();
            return Ok(Qns);
        }

        [HttpPost]
        [Route("api/Questions/Answers")]
        public async Task<IActionResult> Answers(long[] ids)
        {
            var QuestionList = await Repo.GetAll();
            var Ans = (from x in QuestionList
                              join y in ids on x.Id equals y
                              select new
                              {
                                  x.Id,
                                  x.Answer
                              }).OrderBy(x => ids.IndexOf(x.Id)).Select(x => x.Answer).ToArray();

            return Ok(Ans);
        }
    }
}