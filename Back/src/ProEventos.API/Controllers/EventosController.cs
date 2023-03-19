using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        
        public IEventoService EventoService { get; }

        public EventosController(IEventoService eventoService)
        {
            this.EventoService = eventoService;
            
          
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                 var eventos = await EventoService.GetAllEventosAsync(true);
                 if(eventos == null) return NotFound("Nenhum evento encontrado");

                 return Ok(eventos);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByid(int id)
        {
            try
            {
                 var evento = await EventoService.GetEventoByIdAsync(id, true);
                 if(evento == null) return NotFound("Nenhum evento encontrado");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<ActionResult> GetBytema(string tema)
        {
            try
            {
                 var evento = await EventoService.GetAllEventosByTemaAsync(tema, true);
                 if(evento == null) return NotFound("Eventos por tema não encontrados");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                 var evento = await EventoService.AddEventos(model);
                 if(evento == null) return BadRequest("Erro ao tentar adicionar evento");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                 var evento = await EventoService.UpdateEvento(id, model);
                 if(evento == null) return BadRequest("Erro ao atualizar evento");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                 return await EventoService.DeleteEvento(id) ? Ok("Deletado") : BadRequest("Evento não deletado");
                 
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }
    }
}
