using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Data;
using ApiPeliculas.Models;
using ApiPeliculas.Repository.IRepository;

namespace ApiPeliculas.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarCategoria(Categoria categoria)
        {
            _db.Categoria.Update(categoria);
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoria)
        {
            _db.Categoria.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            _db.Categoria.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _db.Categoria.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());  // validacion si existe un nombre igual al que recibo por parametro, lo convierto en minuscula sacando espacios.
            return valor;
        }

        public bool ExisteCategoria(int id)
        {
            return _db.Categoria.Any(c => c.Id == id);  // validacion con el metodo Any() para que busque si hay el mismo id que me llega por parametro
        }

        public Categoria GetCategoria(int CategoriaId)
        {
            return _db.Categoria.FirstOrDefault(c => c.Id == CategoriaId);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _db.Categoria.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
