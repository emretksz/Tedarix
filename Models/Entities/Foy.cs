using System.ComponentModel.DataAnnotations.Schema;

namespace Tedarix.Models.Entities
{
    public class Foy
    {

        public int Id { get; set; }
        public string ModelCode { get; set; }
        public string Age { get; set; }
        public Tenant Tenant { get; set; }
        public int TenantId { get; set; }

        public Atolye? Atolye { get; set; }
        public int? AtolyeId { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastModifierTime { get; set; }
        public bool ArsivlendiMi { get; set; }
        public bool? SevkeHazir { get; set; }
        public bool? SevkEdildi { get; set; }
        public bool? Tamamlandi { get; set; }
        public DateTime SevkTarihi { get; set; }
        ICollection<FoyAndCins> FoyAndCins { get; set; }
        ICollection<FoyAndKesim> FoyAndKesim { get; set; }

    }
}
