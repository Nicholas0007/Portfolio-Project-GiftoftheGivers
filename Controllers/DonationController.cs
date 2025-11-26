using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using DisasterAlleviationFoundation.Models; 
using System;
using Microsoft.AspNetCore.Authorization;

namespace DisasterAlleviationFoundation.Controllers
{
    [Authorize]
    public class DonationController : Controller
    {
        // GET: /Donation/DonateGoods
        public IActionResult DonateGoods()
        {
            return View();
        }

        // POST: /Donation/SubmitDonation
        [HttpPost]
        public IActionResult SubmitDonation(string donorName, string category, string description, int quantity)
        {
            string connects = "Server=tcp:djpromo250.database.windows.net,1433;Initial Catalog=DJPROMODATABASE;Persist Security Info=False;User ID=djadmin;Password=Unicumantae24;";

            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection connect = new Microsoft.Data.SqlClient.SqlConnection(connects))
                {
                    connect.Open();

                    string sql = @"INSERT INTO Donations (DonorName, Category, Description, Quantity, DateDonated) 
                                   VALUES (@DonorName, @Category, @Description, @Quantity, @DateDonated)";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, connect))
                    {
                        cmd.Parameters.AddWithValue("@DonorName", donorName);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@DateDonated", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                    connect.Close();
                }
                ViewBag.Message = "Thank you for your generous donation!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View("DonateGoods");
        }
        // GET: /Donation/ViewDonations
public IActionResult ViewDonations()
{
    List<Donation> donations = new List<Donation>();
    string connects = "Server=tcp:djpromo250.database.windows.net,1433;Initial Catalog=DJPROMODATABASE;Persist Security Info=False;User ID=djadmin;Password=Unicumantae24;";

    try
    {
        using (SqlConnection connect = new SqlConnection(connects))
        {
            connect.Open();
            string sql = "SELECT * FROM Donations ORDER BY DateDonated DESC";

            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        donations.Add(new Donation
                        {
                            Id = reader.GetInt32(0),
                            DonorName = reader.GetString(1),
                            Category = reader.GetString(2),
                            Description = reader.GetString(3),
                            Quantity = reader.GetInt32(4),
                            DateDonated = reader.GetDateTime(5)
                        });
                    }
                }
            }
            connect.Close();
        }
    }
    catch (Exception ex)
    {
        ViewBag.Error = "Error loading donations: " + ex.Message;
    }

    return View("~/Views/Donation/ViewDonations.cshtml", donations);
}
    }
}
