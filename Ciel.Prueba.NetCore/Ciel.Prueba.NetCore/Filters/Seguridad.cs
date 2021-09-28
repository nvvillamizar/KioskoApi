using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ciel.Prueba.NetCore.Filters
{
    public class Seguridad : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var usuario = context.HttpContext.Session.GetString("usuario");
            if (usuario == null) context.Result = new RedirectResult("Login");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var usuario = context.HttpContext.Session.GetString("usuario");
            if (usuario == null) context.Result = new RedirectResult("Login");
        }
    }
}
