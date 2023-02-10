using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpErrorException resultException = context.Exception is not HttpErrorException
                ? new HttpErrorException(context.Exception.Message) : (HttpErrorException) context.Exception;

            context.HttpContext.Response.StatusCode = resultException.StatusCode;
            context.Result = new ObjectResult(new
            {
                error = resultException.Message
            });
        }
    }
}
