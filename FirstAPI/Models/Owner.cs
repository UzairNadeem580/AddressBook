using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string HouseNo { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public List<BuildingDetails> BuildingDetails { get; set; }
    }

    [Keyless]
    public class BuildingDetails
    {
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneRenter { get; set; }
        public string EmailRenter { get; set; }
        public string PhonePropertyManagement { get; set; }
        public string EmailPropertyManagement { get; set; }
    }

}
