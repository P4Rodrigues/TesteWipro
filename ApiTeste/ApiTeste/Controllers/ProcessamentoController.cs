using Dominio;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTeste.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProcessamentoController : ControllerBase
    {
        private readonly IRepositorio repo;

        public ProcessamentoController(IRepositorio _repo)
        {
            repo = _repo;
        }

        [HttpGet("GetItemFila")]
        public async Task<IActionResult> GetItemFila()
        {
            try
            {
                var Processamento = await repo.GetUltimoProcessamento();

                return Ok(Processamento);
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao consultar processamento: {ex}");
            }
                
        }

        [HttpPost("AddItemFila")]
        public async Task<IActionResult> AddItemFila([FromBody] Processamento[] dados)
        {
            try
            {
                foreach (var model in dados)
                {
                    if (model.Data_inicio == DateTime.MinValue || model.Data_inicio == DateTime.MinValue || model.Moeda == null)
                    {
                        return BadRequest("Necessário preencher todos os campos!.");
                    }
                }

                foreach (var model in dados)
                {
                    model.Status = 0;
                    repo.Add(model);
                }
                                    
                if(await repo.SaveChangeAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao processar: {ex}");
            }
            return BadRequest($"Não Processado");
        }

    }
}
