using FilmesAPI.Data.Dto.Endereco;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dto.Cinema
{
    public class ReadCinemaDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public ReadEnderecoDto Endereco { get; set; }
    }
}
