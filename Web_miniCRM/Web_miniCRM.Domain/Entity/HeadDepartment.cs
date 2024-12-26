namespace Web_miniCRM.Domain.Entity
{
    public class HeadDepartment
    {
        public int HeadDepartmentId { get; set; }
        public int? DepartmentNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NumberPhone { get; set; }
        public string? Email { get; set; }
        public virtual List<Manager>? Managers { get; set; } = new List<Manager>();
    }
}
