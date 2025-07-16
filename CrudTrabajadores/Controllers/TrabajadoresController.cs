using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajadoresPrueba.Models;
using TrabajadoresPrueba.Repositories;

namespace TrabajadoresPrueba.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly ITrabajadorRepository _repo;
        private readonly TrabajadoresContext _context;

        // Inyectamos la interfaz, no el DbContext directamente
        public TrabajadoresController(ITrabajadorRepository repo, TrabajadoresContext context)
        {
            _repo = repo;
            _context = context;
        }

        // GET: Trabajadores
        public IActionResult Index(string sexo)
        {
            // Obtener todos los trabajadores
            var trabajadores = _repo.ListarTrabajadores();

            // Filtrar por sexo si se recibe
            if (!string.IsNullOrEmpty(sexo))
            {
                trabajadores = trabajadores.Where(t => t.Sexo == sexo).ToList();
            }

            // Enviar filtro a la vista para mantenerlo seleccionado
            ViewBag.SexoSeleccionado = sexo;

            return View(trabajadores);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_repo.ObtenerDepartamentos(), "Id", "NombreDepartamento");
            ViewData["IdProvincia"] = new SelectList(_repo.ObtenerProvincias(), "Id", "NombreProvincia");
            ViewData["IdDistrito"] = new SelectList(_repo.ObtenerDistritos(), "Id", "NombreDistrito");

            return View();
        }

        // POST: Trabajadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (ModelState.IsValid)
            {
                _repo.CrearTrabajador(trabajadore); 
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdDepartamento"] = new SelectList(_repo.ObtenerDepartamentos(), "Id", "NombreDepartamento", trabajadore.IdDepartamento);
            ViewData["IdProvincia"] = new SelectList(_repo.ObtenerProvincias(), "Id", "NombreProvincia", trabajadore.IdProvincia);
            ViewData["IdDistrito"] = new SelectList(_repo.ObtenerDistritos(), "Id", "NombreDistrito", trabajadore.IdDistrito);

            return View(trabajadore);
        }

        // GET: Trabajadores/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var trabajadore = _repo.ObtenerTrabajadorPorId(id.Value);
            if (trabajadore == null)
                return NotFound();

            ViewData["IdDepartamento"] = new SelectList(_repo.ObtenerDepartamentos(), "Id", "NombreDepartamento", trabajadore.IdDepartamento);
            ViewData["IdProvincia"] = new SelectList(_repo.ObtenerProvincias(), "Id", "NombreProvincia", trabajadore.IdProvincia);
            ViewData["IdDistrito"] = new SelectList(_repo.ObtenerDistritos(), "Id", "NombreDistrito", trabajadore.IdDistrito);

            return View(trabajadore);
        }

        // POST: Trabajadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (id != trabajadore.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.ActualizarTrabajador(trabajadore);
                }
                catch (Exception)
                {
                    if (!_repo.ExisteTrabajador(trabajadore.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdDepartamento"] = new SelectList(_repo.ObtenerDepartamentos(), "Id", "NombreDepartamento", trabajadore.IdDepartamento);
            ViewData["IdProvincia"] = new SelectList(_repo.ObtenerProvincias(), "Id", "NombreProvincia", trabajadore.IdProvincia);
            ViewData["IdDistrito"] = new SelectList(_repo.ObtenerDistritos(), "Id", "NombreDistrito", trabajadore.IdDistrito);

            return View(trabajadore);
        }

        // GET: Trabajadores/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var trabajadore = _repo.ObtenerTrabajadorPorId(id.Value);
            if (trabajadore == null)
                return NotFound();

            return View(trabajadore);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_repo.ExisteTrabajador(id))
                return NotFound();
            
            _repo.EliminarTrabajador(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
