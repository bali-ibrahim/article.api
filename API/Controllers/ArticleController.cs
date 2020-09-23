using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _service;

        public ArticleController(IArticleService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var rows = await _service.GetAsync(id);
            return Ok(rows);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchAsync(Article model)
        {
            var rows = await _service.SearchAsync(model);
            return Ok(rows);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Article model)
        {
            var rows = await _service.AddAsync(model);
            return Ok(rows);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Article model)
        {
            var rows = await _service.UpdateAsync(model);
            return Ok(rows);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var rows = await _service.DeleteAsync(id);
            return Ok(rows);
        }
    }
}