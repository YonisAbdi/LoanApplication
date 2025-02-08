namespace WebbAPI.Models
{
    public class BorrowerApplicationsModels
    {
        public class Borrower
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal Income { get; set; }
        }
    }
}
