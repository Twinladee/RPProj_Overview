using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.DAL.Models
{
    public class AccountModel
    {
        [Key]
        public int AccountId { get; set; }
        public ulong? SocialClubId { get; set; }

        public AccountModel()
        {
            
        }
    }
}
