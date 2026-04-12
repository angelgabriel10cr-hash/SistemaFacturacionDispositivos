using System.Collections.Generic;
using SVDE.Entidades.Maestros;
using SVDE.Interfaces.BLL;
using SVDE.Interfaces.DAL;
using SVDE.DAL.Repositorios;

namespace SVDE.BLL.Servicios
{
    public class ClienteBLL : IClienteBLL
    {
        private readonly IClienteDAL _clienteDAL;

        public ClienteBLL()
        {
            _clienteDAL = new ClienteDAL();
        }

        public List<Cliente> Listar()
        {
            return _clienteDAL.Listar();
        }

        private ClienteDAL clienteDAL = new ClienteDAL();

        public Cliente ObtenerPorIdentificacion(string identificacion)
        {
            return clienteDAL.ObtenerPorIdentificacion(identificacion);
        }
        public Cliente ObtenerPorId(int idCliente)
        {
            return clienteDAL.ObtenerPorId(idCliente);
        }

        public bool Guardar(Cliente entidad)
        {
            if (entidad.IdCliente == 0)
                return _clienteDAL.Insertar(entidad);
            else
                return _clienteDAL.Actualizar(entidad);
        }

        public bool Eliminar(int idCliente)
        {
            return _clienteDAL.Eliminar(idCliente);
        }

    }
}
