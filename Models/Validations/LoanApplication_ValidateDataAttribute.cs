using System.ComponentModel.DataAnnotations;
using static WebbAPI.Models.LoanApplicationsModels;

public class LoanApplication_ValidateDataAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var loanApplication = validationContext.ObjectInstance as LoanApplication;

        if (loanApplication != null)
        {
            // Validate Amount - Example: Loan amount must be positive
            if (loanApplication.Amount <= 0)
            {
                return new ValidationResult("Loan amount must be greater than zero.");
            }

            var validStatuses = new List<string> { "Submitted", "Approved", "Rejected" };
            if (!validStatuses.Contains(loanApplication.Status))
            {
                return new ValidationResult("Invalid status. Status must be 'Submitted', 'Approved', or 'Rejected'.");
            }
        }

        return ValidationResult.Success;
    }
}
