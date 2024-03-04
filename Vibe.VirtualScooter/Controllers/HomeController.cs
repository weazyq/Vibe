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

    [HttpGet("EndRent")]
    public Result EndRent()
    {
        return VirtualScooterData.Instance.EndRent();
    }

    [HttpGet("ScooterMove")]
    public void ScooterMove(Double x, Double y)
    {
        VirtualScooterData.Instance.MoveTo(x, y);
    }
}