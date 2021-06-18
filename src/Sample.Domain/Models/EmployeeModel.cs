namespace Sample.Domain.Models
{
    public class EmployeeModel 
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get { return $"{LastName}, {FirstName}"; } }
        public string Email { get; set; }
    }
}
