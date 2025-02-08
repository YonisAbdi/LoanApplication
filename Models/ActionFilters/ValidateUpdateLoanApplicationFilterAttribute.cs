using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static WebbAPI.Models.LoanApplicationsModels;

namespace WebAPIDemo.Models.Filters.ActionFilters
{
    public class ValidateUpdateLoanApplicationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

    }
}
