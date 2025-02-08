using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPIDemo.Models.Filters.ActionFilters;
using WebAPIDemo.Models.Repository;
using static WebbAPI.Models.LoanApplicationsModels;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanApplicationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(LoanApplicationRepository.GetAll());
        }

        [HttpGet("{id}")]
        [ValidateLoanApplicationIdFilter]
        public IActionResult GetById(int id)
        {
            var application = LoanApplicationRepository.GetById(id);
            if (application == null) return NotFound();
            return Ok(application);
        }

        [HttpGet("status/all")]
        public IActionResult GetAllApplicantsStatus()
        {
            var allApplications = LoanApplicationRepository.GetAll();
            if (allApplications == null || !allApplications.Any())
            {
                return NotFound("No loan applications found.");
            }

            var statusList = allApplications.Select(app => new
            {
                ApplicantId = app.Id,
                ApplicantName = $"{app.Borrower.FirstName} {app.Borrower.LastName}",
                Status = app.Status
            });

            return Ok(statusList);
        }



        [HttpPost]
        [ValidateCreateLoanApplicationFilter]
        public IActionResult Create([FromBody] LoanApplication application)
        {
            var existingApplication = LoanApplicationRepository.GetLoanApplicationByProperties(
                application.Borrower?.Id, application.Amount, application.Status);

            if (existingApplication != null)
            {
                return BadRequest("A similar loan application already exists.");
            }

            LoanApplicationRepository.Create(application);
            return CreatedAtAction(nameof(GetById), new { id = application.Id }, application);
        }


        [HttpPut("{id}")]
        [ValidateUpdateLoanApplicationFilter]
        [LoanApplication_HandleUpdateFilters]
        public IActionResult Update(int id, [FromBody] LoanApplication application)
        {
            if (application.Id != id) return BadRequest();

            var existingApplication = LoanApplicationRepository.GetById(id);
            if (existingApplication == null) return NotFound();

            LoanApplicationRepository.UpdateLoanApplication(application);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ValidateLoanApplicationIdFilter]
        public IActionResult Delete(int id)
        {
            var application = LoanApplicationRepository.GetById(id);
            if (application == null) return NotFound();

            LoanApplicationRepository.Delete(id);
            return NoContent();
        }
    }
}
