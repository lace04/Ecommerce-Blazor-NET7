using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
  public class UsuarioServicio : IUsuarioServicio
  {
    private readonly HttpClient _httpClient;

    public UsuarioServicio(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<ResponseDTO<SesionDTO>> Autorizacion(LoginDTO modelo)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync("Usuario/Autorizacion", modelo);
        var result = await response.Content.ReadFromJsonAsync<ResponseDTO<SesionDTO>>();
        return result!;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<UsuarioDTO>> Crear(UsuarioDTO modelo)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync("Usuario/Crear", modelo);
        var result = await response.Content.ReadFromJsonAsync<ResponseDTO<UsuarioDTO>>();
        return result!;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<bool>> Editar(UsuarioDTO modelo)
    {
      try
      {
        var response = await _httpClient.PutAsJsonAsync("Usuario/Editar", modelo);
        var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
        return result!;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<bool>> Eliminar(int id)
    {
      try
      {
        return await _httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"Usuario/Eliminar/{id}");
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<List<UsuarioDTO>>> Lista(string rol, string buscar)
    {
      try
      {
        return await _httpClient.GetFromJsonAsync<ResponseDTO<List<UsuarioDTO>>>($"Usuario/Lista/{rol}/{buscar}");
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<UsuarioDTO>> Obtener(int id)
    {
      try
      {
        return await _httpClient.GetFromJsonAsync<ResponseDTO<UsuarioDTO>>($"Usuario/Obtener/{id}");
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
