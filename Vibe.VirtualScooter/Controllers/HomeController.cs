using Microsoft.AspNetCore.Mvc;
using Vibe.Tools.Result;
using Vibe.VirtualScooter.Modules;

public class HomeController : Controller
{
    public HomeController() { }


    [HttpGet("CheckScooterAvailability")]
    public Result CheckScooterAvailability()
    {
        return VirtualScooterData.Instance.CheckScooterAvailability();
    }

    [HttpGet("ScooterMove")]
    public void ScooterMove(Double x, Double y)
    {
        VirtualScooterData.Instance.MoveTo(x, y);
    }
}