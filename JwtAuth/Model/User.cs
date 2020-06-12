using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuth.Model
{
    public class User
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonProperty]
        public string Nome { get; set; }
        [JsonProperty]
        public string Cognome { get; set; }
        [JsonIgnore]
        public string Token { get; set; }

    }
}
