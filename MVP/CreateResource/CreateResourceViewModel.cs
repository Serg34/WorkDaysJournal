using System.ComponentModel.DataAnnotations;

namespace Furmanov.MVP.CreateResource
{
	public class CreateResourceViewModel
	{
		public int Resource_Id { get; set; }

		[Required(ErrorMessage = "Не заполнено поле 'ФИО'")]
		public string Name { get; set; }

#if DEBUG
		public string Phone { get; set; }
		public string Card { get; set; }
#else
		[Required(ErrorMessage = "Не заполнено поле 'Телефон'")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Не заполнено поле 'Карта'")]
		public string Card { get; set; }
#endif
		public string OfficialSalary { get; set; }
		public bool IsStaff { get; internal set; }
	}
}
