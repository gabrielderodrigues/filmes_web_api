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
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            filme.Id = Id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(BuscarFilmePorId), new { id = filme.Id }, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> BuscarFilmes([FromQuery] int skip = 0, [FromQuery] int take = int.MaxValue)
        {
            return filmes.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarFilmePorId(int id)
        {
            var output = filmes.FirstOrDefault(f => f.Id.Equals(id));

            if (output == null) return NotFound();
            return Ok(output);
        }

        [HttpDelete("{id}")]
        public string DeletarFilmePorId(int id)
        {
            var output = filmes.FirstOrDefault(f => f.Id.Equals(id));
            return filmes.Remove(output) ? $"Filme deletado com sucesso." : "Não foi possível deletar o filme.";
        }
    }
}
