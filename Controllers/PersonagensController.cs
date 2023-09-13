using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using RpgApi.Data;
using RpgApi.Models;
using Microsoft.EntityFrameworkCore;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagensController : ControllerBase
        {
            private readonly DataContext _context; 
        
            public PersonagensController(DataContext context)
            {
                _context = context;
            }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        try
        {
            Personagem p = await _context.TBPERSONAGENS
                .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

            return Ok(p);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
        [HttpGet("GetAll")]
    public async Task<IActionResult> Get()
    {
        try
        {
            List<Personagem> lista = await _context.TBPERSONAGENS.ToListAsync();
            return Ok(lista);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

    
    [HttpPost]
    
    public async Task<IActionResult> Add(Personagem novoPersonagem)
        {
            try
            {
                if (novoPersonagem.PontosVida > 100)
                {
                    throw new Exception("Pontos de vida não pode ser maior que 100");
                }
                await _context.TBPERSONAGENS.AddAsync(novoPersonagem);
                await _context.SaveChangesAsync();

                return Ok(novoPersonagem.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    [HttpPut]

    public async Task<IActionResult> Update(Personagem novoPersonagem)
    {
        try
        {
            if (novoPersonagem.PontosVida > 100)
            {
                throw new System.Exception("Pontos de vida não pode ser maior que 100");
            }
            _context.TBPERSONAGENS.Update(novoPersonagem);
            int linhasAfetadas = await _context.SaveChangesAsync();

            return Ok(linhasAfetadas);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Personagem pRemover = await _context.TBPERSONAGENS.FirstOrDefaultAsync(p => p.Id == id);

            _context.TBPERSONAGENS.Remove(pRemover);
            int linhasAfetadas = await _context.SaveChangesAsync();
            return Ok(linhasAfetadas);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
        }

    
}
