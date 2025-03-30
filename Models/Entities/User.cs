using System.ComponentModel.DataAnnotations.Schema;

namespace Tedarix.Models.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }

        public Role? Role { get; set; }
        public int? RoleId { get; set; }
    }
}
