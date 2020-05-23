using Dapper;
using DapperExamples.Abstraction;
using DapperExamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DapperExamples.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseContext _databaseContext;

        public HomeController(ILogger<HomeController> logger, IDatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            using (var connection = _databaseContext.Connection)
            {
                var data = connection.Query<UserDto>("select * from [AppUser]");
                return View(data);
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new UserDto());
        }

        [HttpPost]
        public IActionResult Add(UserDto model)
        {
            using (var connection = _databaseContext.Connection)
            {
                int count = connection.Execute(@"insert into [AppUser](FirstName, LastName, Age) values (@firstName, @lastName, @age)",
                           new { firstName = model.FirstName, lastName = model.LastName, age = model.Age }
                   );
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            using (var connection = _databaseContext.Connection)
            {
                var data = connection.Query<UserDto>("select * from [AppUser] where Id = @id", new { id = id }).First();
                return View(data);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var connection = _databaseContext.Connection)
            {
                var data = connection.Query<UserDto>("select * from [AppUser] where Id = @id", new { id = id }).First();
                return View(data);
            }
        }

        [HttpPost]
        public IActionResult Edit(UserDto model)
        {
            using (var connection = _databaseContext.Connection)
            {
                int count = connection.Execute(@"Update [AppUser] set FirstName = @firstName, LastName = @lastName, Age = @age where Id = @id",
                           new { firstName = model.FirstName, lastName = model.LastName, age = model.Age, id = model.Id });
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var connection = _databaseContext.Connection)
            {
                var data = connection.Query<UserDto>("Delete from [AppUser] where Id = @id", new { id = id });
                return RedirectToAction("Index");
            }
        }
    }
}
