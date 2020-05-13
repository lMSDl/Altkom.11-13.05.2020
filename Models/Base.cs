using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Base
    {
        [JsonIgnore]
        public abstract int Id { get; set; }
    }
}
