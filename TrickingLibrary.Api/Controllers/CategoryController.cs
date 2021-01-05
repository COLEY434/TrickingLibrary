using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Api.Models;
using TrickingLibrary.Data;

namespace TrickingLibrary.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public CategoryController(AppDbContext appContext)
        {
            this.dbContext = appContext;
        }

        [HttpGet]
        public IEnumerable<Category> All() => dbContext.Categories.ToList();

        [HttpGet("id")]
        public Category Get(string Id) =>
            dbContext.Categories
                .FirstOrDefault(x => x.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase));

        [HttpGet("{id}/tricks")]
        public IEnumerable<Trick> ListCategoryTricks(string Id)
        {
            return dbContext.TrickCategories.Include(x => x.Trick)
                    .Where(x => x.CategoryId.Equals(Id, StringComparison.InvariantCultureIgnoreCase))
                    .Select(x => x.Trick)
                    .ToList();
        }
        [HttpPost]
        public async Task<Category> Create([FromBody] Category category)
        {
            category.Id = category.Name.Replace(" ", "-").ToLowerInvariant();
            dbContext.Add(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

    }
}
