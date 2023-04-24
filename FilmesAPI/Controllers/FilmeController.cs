using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dto;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscarFilmePorId), new { id = filme.Id }, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> BuscarFilmes([FromQuery] int skip = 0, [FromQuery] int take = int.MaxValue)
        {
            return _context.Filmes.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarFilmePorId(int id)
        {
            var output = _context.Filmes.FirstOrDefault(f => f.Id.Equals(id));

            if (output == null) return NotFound();
            return Ok(output);
        }
    }
}
