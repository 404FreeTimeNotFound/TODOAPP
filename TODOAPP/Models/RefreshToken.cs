using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace TODOAPP.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
		public string UserId { get; set; }
		public string Token { get; set; }
		public  string JwtId { get; set; }
		public bool IsRevoked { get;set; }
		public DateTime CreationDate { get; set; }
		public DateTime ExpirationDate { get; set; }

		[ForeignKey(nameof(UserId))]
		public IdentityUser User { get; set; }
    }
}