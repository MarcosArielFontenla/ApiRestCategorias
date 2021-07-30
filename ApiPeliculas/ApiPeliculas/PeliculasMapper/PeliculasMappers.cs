using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Models;
using ApiPeliculas.Models.DTOs;
using AutoMapper;

namespace ApiPeliculas.PeliculasMapper
{
    public class PeliculasMappers : Profile
    {
        public PeliculasMappers()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
    }
}
