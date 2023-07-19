using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Ecommerce.Modelo;
using Ecommerce.DTO;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Servicio.Contrato;
using AutoMapper;

namespace Ecommerce.Servicio.Implementacion
{
  public class UsuarioServicio : IUsuarioServicio
  {
    private readonly IGenericoRepositorio<Usuario> _modeloRepositorio;
    private readonly IMapper _mapper;

    public UsuarioServicio(IGenericoRepositorio<Usuario> modeloRepositorio, IMapper mapper)
    {
      _modeloRepositorio = modeloRepositorio;
      _mapper = mapper;
    }

    public async Task<SesionDTO> Autorizacion(LoginDTO modelo)
    {
      try
      {
        var consulta = _modeloRepositorio.Consultar(p => p.Correo == modelo.Correo && p.Clave == modelo.Clave);

        var fromDbModelo = await consulta.FirstOrDefaultAsync();

        if (fromDbModelo != null)
          return _mapper.Map<SesionDTO>(fromDbModelo);
        else
          throw new TaskCanceledException("Usuario o clave incorrecta");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
    {
      try
      {
        var dbModelo = _mapper.Map<Usuario>(modelo);
        var rspModelo = await _modeloRepositorio.Crear(dbModelo);

        if (rspModelo.IdUsuario != 0)
          return _mapper.Map<UsuarioDTO>(dbModelo);
        else
          throw new TaskCanceledException("Usuario no creado");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<bool> Editar(UsuarioDTO modelo)
    {
      try
      {
        var consulta = _modeloRepositorio.Consultar(p => p.IdUsuario == modelo.IdUsuario);
        var fromDbModelo = await consulta.FirstOrDefaultAsync();

        if (fromDbModelo != null)
        {
          fromDbModelo.NombreCompleto = modelo.NombreCompleto;
          fromDbModelo.Correo = modelo.Correo;
          fromDbModelo.Clave = modelo.Clave;
          fromDbModelo.Rol = modelo.Rol;
          var respuesta = await _modeloRepositorio.Editar(fromDbModelo);

          if (!respuesta)
            throw new TaskCanceledException("Usuario no editado");
          else
            return respuesta;
        }
        else
          throw new TaskCanceledException("Usuario no encontrado");

      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<bool> Eliminar(int id)
    {
      try
      {
        var consulta = _modeloRepositorio.Consultar(p => p.IdUsuario == id);
        var fromDbModelo = await consulta.FirstOrDefaultAsync();

        if (fromDbModelo != null)
        {
          var respuesta = await _modeloRepositorio.Eliminar(fromDbModelo);
          if (!respuesta)
            throw new TaskCanceledException("Usuario no eliminado");
          else
            return respuesta;
        }
        else
          throw new TaskCanceledException("Usuario no encontrado");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<List<UsuarioDTO>> Lista(string rol, string buscar)
    {
      try
      {
        var consulta = _modeloRepositorio.Consultar(p => p.Rol == rol &&
        string.Concat(p.NombreCompleto.ToLower(), p.Correo.ToLower()).Contains(buscar.ToLower()));

        List<UsuarioDTO> lista = _mapper.Map<List<UsuarioDTO>>(await consulta.ToListAsync());
        return lista;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<UsuarioDTO> Obtener(int id)
    {
      try
      {
        var consulta = _modeloRepositorio.Consultar(p => p.IdUsuario == id);
        var fromDbModelo = await consulta.FirstOrDefaultAsync();

        if (fromDbModelo != null)
          return _mapper.Map<UsuarioDTO>(fromDbModelo);
        else
          throw new TaskCanceledException("Usuario no encontrado");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
