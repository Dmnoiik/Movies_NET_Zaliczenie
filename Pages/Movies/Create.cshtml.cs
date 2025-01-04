using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieReviewApp.Data;
using MovieReviewApp.Models;
using System.Security.Claims;

namespace MovieReviewApp.Pages.Movies
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Movies/Index");
        }
    }
}
