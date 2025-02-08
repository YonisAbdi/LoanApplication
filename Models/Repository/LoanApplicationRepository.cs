using static WebbAPI.Models.BorrowerApplicationsModels;
using static WebbAPI.Models.LoanApplicationsModels;

namespace WebAPIDemo.Models.Repository
{
    public static class LoanApplicationRepository
    {
        private static List<LoanApplication> loanApplications = new List<LoanApplication>();
        private static int nextLoanApplicationId = 1;
        private static int nextBorrowerId = 1;

        static LoanApplicationRepository()
        {
            // Populate the list with initial data
            loanApplications.AddRange(new List<LoanApplication>
            {
                new LoanApplication
                {
                    Id = nextLoanApplicationId++,
                    Borrower = new Borrower { Id = nextBorrowerId++, FirstName = "John", LastName = "Doe", Income = 50000 },
                    Amount = 10000, Status = "Pending", Date = DateTime.Now.AddDays(-10)
                },
                new LoanApplication
                {
                    Id = nextLoanApplicationId++,
                    Borrower = new Borrower { Id = nextBorrowerId++, FirstName = "Jane", LastName = "Smith", Income = 60000 },
                    Amount = 15000, Status = "Approved", Date = DateTime.Now.AddDays(-8)
                },
                new LoanApplication
                {
                    Id = nextLoanApplicationId++,
                    Borrower = new Borrower { Id = nextBorrowerId++, FirstName = "Alice", LastName = "Johnson", Income = 55000 },
                    Amount = 5000, Status = "Rejected", Date = DateTime.Now.AddDays(-15)
                },
                new LoanApplication
                {
                    Id = nextLoanApplicationId++,
                    Borrower = new Borrower { Id = nextBorrowerId++, FirstName = "Bob", LastName = "Brown", Income = 45000 },
                    Amount = 20000, Status = "Pending", Date = DateTime.Now.AddDays(-20)
                },
                new LoanApplication
                {
                    Id = nextLoanApplicationId++,
                    Borrower = new Borrower { Id = nextBorrowerId++, FirstName = "Carol", LastName = "Davis", Income = 62000 },
                    Amount = 25000, Status = "Approved", Date = DateTime.Now.AddDays(-5)
                }
            });
        }

        public static List<LoanApplication> GetAll()
        {
            return loanApplications;
        }

        public static LoanApplication? GetById(int id)
        {
            return loanApplications.FirstOrDefault(x => x.Id == id);
        }

        public static LoanApplication? GetLoanApplicationByProperties(int? borrowerId, decimal? amount, string? status)
        {
            return loanApplications.FirstOrDefault(x =>
                borrowerId.HasValue &&
                x.Borrower != null &&
                x.Borrower.Id == borrowerId.Value &&
                amount.HasValue &&
                x.Amount == amount.Value &&
                !string.IsNullOrWhiteSpace(status) &&
                !string.IsNullOrWhiteSpace(x.Status) &&
                x.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        public static void Create(LoanApplication application)
        {
            application.Id = nextLoanApplicationId++;
            if (application.Borrower != null)
            {
                application.Borrower.Id = nextBorrowerId++;
            }
            loanApplications.Add(application);
        }

        public static void UpdateLoanApplication(LoanApplication updatedApplication)
        {
            var applicationToUpdate = loanApplications.FirstOrDefault(x => x.Id == updatedApplication.Id);
            if (applicationToUpdate != null)
            {
                // Update each property of the loan application carefully
                applicationToUpdate.Amount = updatedApplication.Amount;
                applicationToUpdate.Status = updatedApplication.Status;
                applicationToUpdate.Date = updatedApplication.Date;

                // Update Borrower details
                if (updatedApplication.Borrower != null)
                {
                    if (applicationToUpdate.Borrower == null)
                    {
                        applicationToUpdate.Borrower = new Borrower();
                    }
                    applicationToUpdate.Borrower.FirstName = updatedApplication.Borrower.FirstName;
                    applicationToUpdate.Borrower.LastName = updatedApplication.Borrower.LastName;
                    applicationToUpdate.Borrower.Income = updatedApplication.Borrower.Income;
                }
            }
            else
            {
                throw new KeyNotFoundException($"No loan application found with ID {updatedApplication.Id}");
            }
        }
        public static void Delete(int id)
        {
            var application = GetById(id);
            if (application != null)
            {
                loanApplications.Remove(application);
            }
        }

        public static bool Exists(int id)
        {
            return loanApplications.Any(x => x.Id == id);
        }

        // Get loan applications by status
        public static List<LoanApplication> GetByStatus(string status)
        {
            return loanApplications.Where(x => x.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Get loan applications by borrower's ID
        public static List<LoanApplication> GetByBorrowerId(int borrowerId)
        {
            return loanApplications.Where(x => x.Borrower.Id == borrowerId).ToList();
        }
    }
}

