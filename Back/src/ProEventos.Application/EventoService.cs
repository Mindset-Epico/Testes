using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        public IGeralPersist GeralPersist { get; }
        public IEventoPersist EventoPersist { get; }
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            this.EventoPersist = eventoPersist;
            this.GeralPersist = geralPersist;
            
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                 GeralPersist.Add<Evento>(model);
                 if(await GeralPersist.SaveChangesAsync())
                 {
                    return await EventoPersist.GetEventoByIdAsync(model.Id, false);
                 }
                 return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                 var evento = await EventoPersist.GetEventoByIdAsync(eventoId, false);
                 if(evento == null) return null;

                 model.Id = evento.Id;

                 GeralPersist.Update(model);
                 if(await GeralPersist.SaveChangesAsync())
                 {
                    return await EventoPersist.GetEventoByIdAsync(model.Id, false);
                 }
                 return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                 var evento = await EventoPersist.GetEventoByIdAsync(eventoId, false);
                 if(evento == null) throw new Exception("Evento para delete não encontrado.");

                 

                 GeralPersist.Delete<Evento>(evento);
                 return await GeralPersist.SaveChangesAsync();
                 
                
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                 var eventos = await EventoPersist.GetAllEventosAsync(includePalestrantes);
                 if (eventos == null) return null;

                 return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                 var eventos = await EventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                 if (eventos == null) return null;

                 return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                 var eventos = await EventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                 if (eventos == null) return null;

                 return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

    }
}