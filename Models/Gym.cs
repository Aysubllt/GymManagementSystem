using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Models // Hatanın çözümü burası
{
    public class Gym
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Salon adı zorunludur.")]
        [StringLength(100)]
        [Display(Name = "Spor Salonu Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adres alanı zorunludur.")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        // SAATLER
        [Required]
        [Display(Name = "Açılış Saati")]
        [DataType(DataType.Time)]
        public TimeSpan OpeningTime { get; set; }

        [Required]
        [Display(Name = "Kapanış Saati")]
        [DataType(DataType.Time)]
        public TimeSpan ClosingTime { get; set; }

        // GÜNLER
        public bool OpenMonday { get; set; } = true;
        public bool OpenTuesday { get; set; } = true;
        public bool OpenWednesday { get; set; } = true;
        public bool OpenThursday { get; set; } = true;
        public bool OpenFriday { get; set; } = true;
        public bool OpenSaturday { get; set; } = false;
        public bool OpenSunday { get; set; } = false;

        // Navigation Properties
        public virtual ICollection<Service>? Services { get; set; }
        public virtual ICollection<Trainer>? Trainers { get; set; }
    }
}