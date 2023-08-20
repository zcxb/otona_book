using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using OtonaBookApi.DataAccess;

namespace OtonaBookApi.Controllers
{
    [ApiController]
    public abstract class AreaControllerBase : ControllerBase
    {
        public ILogger Logger { get; set; }

        public OtonaBookContext DbContext { get; set; }
    }

    public class AreaControllerBaseActivator : IControllerActivator
    {
        public object Create(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();
            var controller = context.HttpContext.RequestServices.GetRequiredService(controllerType);

            if (controller is AreaControllerBase controllerBase)
            {
                controllerBase.Logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(controllerType);
                controllerBase.DbContext = context.HttpContext.RequestServices.GetRequiredService<OtonaBookContext>();
            }

            return controller;
        }

        public void Release(ControllerContext context, object controller)
        {
        }
    }
}