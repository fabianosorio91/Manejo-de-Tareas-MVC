using System.ComponentModel.DataAnnotations;

namespace TareasMVC.Models
{
	public class PasoCrearDTO
	{
		[Required]
		public string Desccripcion { get; set; }
		public bool Realizado { get; set; }

	}
}
