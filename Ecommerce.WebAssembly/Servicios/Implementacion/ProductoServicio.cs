using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
  public class ProductoServicio : IProductoServicio
  {
    private readonly HttpClient _httpClient;
    public ProductoServicio(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<ResponseDTO<List<ProductoDTO>>> Catalogo(string categoria, string buscar)
    {
      try
      {
        return await _httpClient.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>($"Producto/Catalogo/{categoria}/{buscar}");
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<ProductoDTO>> Crear(ProductoDTO modelo)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync("Producto/Crear", modelo);
        var result = await response.Content.ReadFromJsonAsync<ResponseDTO<ProductoDTO>>();
        return result!;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<bool>> Editar(ProductoDTO modelo)
    {
      try
      {
        var response = await _httpClient.PutAsJsonAsync("Producto/Editar", modelo);
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
        return await _httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"Producto/Eliminar/{id}");
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<List<ProductoDTO>>> Lista(string buscar)
    {
      try
      {
        return await _httpClient.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>($"Producto/Lista/{buscar}");
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ResponseDTO<ProductoDTO>> Obtener(int id)
    {
      try
      {
        return await _httpClient.GetFromJsonAsync<ResponseDTO<ProductoDTO>>($"Producto/Obtener/{id}");
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
