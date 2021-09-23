using Blog.Services.Data;
using Blog.Services.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PublicacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Publicaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publicacion>>> GetPublicaciones()
        {
            return await _context.Publicaciones.ToListAsync();
        }

        // GET: api/Publicaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publicacion>> GetArticulo(int id)
        {
            var articulo = await _context.Publicaciones.FindAsync(id);

            if (articulo == null)
            {
                return NotFound();
            }

            return articulo;
        }

        // PUT: api/Publicaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulo(int id, Publicacion articulo)
        {
            if (id != articulo.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(articulo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloExists(id))
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

        // POST: api/Publicaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publicacion>> PostArticulo(Publicacion articulo)
        {
            _context.Publicaciones.Add(articulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticulo", new { id = articulo.Codigo }, articulo);
        }

        // DELETE: api/Publicaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulo(int id)
        {
            var articulo = await _context.Publicaciones.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }

            _context.Publicaciones.Remove(articulo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticuloExists(int id)
        {
            return _context.Publicaciones.Any(e => e.Codigo == id);
        }
    }
}

