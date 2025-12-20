using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MemberId { get; set; }

        [Required(ErrorMessage = "Lütfen bir antrenör seçiniz.")]
        [Display(Name = "Antrenör")]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Lütfen bir hizmet seçiniz.")]
        [Display(Name = "Hizmet")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Randevu tarihi ve saati seçilmelidir.")]
        [Display(Name = "Randevu Tarihi ve Saati")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Onay Durumu")]
        public bool IsConfirmed { get; set; } = false;

        // Navigation Properties
        [ForeignKey("TrainerId")]
        public virtual Trainer? Trainer { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service? Service { get; set; }

        [ForeignKey("MemberId")]
        public virtual IdentityUser? Member { get; set; }
    }
}