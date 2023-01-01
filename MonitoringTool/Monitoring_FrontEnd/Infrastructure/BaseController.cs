using Microsoft.AspNetCore.Mvc;

namespace Monitoring_FrontEnd.Infrastructure
{
    [Route("[controller]/[action]", Name = "[controller]_[action]")]
    public abstract class BaseController : Controller
    {
    }
}
