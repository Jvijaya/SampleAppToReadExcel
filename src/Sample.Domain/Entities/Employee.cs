namespace Sample.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
