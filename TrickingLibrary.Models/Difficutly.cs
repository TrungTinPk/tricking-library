using System.Collections.Generic;

namespace TrickingLibrary.Models
{
    public class Difficutly : BaseModel<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Trick> Tricks { get; set; }
    }
}