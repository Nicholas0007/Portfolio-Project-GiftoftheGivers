using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;   

namespace DisasterAlleviationFoundation.Controllers
{
    [Authorize]
    public class DisasterController : Controller
    {


        // This action method handles GET requests to /Disaster/Report
        // It simply returns the View (the form we just made)
        public IActionResult Report()
        {
            return View();
        }
        // This action method handles POST requests from the form
        [HttpPost] // This attribute is crucial - it means this method only accepts form submissions
        public IActionResult SubmitReport(string location, string description, string urgency)
        {
            // This is your connection string - we'll put it here for now
            string connects = "Server=tcp:djpromo250.database.windows.net,1433;Initial Catalog=DJPROMODATABASE;Persist Security Info=False;User ID=djadmin;Password=Unicumantae24;";

            try
            {
                using (SqlConnection connect = new SqlConnection(connects))
                {
                    connect.Open();

                    // Your SQL command to INSERT the data into a table
                    string sql = @"INSERT INTO DisasterReports (Location, Description, Urgency, DateReported) 
                           VALUES (@Location, @Description, @Urgency, @DateReported)";

                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        // Add parameters to prevent SQL injection - VERY IMPORTANT
                        cmd.Parameters.AddWithValue("@Location", location);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Urgency", urgency);
                        cmd.Parameters.AddWithValue("@DateReported", DateTime.Now); // Adds current date/time

                        cmd.ExecuteNonQuery(); // Run the INSERT command
                    }
                    connect.Close();
                }
                ViewBag.Message = "Disaster report submitted successfully! Thank you.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View("~/Views/Disaster/ViewReports.cshtml", ViewReports);
        }

        // GET: /Disaster/ViewReports
        public IActionResult ViewReports()
        {
            List<DisasterReport> reports = new List<DisasterReport>(); // Create an empty list to hold our data
            string connects = "Server=tcp:djpromo250.database.windows.net,1433;Initial Catalog=DJPROMODATABASE;Persist Security Info=False;User ID=djadmin;Password=Unicumantae24;";

            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection connect = new Microsoft.Data.SqlClient.SqlConnection(connects))
                {
                    connect.Open();
                    string sql = "SELECT * FROM DisasterReports ORDER BY DateReported DESC"; // SQL to get all reports, newest first

                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        // Execute the command and get a data reader
                        using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Loop through each row the reader returns
                            while (reader.Read())
                            {
                                // For each row, create a new DisasterReport object and fill it with data
                                reports.Add(new DisasterReport
                                {
                                    Id = reader.GetInt32(0), // Assuming first column is Id
                                    Location = reader.GetString(1), // Second column is Location
                                    Description = reader.GetString(2), // Third column is Description
                                    Urgency = reader.GetString(3), // Fourth column is Urgency
                                    DateReported = reader.GetDateTime(4) // Fifth column is DateReported
                                });
                            }
                        }
                    }
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading reports: " + ex.Message;
            }

            // Pass the list of reports to the View
            return View(reports);
        }

    }
}
