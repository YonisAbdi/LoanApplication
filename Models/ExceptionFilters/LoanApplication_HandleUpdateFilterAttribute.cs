using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo.Models.Repository;

public class LoanApplication_HandleUpdateFiltersAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        base.OnException(context);

        var strLoanApplicationId = context.RouteData.Values["id"] as string;
        if (int.TryParse(strLoanApplicationId, out var loanApplicationId))
        {
            if (!LoanApplicationRepository.Exists(loanApplicationId))
            {
                context.ModelState.AddModelError("LoanApplicationId", "Loan application doesn't exist anymore.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(problemDetails);
            }
        }
    }
}
