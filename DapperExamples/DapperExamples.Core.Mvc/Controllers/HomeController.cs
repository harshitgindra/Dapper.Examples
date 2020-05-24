#region USING
using Dapper;
using DapperExamples.Abstraction;
using DapperExamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
#endregion

namespace DapperExamples.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseStrategy _databaseContext;

        public HomeController(ILogger<HomeController> logger, IDatabaseStrategy databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            using (var connection = _databaseContext.Connection)
            {
                //***
                //***  Execute query to get all data from the table
                //***
                var data = connection.Query<AppUserDto>("select * from [AppUser]");
                return View(data);
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new AppUserDto());
        }

        [HttpPost]
        public IActionResult Add(AppUserDto model)
        {
            using (var connection = _databaseContext.Connection)
            {
                //***
                //***  Add new user to the database
                //***
                _ = connection.Execute(@"insert into [AppUser](FirstName, LastName, Age) values (@FirstName, @LastName, @Age)", model);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            using (var connection = _databaseContext.Connection)
            {
                //***
                //***  Get user info from the table based on ID
                //***
                var data = connection.QueryFirstOrDefault<AppUserDto>("select * from [AppUser] where Id = @id", new { id = id });
                return View(data);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var connection = _databaseContext.Connection)
            {
                //***
                //***  Get user info from the table based on ID
                //***
                var data = connection.QueryFirstOrDefault<AppUserDto>("select * from [AppUser] where Id = @id", new { id = id });
                return View(data);
            }
        }

        [HttpPost]
        public IActionResult Edit(AppUserDto model)
        {
            using (var connection = _databaseContext.Connection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //***
                        //***  Update entity properties in the database
                        //***
                        _ = connection.Execute(@"Update [AppUser] set FirstName = @FirstName, LastName = @LastName, Age = @Age where Id = @Id", model);
                        //***
                        //*** Commit the changes to the dB
                        //***
                        transaction.Commit();
                    }
                    catch
                    {
                        //***
                        //*** Rollback the changes added as paart of the transaction
                        //***
                        transaction.Rollback();
                    }

                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var connection = _databaseContext.Connection)
            {
                //***
                //***  Delete the record fro the database based on ID
                //***
                var data = connection.Query<AppUserDto>("Delete from [AppUser] where Id = @id", new { id = id });
                return RedirectToAction("Index");
            }
        }
    }
}
