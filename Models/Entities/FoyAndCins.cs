using System.ComponentModel.DataAnnotations.Schema;

namespace Tedarix.Models.Entities
{
    public class FoyAndCins
    {
        public int Id { get; set; }

        public int FoyId { get; set; }
        public Foy Foy { get; set; }
        public Cins Cins { get; set; }

        public int CinsId { get; set; }
        public string?RenkAdi { get; set; }
        public bool State { get; set; }
        public DateTime RegisterDate { get; set; }
    

    }   
    public class FoyAndCinsDto
    {

        public string CinsAdi{ get; set; }
        public DateTime RegisterDate { get; set; }
        public TimeSpan GecenGun { get; set; }


    }
}
