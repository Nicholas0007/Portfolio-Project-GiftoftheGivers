using System.Diagnostics;
using DisasterAlleviationFoundation_01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation_01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //connection string
            string connects = "Server=tcp:djpromo250.database.windows.net,1433;Initial Catalog=DJPROMODATABASE;Persist Security Info=False;User ID=djadmin;Password=Unicumantae24;";
            //try and catch he connection

            try
            {

                //using function
                using (SqlConnection connect = new SqlConnection(connects))
                {
                    //open connection db
                    connect.Open();

                    //store


                    //then code inside here
                    Console.WriteLine("connected");

                    connect.Close();
                }


            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);
            }



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
