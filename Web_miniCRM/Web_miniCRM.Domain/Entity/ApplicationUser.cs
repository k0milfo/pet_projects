using Microsoft.AspNetCore.Identity;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.Domain.Entity
{
	public class ApplicationUser : IdentityUser
	{
		public int? ManagerId { get; set; }
		public Manager? Manager { get; set; }

		public int? HeadDepartmentId { get; set; }
		public HeadDepartment? HeadDepartment { get; set; }
	}
}