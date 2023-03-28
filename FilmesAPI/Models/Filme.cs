﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Required(ErrorMessage = "O título do filme é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero do filme é obrigatório.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "O filme precisa ter um diretor.")]
        public string Diretor { get; set; }

        [Required(ErrorMessage = "A duração do filme é obrigatório.")]
        [Range(70, 600, ErrorMessage = "A duração deve ter entre 70 a 600 minutos.")]
        public int Duracao { get; set; }
    }
}
