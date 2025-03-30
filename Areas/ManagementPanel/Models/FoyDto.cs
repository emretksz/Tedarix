using Tedarix.Models.Entities;

namespace Tedarix.Areas.ManagementPanel.Models
{
    public class FoyDto
    {
        public Foy Foy { get; set; }
        public List<FoyAndCins> FoyAndCins { get; set; }
        public List<FoyAndKesim> FoyAndKesim { get; set; }
        public List<FoyAndColor> FoyAndColor { get; set; }

    }
    public class FoyDtoIndex
    {
        public Foy Foy { get; set; }
        public List<FoyAndCinsDto> FoyAndCins { get; set; }
        public List<FoyAndKesimDto> FoyAndKesim { get; set; }

    }

}
