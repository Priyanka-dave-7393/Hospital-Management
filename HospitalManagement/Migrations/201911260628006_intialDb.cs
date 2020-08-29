namespace HospitalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Adminid = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Adminid);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        DoctorCharge = c.Int(nullable: false),
                        RoomCharge = c.Int(nullable: false),
                        Dischargedate = c.DateTime(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.DoctorId)
                .Index(t => t.RoomId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(),
                        Address = c.String(),
                        MobileNo = c.String(),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 30),
                        Lastname = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 250),
                        Patient_Dob = c.DateTime(nullable: false),
                        MobileNo = c.String(nullable: false, maxLength: 10),
                        DateOfAdmit = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        ConfirmPassword = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PatientId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RoomType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.PatientDoctors",
                c => new
                    {
                        PatientId = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PatientId, t.DoctorID });
            
            CreateTable(
                "dbo.RoomPatients",
                c => new
                    {
                        RoomId = c.Int(nullable: false),
                        PatinetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoomId, t.PatinetId });
            
            CreateTable(
                "dbo.PatientDoctors1",
                c => new
                    {
                        Patient_PatientId = c.Int(nullable: false),
                        Doctor_DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patient_PatientId, t.Doctor_DoctorId })
                .ForeignKey("dbo.Patients", t => t.Patient_PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId, cascadeDelete: true)
                .Index(t => t.Patient_PatientId)
                .Index(t => t.Doctor_DoctorId);
            
            CreateTable(
                "dbo.RoomPatients1",
                c => new
                    {
                        Room_RoomId = c.Int(nullable: false),
                        Patient_PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Room_RoomId, t.Patient_PatientId })
                .ForeignKey("dbo.Rooms", t => t.Room_RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_PatientId, cascadeDelete: true)
                .Index(t => t.Room_RoomId)
                .Index(t => t.Patient_PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomPatients1", "Patient_PatientId", "dbo.Patients");
            DropForeignKey("dbo.RoomPatients1", "Room_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.PatientDoctors1", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.PatientDoctors1", "Patient_PatientId", "dbo.Patients");
            DropForeignKey("dbo.Bills", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Bills", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.RoomPatients1", new[] { "Patient_PatientId" });
            DropIndex("dbo.RoomPatients1", new[] { "Room_RoomId" });
            DropIndex("dbo.PatientDoctors1", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.PatientDoctors1", new[] { "Patient_PatientId" });
            DropIndex("dbo.Bills", new[] { "PatientId" });
            DropIndex("dbo.Bills", new[] { "RoomId" });
            DropIndex("dbo.Bills", new[] { "DoctorId" });
            DropTable("dbo.RoomPatients1");
            DropTable("dbo.PatientDoctors1");
            DropTable("dbo.RoomPatients");
            DropTable("dbo.PatientDoctors");
            DropTable("dbo.Rooms");
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.Bills");
            DropTable("dbo.Admins");
        }
    }
}
