using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Domain.Entities
{
    public class NumRegDomain
    {
        [JsonProperty("NUM_REGULARIZACION")]
        public int NUM_REGULARIZACION { get; set; }
    }
}
