using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementSystem.Models // Hatanın çözümü burası
{
    public class Trainer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Antrenör adı zorunludur.")]
        [Display(Name = "Ad Soyad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Uzmanlık alanı belirtilmelidir.")]
        [Display(Name = "Uzmanlık")]
        public string Specialty { get; set; }

        // MESAİ SAATLERİ
        [Required]
        [Display(Name = "Mesai Başlangıç")]
        [DataType(DataType.Time)]
        public TimeSpan ShiftStartTime { get; set; }

        [Required]
        [Display(Name = "Mesai Bitiş")]
        [DataType(DataType.Time)]
        public TimeSpan ShiftEndTime { get; set; }

        // ÇALIŞTIĞI GÜNLER
        public bool WorkMonday { get; set; } = true;
        public bool WorkTuesday { get; set; } = true;
        public bool WorkWednesday { get; set; } = true;
        public bool WorkThursday { get; set; } = true;
        public bool WorkFriday { get; set; } = true;
        public bool WorkSaturday { get; set; } = false;
        public bool WorkSunday { get; set; } = false;

        public string? ImagePath { get; set; }
        public string? Bio { get; set; }

        [Display(Name = "Spor Salonu")]
        public int GymId { get; set; }

        [ForeignKey("GymId")]
        public virtual Gym? Gym { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}