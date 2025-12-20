using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementSystem.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Hizmet adı zorunludur.")]
        [Display(Name = "Hizmet Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Süre bilgisi zorunludur.")]
        [Range(15, 300, ErrorMessage = "Süre 15 ile 300 dakika arasında olmalıdır.")]
        [Display(Name = "Süre (Dakika)")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "Ücret belirtilmelidir.")]
        [Column(TypeName = "decimal(18,2)")] // Veritabanı hassasiyeti için
        [Display(Name = "Ücret (£)")]
        public decimal Price { get; set; }

        // Foreign Key
        [Display(Name = "Spor Salonu")]
        public int GymId { get; set; }

        [ForeignKey("GymId")]
        public virtual Gym? Gym { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}