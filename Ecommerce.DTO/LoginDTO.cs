﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
  public class LoginDTO
  {
    [Required(ErrorMessage = "Ingrese Correo")]
    public string? Correo { get; set; }
    [Required(ErrorMessage = "Ingrese Contraseña")]
    public string? Clave { get; set; }
  }
}
