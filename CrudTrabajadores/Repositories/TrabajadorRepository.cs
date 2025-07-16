using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Models;

namespace TrabajadoresPrueba.Repositories
{
    public class TrabajadorRepository : ITrabajadorRepository
    {
        private readonly TrabajadoresContext _context;

        public TrabajadorRepository(TrabajadoresContext context)
        {
            _context = context;
        }

        public List<TrabajadorDTO> ListarTrabajadores()
        {
            return _context.TrabajadorDTOs
                .FromSqlRaw("EXEC sp_listar_trabajadores")
                .ToList();
        }

        public void CrearTrabajador(Trabajadore t)
        {
            _context.Trabajadores.Add(t);
            _context.SaveChanges();
        }

        public IEnumerable<Departamento> ObtenerDepartamentos()
        {
            return _context.Departamentos.ToList();
        }

        public IEnumerable<Provincium> ObtenerProvincias()
        {
            return _context.Provincia.ToList();
        }

        public IEnumerable<Distrito> ObtenerDistritos()
        {
            return _context.Distritos.ToList();
        }

        public Trabajadore ObtenerTrabajadorPorId(int id)
        {
            return _context.Trabajadores.FirstOrDefault(t => t.Id == id);
        }

        public void ActualizarTrabajador(Trabajadore t)
        {
            _context.Trabajadores.Update(t);
            _context.SaveChanges();
        }

        public void EliminarTrabajador(int id)
        {
            var trabajador = _context.Trabajadores.Find(id);
            if (trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
                _context.SaveChanges();
            }
        }

        public bool ExisteTrabajador(int id)
        {
            return _context.Trabajadores.Any(t => t.Id == id);
        }
    }
}
