
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key] //O nosso id é a chave de autenticação, portanto usaremos a chave "key"
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campos do título é obrigatório")] // -> Quando colocar uma string vazia no título, o postman irá mandar um Bad request error (400) informando que esse campo tem que ser preenchido
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo Diretor é obrigatório")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "O gênero não pode passar de 30 caracteres")]
        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "A duração deve ser menor que 600 minutos")]
        public int Duracao { get; set; }
    }
}
