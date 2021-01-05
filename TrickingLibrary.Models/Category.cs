using System.Collections.Generic;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Models
{
    public class Category : BaseModel<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<TrickCategory> Tricks { get; set; }
    }
}
