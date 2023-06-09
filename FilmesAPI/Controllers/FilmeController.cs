﻿using AutoMapper;
using Azure;
using FilmesAPI.Data;
using FilmesAPI.Data.Dto;
using FilmesAPI.Data.Dto.Filme;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        /// <summary>
        /// Construtor da Controller Filme.
        /// </summary>
        /// <param name="context">Integração com banco de dados.</param>
        /// <param name="mapper">Realiza mapeamento das entidades.</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a alteração seja feita com sucesso.</response>
        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um filme no banco de dados.
        /// </summary>
        /// <param name="filmeDto">Objeto necessário para criação de um filme.</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscarFilmePorId), new { id = filme.Id }, filme);
        }

        /// <summary>
        /// Busca todos os filmes no banco de dados.
        /// </summary>
        /// <param name="filmeDto">Objeto necessário para criação de um filme.</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso.</response>
        [HttpGet]
        public IEnumerable<ReadFilmeDto> BuscarFilmes([FromQuery] int skip = 0, [FromQuery] int take = int.MaxValue, [FromQuery] string? nomeCinema = null)
        {
            if (nomeCinema == null)
            {
                return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take).ToList());
            }

            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes
                .Skip(skip)
                .Take(take)
                .Where(filme => filme.Sessoes
                .Any(sessao => sessao.Cinema.Nome == nomeCinema)));
        }

        /// <summary>
        /// Busca filme no banco de dados.
        /// </summary>
        /// <param name="id">Parâmetro necessário para busca do filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a busca seja feita com sucesso.</response>
        [HttpGet("{id}")]
        public IActionResult BuscarFilmePorId(int id)
        {
            var output = _context.Filmes.FirstOrDefault(f => f.Id.Equals(id));

            if (output == null) return NotFound();
            
            var filmeDto = _mapper.Map<ReadFilmeDto>(output);

            return Ok(filmeDto);
        }

        /// <summary>
        /// Atualiza filme no banco de dados.
        /// </summary>
        /// <param name="id">Parâmetro necessário para busca do filme.</param>
        /// <param name="filmeDto">Objeto necessário para alteração do filme.</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a alteração seja feita com sucesso.</response>
        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = BuscarFilmePorId(id);

            if (filme == null) return NotFound();

            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return Ok(filme);
        }

        /// <summary>
        /// Atualiza filme no banco de dados.
        /// </summary>
        /// <param name="id">Parâmetro necessário para busca do filme.</param>
        /// <param name="patch">Objeto necessário para alteração do filme.</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a alteração seja feita com sucesso.</response>
        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmePatch(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = BuscarFilmePorId(id);

            if (filme == null) return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar)) return ValidationProblem(ModelState);

            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return Ok(filme);
        }

        /// <summary>
        /// Deleta filme pelo Id.
        /// </summary>
        /// <param name="id">Parâmetro necessário para deletar filme no banco de dados.</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a remoção seja feita com sucesso.</response>
        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = BuscarFilmePorId(id);

            if (filme == null) return NotFound();

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
