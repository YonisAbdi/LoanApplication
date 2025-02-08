using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo.Models.Repository;

namespace WebAPIDemo.Models.Filters.ActionFilters
{
    public class ValidateLoanApplicationIdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (context.ActionArguments["id"] is not int id)
            {
                context.ModelState.AddModelError("Id", "Loan application ID is invalid.");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            if (!LoanApplicationRepository.Exists(id))
            {
                context.ModelState.AddModelError("Id", "Loan application does not exist.");
                context.Result = new NotFoundObjectResult(context.ModelState);
            }
        }
    }
}
