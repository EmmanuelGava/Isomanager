using Isomanager.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Http;

[RoutePrefix("api/Usuarios")]
public class UsuariosController : ApiController
{
    private MyDbContext db = new MyDbContext();

    // GET: api/Usuarios
    [HttpGet]
    [Route("")] // Ruta para obtener todos los usuarios
    public async Task<IHttpActionResult> GetUsuarios()
    {
        var usuarios = await db.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    // GET: api/Usuarios/desempenos
    [HttpGet]
    [Route("desempenos")] // Ruta para obtener promedios de desempeño
    public async Task<IHttpActionResult> GetDesempenos()
    {
        var desempenos = await db.Desempenos
            .GroupBy(d => d.Mes) // Agrupar por mes
            .Select(g => new
            {
                Mes = g.Key,
                Promedio = g.Average(d => d.Promedio) // Calcular el promedio por mes
            })
            .ToListAsync();

        return Ok(desempenos);
    }

    // GET: api/Usuarios/{usuarioId}/formacion
    [HttpGet]
    [Route("{usuarioId:int}/formacion")] // Ruta para obtener formación de un usuario
    public async Task<IHttpActionResult> GetFormacion(int usuarioId)
    {
        var formaciones = await db.Formaciones
            .Where(f => f.UsuarioId == usuarioId)
            .ToListAsync();

        if (formaciones == null || !formaciones.Any())
        {
            return NotFound(); // Si no hay formaciones, devolver 404
        }

        return Ok(formaciones);
    }

    // PUT: api/Usuarios/5
    [HttpPut]
    [Route("{id:int}")] // Ruta para actualizar un usuario
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutUsuarios(int id, Usuarios usuarios)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != usuarios.UsuarioId)
        {
            return BadRequest();
        }

        db.Entry(usuarios).State = EntityState.Modified;

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuariosExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return StatusCode(HttpStatusCode.NoContent);
    }

    // POST: api/Usuarios
    [HttpPost]
    [Route("")] // Ruta para crear un nuevo usuario
    [ResponseType(typeof(Usuarios))]
    public async Task<IHttpActionResult> PostUsuarios(Usuarios usuarios)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        db.Usuarios.Add(usuarios);
        await db.SaveChangesAsync();

        // Cambia esto para usar Created en lugar de CreatedAtRoute
        return Created("api/Usuarios", usuarios);
    }

    // DELETE: api/Usuarios/5
    [HttpDelete]
    [Route("{id:int}")] // Ruta para eliminar un usuario
    [ResponseType(typeof(Usuarios))]
    public async Task<IHttpActionResult> DeleteUsuarios(int id)
    {
        Usuarios usuarios = await db.Usuarios.FindAsync(id);
        if (usuarios == null)
        {
            return NotFound();
        }

        db.Usuarios.Remove(usuarios);
        await db.SaveChangesAsync();

        return Ok(usuarios);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }

    private bool UsuariosExists(int id)
    {
        return db.Usuarios.Count(e => e.UsuarioId == id) > 0;
    }

    // POST: api/Usuarios/desempenos
    [HttpPost]
    [Route("desempenos")] // Ruta para agregar promedios de desempeño
    public async Task<IHttpActionResult> PostDesempenos(List<Desempeno> desempenos)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Verificar que todos los usuarios existen
        var usuarioIds = desempenos.Select(d => d.UsuarioId).Distinct().ToList();
        var usuarios = await db.Usuarios.Where(u => usuarioIds.Contains(u.UsuarioId)).ToListAsync();

        if (usuarios.Count != usuarioIds.Count)
        {
            return NotFound(); // Al menos un usuario no fue encontrado
        }

        // Agregar todos los promedios a la base de datos
        db.Desempenos.AddRange(desempenos);
        await db.SaveChangesAsync();

        // Cambia esto para usar Created en lugar de CreatedAtRoute
        return Created("api/Usuarios/desempenos", desempenos);
    }
}