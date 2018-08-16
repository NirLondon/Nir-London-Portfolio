using System.ComponentModel.DataAnnotations;

namespace Backgammon.Common
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
