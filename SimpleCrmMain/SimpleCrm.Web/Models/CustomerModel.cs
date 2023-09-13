using System;
namespace SimpleCrm.Web.Models
{
	public class CustomerModel
	{
		public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string FavoriteSeason { get; set; }

        public string FavoriteFlower { get; set; }
    }
}

