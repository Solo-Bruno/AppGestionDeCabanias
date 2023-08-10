using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Librería.LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;

namespace Libreria.LogicaAccesoDatos.EF
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private LibreriaContext _db;

        public RepositorioUsuario(LibreriaContext DB)
        {
            _db = DB; 
        }
        public void Add(Usuario tipo)
        {
            if (tipo == null)
            {
                throw new ArgumentNullException("El usuario es nulo");
            }
            try
            {
                tipo.Validar();
                _db.Usuarios.Add(tipo);
                _db.SaveChanges();
            }
            catch (Exception e )
            {

                throw e;
            }
        }

        public IEnumerable<Usuario> FindAll()
        {
           return _db.Usuarios.ToList();
        }

        public Usuario FindById(int id)
        {
            try
            {
                Usuario aux = _db.Usuarios.Find(id);
                if (aux != null)
                {
                    return aux;
                }
                else
                {
                    throw new InvalidOperationException("No se encuentra ese Usuario");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Usuario FindByName(string email, string pass)
        {


            Usuario aux = _db.Usuarios
                           .FirstOrDefault(us => us.Email == email && us.Pass == pass);

            if (aux != null)
            {
                return aux;
            }
            else
            {
                throw new ArgumentException("Usuario incorrecto");
            }


        }

        public void Remove(Usuario tipo)
        {
            try
            {
                _db.Usuarios.Remove(tipo);
                _db.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void RemoveById(int id)
        {
            try
            {
                var aux = FindById(id);
                _db.Usuarios.Remove(aux);
                _db.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Update(Usuario tipo)
        {
            var aux = FindById(tipo.Id);
            if (aux != null)
            {
                aux.Email = tipo.Email;
                aux.Pass = tipo.Pass;
                _db.Entry(aux).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se Puede actualizar ese Usuario");
            }
        }
    }
}
