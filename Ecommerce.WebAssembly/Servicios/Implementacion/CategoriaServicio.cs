using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
  public class CategoriaServicio : ICategoriaServicio
  {
    private readonly HttpClient _httpClient;
    public CategoriaServicio(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<ResponseDTO<CategoriaDTO>> Crear(CategoriaDTO modelo)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync("Categoria/Crear", modelo);
        var result = await response.Content.ReadFromJsonAsync<ResponseDTO<CategoriaDTO>>();
        return result!;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<bool>> Editar(CategoriaDTO modelo)
    {
      try
      {
        var response = await _httpClient.PutAsJsonAsync("Categoria/Editar", modelo);
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
        return await _httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"Categoria/Eliminar/{id}");

      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<List<CategoriaDTO>>> Lista(string buscar)
    {
      try
      {
        return await _httpClient.GetFromJsonAsync<ResponseDTO<List<CategoriaDTO>>>($"Categoria/Lista/{buscar}");
      }
      catch (Exception)
      {
        throw;
      }
    }

    public Task<ResponseDTO<CategoriaDTO>> Obtener(int id)
    {
      try
      {
        return _httpClient.GetFromJsonAsync<ResponseDTO<CategoriaDTO>>($"Categoria/Obtener/{id}");
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
