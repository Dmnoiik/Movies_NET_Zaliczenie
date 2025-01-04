using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [ValidateNever]
        public Movie Movie { get; set; } // Relacja z modelem Movie

        [ForeignKey("User")]
        public string UserId { get; set; } // Powiązanie z użytkownikiem

        [ValidateNever]
        public IdentityUser User { get; set; } // Nawigacja do użytkownika
    }
}
