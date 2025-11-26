using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using DisasterAlleviationFoundation.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace DisasterAlleviationFoundation.Controllers
{
    [Authorize]
    public class VolunteerController : Controller
    {
        // GET: /Volunteer/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: /Volunteer/SubmitSignUp
        [HttpPost]
        public IActionResult SubmitSignUp(string firstName, string lastName, string email, string phone, string skills)
        {
            string connects = "Server=tcp:djpromo250.database.windows.net,1433;Initial Catalog=DJPROMODATABASE;Persist Security Info=False;User ID=djadmin;Password=Unicumantae24;";

            try
            {
                using (SqlConnection connect = new SqlConnection(connects))
                {
                    connect.Open();

                    string sql = @"INSERT INTO Volunteers (FirstName, LastName, Email, Phone, Skills, DateRegistered) 
                                   VALUES (@FirstName, @LastName, @Email, @Phone, @Skills, @DateRegistered)";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, connect))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Skills", skills);
                        cmd.Parameters.AddWithValue("@DateRegistered", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                    connect.Close();
                }
                ViewBag.Message = "Thank you for signing up as a volunteer!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View("SignUp");
        }
        // GET: /Volunteer/ViewVolunteers
        public IActionResult ViewVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();
            string connects = "Server=tcp:djpromo250.database.windows.net,1433;Initial Catalog=DJPROMODATABASE;Persist Security Info=False;User ID=djadmin;Password=Unicumantae24;";

            try
            {
                using (SqlConnection connect = new SqlConnection(connects))
                {
                    connect.Open();
                    string sql = "SELECT * FROM Volunteers ORDER BY DateRegistered DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                volunteers.Add(new Volunteer
                                {
                                    Id = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Phone = reader.GetString(4),
                                    Skills = reader.GetString(5),
                                    DateRegistered = reader.GetDateTime(6)
                                });
                            }
                        }
                    }
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading volunteers: " + ex.Message;
            }

            return View("~/Views/Volunteer/ViewVolunteers.cshtml", volunteers);
        }
    }
}
