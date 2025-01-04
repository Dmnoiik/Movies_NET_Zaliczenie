using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieReviewApp.Models
{
    public class Movie
    {
        public Movie()
        {
            Reviews = new List<Review>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        public ICollection<Review> Reviews { get; set; }

        [ValidateNever]
        public string UserId { get; set; } // Powiązanie z użytkownikiem

        [ValidateNever]
        public IdentityUser User { get; set; } // Nawigacja do użytkownika
    }
}
