using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using static RpgApi.Models.Personagem;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagensExercicioController : ControllerBase
    
    {
      private static List<Personagem> personagens = new List<Personagem>()
        {
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo},
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago},
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo},
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago}
        };  

        [HttpGet("GetByNome/{nome}")]
        public ActionResult<Personagem> GetByNome(string nome)
        {
            var personagem = personagens.FirstOrDefault(p => p.Nome.ToLower() == nome.ToLower());

            if (personagem == null)
            {
                return NotFound($"Personagem com o nome '{nome}' não foi encontrado.");
            }

            return Ok(personagem);
        }

        [HttpPost("PostValidacao")]
        public ActionResult<Personagem> PostValidacao(Personagem novoPersonagem)
        {
            if (novoPersonagem.Defesa < 10 || novoPersonagem.Inteligencia > 30)
            {
                return BadRequest("O valor de defesa deve ser maior ou igual a 10 e o valor de inteligência deve ser menor ou igual a 30.");
            }
     
            int novoId = personagens.Count > 0 ? personagens.Max(p => p.Id) + 1 : 1;

            novoPersonagem.Id = novoId;
            personagens.Add(novoPersonagem);

            return CreatedAtAction(nameof(PostValidacao), novoPersonagem);
        }

        [HttpPost("PostValidacaoMago")]
        public ActionResult<Personagem> PostValidacaoMago(Personagem novoPersonagem)
        {
            if (novoPersonagem.Classe == ClasseEnum.Mago && novoPersonagem.Inteligencia < 35)
            {
                return BadRequest("Um personagem do tipo Mago deve ter uma inteligência maior ou igual a 35.");
            }

            // Gere um novo ID para o personagem (exemplo: pegando o maior ID atual e incrementando)
            int novoId = personagens.Count > 0 ? personagens.Max(p => p.Id) + 1 : 1;

            novoPersonagem.Id = novoId;
            personagens.Add(novoPersonagem);

            return CreatedAtAction(nameof(PostValidacaoMago), novoPersonagem);
        }

        [HttpGet("GetClerigoMago")]
        public ActionResult<List<Personagem>> GetClerigoMago()
        {
            var clerigoMagoPersonagens = personagens
                .Where(p => p.Classe == ClasseEnum.Clerigo || p.Classe == ClasseEnum.Mago)
                .OrderByDescending(p => p.PontosVida)
                .ToList();

            return Ok(clerigoMagoPersonagens);
        }

        [HttpGet("GetEstatisticas")]
        public ActionResult<EstatisticasDto> GetEstatisticas()
        {
            var totalPersonagens = personagens.Count;
            var somatorioInteligencia = personagens.Sum(p => p.Inteligencia);

            var estatisticas = new EstatisticasDto
            {
                TotalPersonagens = totalPersonagens,
                SomatorioInteligencia = somatorioInteligencia
            };

            return Ok(estatisticas);
        }

        [HttpGet("GetByClasse/{classeId}")]
        public ActionResult<List<Personagem>> GetByClasse(int classeId)
        {
            var classeEnum = (ClasseEnum)classeId;
            var personagensDaClasse = personagens.Where(p => p.Classe == classeEnum).ToList();

            return Ok(personagensDaClasse);
        }


    

        
    }
}