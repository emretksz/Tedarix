using System.ComponentModel.DataAnnotations.Schema;

namespace Tedarix.Models.Entities
{
    public class FoyAndKesim
    {
        public int Id { get; set; }

        public int FoyId { get; set; }
        public Foy Foy { get; set; }
        public Kesim Kesim { get; set; }
        public int KesimId { get; set; }
        public bool State { get; set; }
        public DateTime RegisterDate { get; set; }

    }  
    public class FoyAndKesimDto
    {
        public string KesimAdi { get; set; }
        public DateTime RegisterDate { get; set; }
        public TimeSpan GecenGun { get; set; }

    }
}
