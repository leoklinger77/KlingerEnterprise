using System;
using System.Collections.Generic;

namespace KlingerSystem.Authentication.Api.Models
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}
