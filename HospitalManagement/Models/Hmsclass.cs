using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Pleasse Enter FirstName")]
        [StringLength(30)]
        public String Firstname { get; set; }
        [Required(ErrorMessage = "Please Enter Lastname")]
        [StringLength(30)]
        public String Lastname { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        [StringLength(250)]
        public String Address { get; set; }
        [Required]
        public DateTime Patient_Dob { get; set; }
        [Required]
        [MaxLength(10)]
        public string MobileNo { get; set; }
        [Required]
        public DateTime DateOfAdmit { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String RoomType { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }

    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }
        [Required]
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Address { get; set; }
        public String MobileNo { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }

    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillId { get; set; }
        public int DoctorId { get; set; }
        public int RoomId { get; set; }
        public int DoctorCharge { get; set; }
        public int RoomCharge { get; set; }

        public DateTime Dischargedate { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

    }
    public class Admin
    {
        [Key]
        public int Adminid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class PatientDoctors
    {
        [Key]
        [Column(Order =1)]
        public int PatientId { get; set; }
        [Key]
        [Column(Order =2)]
        public int DoctorID { get; set; }
    }
    public class RoomPatients
    {
        [Key]
        [Column(Order =1)]
        public int RoomId { get; set; }
        [Key]
        [Column(Order =2)]
        public int PatinetId { get; set; }
    }

}