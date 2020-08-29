using HospitalManagement.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Controllers
{
    [Authorize]
    public class MasterDetailsController : Controller
    {
        // GET: MasterDetails
        private HmsDb db = new HmsDb();
        public ActionResult Index()
        {
             var bills = GetBill();
            var doctors = GetDoctor();
            var patients = GetPatient();


            dynamic model = new ExpandoObject();
            model.Bill = bills;
            model.Doctor = doctors;
            model.Patient = patients;

            return View(model);
        }

       
        
        public List<Bill> GetBill()
        {
            
            List<Bill> bills = new List<Bill>();
            string query = "SELECT b.DoctorCharge,b.RoomCharge,b.Dischargedate FROM Bills b join Patients p on p.PatientId=b.PatientId where p.Email='"+ Session["email"].ToString() + "'";
            string constr = ConfigurationManager.ConnectionStrings["HmsDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            bills.Add(new Bill
                            {
                                DoctorCharge = int.Parse(sdr["DoctorCharge"].ToString()),
                                RoomCharge = int.Parse(sdr["RoomCharge"].ToString()),
                                Dischargedate = DateTime.Parse(sdr["Dischargedate"].ToString())
                            }) ;
                        }
                        con.Close();
                        return bills;
                    }
                }
            }
        }
        public List<Doctor> GetDoctor()
        {
            string eml = Session["email"].ToString();
            List<Doctor> doctors = new List<Doctor>();
            string query = "SELECT d.Firstname,d.Lastname,d.MobileNo FROM Doctors d join PatientDoctors pd on pd.DoctorId=d.DoctorId join Patients p on p.PatientId=pd.PatientId where p.Email='"+eml+"'";
            string constr = ConfigurationManager.ConnectionStrings["HmsDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            doctors.Add(new Doctor
                            {
                                Firstname = sdr["Firstname"].ToString(),
                                Lastname = sdr["Lastname"].ToString(),
                                MobileNo=sdr["MobileNo"].ToString()
                            });
                        }
                        con.Close();
                        return doctors;
                    }
                }
            }
        }
        public List<Patient> GetPatient()
        {
            /* string data = Session["email"].ToString();

            
            List<Patient> patients = new List<Patient>();
            using (var context = new HmsDb())
            {
                var patientdata = db.Patients.Where(x => x.Email == data).Select(x=>x.Firstname);
                Console.WriteLine(patientdata);
            }
            Console.WriteLine(patients);*/
            string eml = Session["email"].ToString();
            List<Patient> patients = new List<Patient>();
            string query = "SELECT Firstname,Lastname,MobileNo,DateOfAdmit FROM Patients where Email='"+eml+"'";
             string constr = ConfigurationManager.ConnectionStrings["HmsDb"].ConnectionString;
             using (SqlConnection con = new SqlConnection(constr))
             {
                 using (SqlCommand cmd = new SqlCommand(query))
                 {
                     cmd.Connection = con;
                     con.Open();
                     using (SqlDataReader sdr = cmd.ExecuteReader())
                     {
                         while (sdr.Read())
                         {
                             patients.Add(new Patient
                             {
                                 Firstname = sdr["Firstname"].ToString(),
                                 Lastname = sdr["Lastname"].ToString(),
                                 DateOfAdmit = DateTime.Parse(sdr["DateOfAdmit"].ToString()),
                                 // MobileNo=int.Parse(sdr["MobileNo"].ToString())),
                             });
                         }
                         con.Close();
                         return patients;
                     }
                 }
             }
            
        }

    }
}