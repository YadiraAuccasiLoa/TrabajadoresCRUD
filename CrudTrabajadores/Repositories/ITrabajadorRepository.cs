using TrabajadoresPrueba.Models;

namespace TrabajadoresPrueba.Repositories
{
    public interface ITrabajadorRepository
    {
        List<TrabajadorDTO> ListarTrabajadores();

        void CrearTrabajador(Trabajadore t);
        Trabajadore ObtenerTrabajadorPorId(int id);
        void ActualizarTrabajador(Trabajadore t);
        void EliminarTrabajador(int id);

        IEnumerable<Departamento> ObtenerDepartamentos();
        IEnumerable<Provincium> ObtenerProvincias();
        IEnumerable<Distrito> ObtenerDistritos();
        
        bool ExisteTrabajador(int id);
    }
}
