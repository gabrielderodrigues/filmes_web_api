using FilmesAPI.Data.Dto.Sessao;

namespace FilmesAPI.Data.Dto
{
    public class ReadFilmeDto
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string Diretor { get; set; }
        public int Duracao { get; set; }
        public DateTime HoraConsulta { get; set; } = DateTime.Now;
        public ICollection<ReadSessaoDto> Sessoes { get; set; }
    }
}
