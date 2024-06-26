﻿using Regularizacion.Domain.Enums;
using Regularizacion.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Regularizacion.Application.Dtos
{
    public class RegularizacionDto : IRegularizacionDomain
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Debes ingresar una vivienda")]
        public Guid IdVivienda { get; set; }
        public string NumeroExpediente { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingresa el valor de la regularización")]
        public decimal ValorRegularizacion { get; set; }
        [Required(ErrorMessage = "Ingresa el anticipo")]
        public decimal Anticipo { get; set; }
        public decimal ValorPendiente { get; set; }
        [JsonIgnore]
        public bool Pagado { get; set; } = false;
        public EstadosType Estado { get; set; } = EstadosType.PorHacer;
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaInsercion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        [Required(ErrorMessage = "Ingresa tu correo municipal")]
        public string Correo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingresa la contrasena del correo municipal")]
        public string Contrasena { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingresa el numero de la regularizacion")]
        public int numRegularizacion { get; set; }

    }
}
