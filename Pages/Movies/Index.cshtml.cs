using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Models;
using System.Security.Claims;

namespace MovieReviewApp.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            Movies = await _context.Movies
                .Include(m => m.Reviews)
                .ToListAsync();
        }
    }
}
