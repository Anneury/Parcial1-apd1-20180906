using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parcial1_apd1_20180906.Entidades;
using Parcial1_apd1_20180906.Dal;

namespace Parcial1_apd1_20180906.BLL
{
    public class CiudadBLL
    {
        public static bool Guardar(Ciudad ciudades)
        {
            if (!Existe(ciudades.CiudadId))
                return Insertar(ciudades);
            else
                return Modificar(ciudades);
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool Encontrado = false;

            try
            {
                Encontrado = contexto.Ciudad.Any(e => e.CiudadId == id);
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Encontrado;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {
                var ciudad = contexto.Ciudad.Find(id);

                if(ciudad != null)
                {
                    contexto.Ciudad.Remove(ciudad);
                    paso = contexto.SaveChanges() > 0;

                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool ExisteCiudad(string NombreCiudad)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                paso = contexto.Ciudad.Any(e => e.Nombre == NombreCiudad);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        public static Ciudad Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Ciudad ciudad = new Ciudad();

            try
            {
                ciudad = contexto.Ciudad.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ciudad;
        }
        public static bool Insertar(Ciudad ciudades)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Ciudad.Add(ciudades);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Ciudad ciudades)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Entry(ciudades).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
    }
}
