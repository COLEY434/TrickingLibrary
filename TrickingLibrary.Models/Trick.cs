using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Models
{
    public class Trick : BaseModel<string>
    {
        public string Name { get; set; }

    }
}
