﻿using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
  public class DashboardServicio : IDashboardServicio
  {
    private readonly HttpClient _httpClient;

    public DashboardServicio(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }
    public async Task<ResponseDTO<DashboardDTO>> Resumen()
    {
      try
      {
        var response = await _httpClient.GetFromJsonAsync<ResponseDTO<DashboardDTO>>($"Dashboard/Resumen");
        return response!;
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
