using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KioskoApiEntidad;
using KioskoW.Data;

namespace KioskoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioEntidadsController : ControllerBase
    {
        private readonly KioskoWContext _context;

        public UsuarioEntidadsController(KioskoWContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioEntidad>>> GetUsuarioEntidad()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioEntidad>> GetUsuarioEntidad(int id)
        {
            var usuarioEntidad = await _context.Usuarios.FindAsync(id);

            if (usuarioEntidad == null)
            {
                return NotFound();
            }

            return usuarioEntidad;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioEntidad(int id, UsuarioEntidad usuarioEntidad)
        {
            if (id != usuarioEntidad.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuarioEntidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioEntidadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioEntidad>> PostUsuarioEntidad(UsuarioEntidad usuarioEntidad)
        {
            _context.Usuarios.Add(usuarioEntidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioEntidad", new { id = usuarioEntidad.IdUsuario }, usuarioEntidad);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioEntidad(int id)
        {
            var usuarioEntidad = await _context.Usuarios.FindAsync(id);
            if (usuarioEntidad == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarioEntidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioEntidadExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
