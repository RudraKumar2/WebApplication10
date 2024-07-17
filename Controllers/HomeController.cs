using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Postgres.Data;

using ReadDB.Models;
namespace ReadDB.Controllers;
public class HomeController : Controller
{
    private readonly DataAccess dataAccess;
    public HomeController(DataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }
    public IActionResult Index()
    {
        List<YourModel> data = dataAccess.GetData();
        return View(data);
    }
}