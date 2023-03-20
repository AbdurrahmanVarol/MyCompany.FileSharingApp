using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MyCompany.FileSharingApp.Entities.Concrete;

namespace MyCompany.FileSharingApp.MVC.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var error = context.Exception.Message;

            context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel { ErrorMessage = $"{error}" });
        }
    }
}
