﻿using FilmesAPI.Models;
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
        public IEnumerable<Filme> BuscarFilmes()
        {
            return filmes;
        }

        [HttpGet("{idFilme}")]
        public Filme? BuscarFilmePorId(int idFilme)
        {
            var output = filmes.FirstOrDefault(f => f.Id.Equals(idFilme));
            return output;
        }
    }
}
