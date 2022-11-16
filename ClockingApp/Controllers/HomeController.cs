using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClockingApp.Models;
using ClockingApp.CustomServices;
using ClockingApp.Models.ClockingData;
using ClockingApp.Models.CustomViewModels;
using ClockingApp.Settings;

namespace ClockingApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ClockingService _clockingService;
    private readonly IUserSettings _userSettings;

    public HomeController(ILogger<HomeController> logger, ClockingService clockingService, IUserSettings userSettings)
    {
        _logger = logger;
        _clockingService = clockingService;
        _userSettings = userSettings;
    }
    
    public async Task<IActionResult> Index()
    {
        DateTime today = DateTime.Now.Date;
        string username = _userSettings.Username;
        Clocking todaysClocking = await _clockingService._clockingRepo.FindOneAsync(clocking => clocking.Username.Equals(username) && clocking.ClockingDate == today);
  
        return View(todaysClocking);
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

