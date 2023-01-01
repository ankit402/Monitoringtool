using Microsoft.AspNetCore.Mvc.Filters;

namespace Monitoring_FrontEnd.Infrastructure.ErrorHandling
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);
    }
}