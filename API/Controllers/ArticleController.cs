﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Data;
using Model.Data.Interface;
using Model.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ICollectionRepository<IMeta> _metaRepository;
        private readonly ISingleRepository<Article> _articleRepository;

        public ArticleController(ICollectionRepository<IMeta> metaRepository,
            ISingleRepository<Article> articleRepository)
        {
            _metaRepository = metaRepository;
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var rows = await _metaRepository.ReadAsync();
            return Ok(rows);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            //var rows = await _articleRepository.ReadAsync(id);
            var rows = await _metaRepository.SearchAsync("ağ");
            return Ok(rows);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchAsync(string pattern)
        {
            var rows = await _metaRepository.SearchAsync(pattern);
            return Ok(rows);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Article model)
        {
            var isSuccessful = await _articleRepository.CreateAsync(model);
            return Ok(isSuccessful);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Article model)
        {
            var isSuccessful = await _articleRepository.UpdateAsync(model);
            return Ok(isSuccessful);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSuccessful = await _articleRepository.DeleteAsync(id);
            return Ok(isSuccessful);
        }
    }
}