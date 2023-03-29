using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int Id = 1;

        [HttpPost]
        public string AdicionaFilme([FromBody] Filme filme)
        {
            filme.Id = Id++;
            filmes.Add(filme);
            return $"'{filme.Titulo}' adicionado com sucesso!"; 
        }

        [HttpGet]
        public IEnumerable<Filme> BuscarFilmes([FromQuery] int skip = 0, [FromQuery] int take = int.MaxValue)
        {
            return filmes.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public Filme? BuscarFilmePorId(int id)
        {
            var output = filmes.FirstOrDefault(f => f.Id.Equals(id));
            return output;
        }

        [HttpDelete("{id}")]
        public string DeletarFilmePorId(int id)
        {
            var filme = BuscarFilmePorId(id);
            return filmes.Remove(filme) ? $"Filme deletado com sucesso." : "Não foi possível deletar o filme.";
        }
    }
}
