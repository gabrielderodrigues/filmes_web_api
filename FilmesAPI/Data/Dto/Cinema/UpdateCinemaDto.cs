using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dto.Cinema
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Nome { get; set; }
    }
}
