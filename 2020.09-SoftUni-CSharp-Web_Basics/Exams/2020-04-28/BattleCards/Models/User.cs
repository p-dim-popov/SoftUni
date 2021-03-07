using System;
using System.Collections.Generic;
using SUS.MvcFramework;

namespace BattleCards.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            Role = IdentityRole.User;
        }
        public virtual ICollection<UserCard> UserCard { get; set; }
            = new HashSet<UserCard>();
    }
}
