using System.Collections.Generic;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Models
{
    public class Difficulty : BaseModel<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Trick> Tricks { get; set; }
    }
}
