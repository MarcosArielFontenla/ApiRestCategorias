using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Models;

namespace ApiPeliculas.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        // interface que trabaja con los metodos para la entidad categoria

        ICollection<Categoria> GetCategorias();     // metodo que trae todas las categorias.
        Categoria GetCategoria(int CategoriaId);    // le paso por parametro el Id de la Categoria.
        bool ExisteCategoria(string nombre);
        bool ExisteCategoria(int id);
        bool CrearCategoria(Categoria categoria);
        bool ActualizarCategoria(Categoria categoria);
        bool BorrarCategoria(Categoria categoria);
        bool Guardar();
    }
}
