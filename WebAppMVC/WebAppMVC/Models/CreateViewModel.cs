using System.ComponentModel.DataAnnotations;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace WebAppMVC.Models
{
	[Serializable]
	public class CreateViewModel
	{
		[Required(ErrorMessage = "Введите имя")]
		[MinLength(2, ErrorMessage = "Минимальная длина должна быть больше 2 символов")]
		[MaxLength(50, ErrorMessage = "Максимальная длина должна быть не более 50 символов")]
        public string Name { get; set; }
	}
}
