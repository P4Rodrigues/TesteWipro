using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Controllers
{
    [Route("moeda/")]
    [ApiController]
    public class MoedaController : ControllerBase
    {
        public readonly ProcessamentoContext context;
        public MoedaController(ProcessamentoContext _context)
        {
            context = _context;
        }

        [HttpGet("GetMoedas")]
        public ActionResult GetMoedas()
        {
            var listaMoedas = context.Moeda.ToList();
            return Ok(listaMoedas);
        }

        [HttpGet("GetMoedaPorPrefixo/{prefixo}")]
        public ActionResult GetMoeda(string prefixo)
        {
            var listaMoedas = context.Moeda.Where(m => m.Prefixo == prefixo).ToList(); ;
            return Ok(listaMoedas);
        }


    }
}
