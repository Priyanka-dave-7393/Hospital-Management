namespace HospitalManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HmsDb : DbContext
    {
        public HmsDb()
            : base("name=HmsDb")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomPatients> RoomPatients { get; set; }
        public virtual DbSet<PatientDoctors> PatientDoctors { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
