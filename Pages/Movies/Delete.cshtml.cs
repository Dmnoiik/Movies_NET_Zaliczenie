using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieReviewApp.Data;
using MovieReviewApp.Models;
using System.Security.Claims;

namespace MovieReviewApp.Pages.Movies
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _context.Movies.FindAsync(id);

            if (Movie == null || Movie.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound(); // Użytkownik nie ma dostępu do tego filmu
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null || movie.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound(); // Użytkownik nie ma dostępu do tego filmu
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
