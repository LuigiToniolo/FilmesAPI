// Esse código vai ser voltado para quando a gente quer criar um filme no nosso sistema 

using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateFilmeDto
    {
        [Required(ErrorMessage = "O campos do título é obrigatório")] 
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo Diretor é obrigatório")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "O gênero não pode passar de 30 caracteres")]
        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "A duração deve ser menor que 600 minutos")]
        public int Duracao { get; set; }
    }
}
