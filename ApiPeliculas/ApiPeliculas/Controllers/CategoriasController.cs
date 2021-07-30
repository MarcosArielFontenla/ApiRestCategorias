using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Models;
using ApiPeliculas.Models.DTOs;
using ApiPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/Categorias")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _ctRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();
            var listaCategoriasDTO = new List<CategoriaDTO>();

            foreach (var lista in listaCategorias)
            {
                listaCategoriasDTO.Add(_mapper.Map<CategoriaDTO>(lista));
            }

            return Ok(listaCategoriasDTO);

            // metodo que trae todas las categorias.
        }

        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        public IActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = _ctRepo.GetCategoria(categoriaId);

            if (itemCategoria == null)
                return NotFound();

            var itemCategoriaDTO = _mapper.Map<CategoriaDTO>(itemCategoria);
            return Ok(itemCategoriaDTO);

            // metodo HttpGet para devolver categoria por Id.
            // de esta manera ganamos seguridad y no exponemos el modelo Categoria, y le pasamos desde el DTO.
        }

        [HttpPost]
        public IActionResult CrearCategoria([FromBody] CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO == null)
                return BadRequest(ModelState);

            if (_ctRepo.ExisteCategoria(categoriaDTO.Nombre))
            {
                ModelState.AddModelError("", "La categoria ya existe");
                return StatusCode(404, ModelState);
            }
            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            if (!_ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal, guardando el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id}, categoria);    // con este return del metodo CreateAtRoute nos devuelve el ultimo registro creado.

            // metodo para crear categorias, con validaciones si ya existe o si arroja un error al guardar.
        }

        [HttpPatch("{categoriaId:int}", Name = "ActualizarCategoria")]
        public IActionResult ActualizarCategoria(int categoriaId, [FromBody]CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO == null || categoriaId != categoriaDTO.Id)
                return BadRequest(ModelState);

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            if (!_ctRepo.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal, actualizando el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

            // metodo para actualizar categorias, con validaciones si el dto es nulo o el id de la categoria es distinto al id del dto, y si no actualiza categoria arroja un 500.
        }

        [HttpDelete("{categoriaId:int}", Name = "BorrarCategorias")]
        public IActionResult BorrarCategorias(int categoriaId)
        {
            if (!_ctRepo.ExisteCategoria(categoriaId))
            {
                return NotFound();
            }
            var categoria = _ctRepo.GetCategoria(categoriaId);

            if (!_ctRepo.BorrarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal, borrando el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

            // metodo para eliminar categorias, con validacion si no existe el id de la categoria, devuelve notFound, sino busca la categoriaId.
        }
    }
}
