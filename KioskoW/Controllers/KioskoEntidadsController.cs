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
    public class KioskoEntidadsController : ControllerBase
    {
        private readonly KioskoWContext _context;

        public KioskoEntidadsController(KioskoWContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KioskoEntidad>>> GetKioskoEntidad()
        {
            return await _context.Kioskos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KioskoEntidad>> GetKioskoEntidad(int id)
        {
            var kioskoEntidad = await _context.Kioskos.FindAsync(id);

            if (kioskoEntidad == null)
            {
                return NotFound();
            }

            return kioskoEntidad;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKioskoEntidad(int id, KioskoEntidad kioskoEntidad)
        {
            if (id != kioskoEntidad.Id)
            {
                return BadRequest();
            }

            _context.Entry(kioskoEntidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KioskoEntidadExists(id))
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
        public async Task<ActionResult<KioskoEntidad>> PostKioskoEntidad(KioskoEntidad kioskoEntidad)
        {
            _context.Kioskos.Add(kioskoEntidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKioskoEntidad", new { id = kioskoEntidad.Id }, kioskoEntidad);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKioskoEntidad(int id)
        {
            var kioskoEntidad = await _context.Kioskos.FindAsync(id);
            if (kioskoEntidad == null)
            {
                return NotFound();
            }

            _context.Kioskos.Remove(kioskoEntidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KioskoEntidadExists(int id)
        {
            return _context.Kioskos.Any(e => e.Id == id);
        }
    }
}
