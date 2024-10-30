using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuoqaIdentidades
{
    public class Account
    {
        [Key]
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        
        [NotMapped]
        public string UserPasswordR { get; set; }
        public string ActivatedUser { get; set; }

    }
    public class AccountLogin
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
