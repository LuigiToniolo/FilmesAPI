using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        //Para acessar o banco, utilizaremos o "FilmesContexto"
        private FilmeContext _context;

        //para inicializar ^
        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        //Esse código está comentado pois o banco ja vai colocar os id's normalmente
        //private static List<Filme> filmes = new List<Filme>(); // --> Declarando a lista filmes
        //private static int id = 1; // --> Adicionando id para diferenciar cada array dos objetos

        [HttpPost] //quando queremos criar um recurso novo em nosso sistema. "POST" -> padrão para criação de algo novo.
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto) // void --> não retorna nada. | Preciso indicar que filme está vindo do corpo (body) da requisição feita no postman
        {
            //Tranasformando o filmeDto em um filme:
            Filme filme = new Filme
            {
                Titulo = filmeDto.Titulo,
                Genero = filmeDto.Genero,
                Diretor = filmeDto.Diretor,
                Duracao = filmeDto.Duracao,
            };

            //Adicionando o filme no banco
            _context.Filmes.Add(filme);
            _context.SaveChanges(); // --> Guardar o estado da operação, sem isso nós damos um POST e não "grava"

            //filme.Id = id++;
            //filmes.Add(filme); //Irá guardar o filmes através de uma lista "filmes"
            return CreatedAtAction(nameof(RecuperarFilmesPorId), new { id = filme.Id }, filme); //queremos que a lógica do "recuperafilmesporid" seja executada no primeiro parâmetro, e no segundo estamos passando um id que é igual ao id do filme que acabamos de criar
        }

        [HttpGet] //Quando queremos recuperar um recurso do sistema
        public IActionResult RecuperarFilmes()
        {
            //return Ok(filmes);
            return Ok(_context.Filmes);
        }

        [HttpGet("{id}")] //Colocamos esse id dentro do parenteses para diferenciarmos os dois http get
        //Quando for enviar o método GET no postman, colocar /filmes/"id" que quer 
        public IActionResult RecuperarFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            //Filme filme = filmes.FirstOrDefault(filme => filme.Id == id);  // Para cada filme que a gente tiver em nossa lista de filmes. Se o id desse filme for igual ao id que eu recebi, irá retornar filme
            // Fazer aparecer um erro 404 quando coloca um id que não existe:
            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

        //para alterar ou deletar alguma coisa dentro do nosso banco

        [HttpPut("{id}")] //Verbo específico para ATUALIZAÇÕES do nosso sistema
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null)
            {
                return NotFound();
            }
            filme.Titulo = filmeDto.Titulo;
            filme.Genero = filmeDto.Genero;
            filme.Diretor = filmeDto.Diretor;
            filme.Duracao = filmeDto.Duracao;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme (int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
