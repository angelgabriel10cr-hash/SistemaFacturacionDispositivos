using SVDE.DAL1.Repositorios;
using SVDE.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.BLL.Servicios
{
    public class UsuarioBLL
    {
        private readonly UsuarioDAL _usuarioDAL;

        public UsuarioBLL()
        {
            _usuarioDAL = new UsuarioDAL();
        }

        public Usuario Autenticar(string nombreUsuario, string clave)
        {
            Usuario usuario = _usuarioDAL.ObtenerPorNombreUsuario(nombreUsuario);

            if (usuario == null)
                return null;

            if (!usuario.Estado)
                return null;

            bool claveCorrecta = SeguridadHelper.VerificarHash(clave, usuario.ClaveHash);

            if (!claveCorrecta)
                return null;

            return usuario;
        }

        public List<Usuario> Listar()
        {
            return _usuarioDAL.Listar();
        }

        public bool Insertar(Usuario usuario)
        {
            usuario.ClaveHash = SeguridadHelper.GenerarHashSHA256(usuario.ClavePlano);
            return _usuarioDAL.Insertar(usuario);
        }

        public bool Actualizar(Usuario usuario)
        {
            if (!string.IsNullOrWhiteSpace(usuario.ClavePlano))
                usuario.ClaveHash = SeguridadHelper.GenerarHashSHA256(usuario.ClavePlano);

            return _usuarioDAL.Actualizar(usuario);
        }

        public bool CambiarEstado(int idUsuario, bool estado)
        {
            return _usuarioDAL.CambiarEstado(idUsuario, estado);
        }
        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            return _usuarioDAL.ExisteNombreUsuario(nombreUsuario);
        }
    }
}
