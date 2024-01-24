
using System;
using System.ComponentModel.DataAnnotations;

namespace W8_Backend.Models.UserModels.Input
{
    public class UserUpdateRequest
    {
        public int? CompanyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string PhoneNumber { get; set; }
        public int? CountryID { get; set; }
        public int? StateProvinceID { get; set; }
        public string ZipPostCode { get; set; }
        public string City { get; set; }
        public int? RoleID { get; set; }
        public bool? NotifInv { get; set; }
        public bool? NotifShip { get; set; }
        public bool? NotifOrder { get; set; }
        public bool? NotifRMA { get; set; }
        public bool? IsCertified { get; set; }
        public bool? AdministrationRole { get; set; }
        public bool? DesignRole { get; set; }
        public bool? EstimatingRole { get; set; }
        public bool? PurchasingRole { get; set; }
        public bool? ConstructionRole { get; set; }
        public bool? InstallationRole { get; set; }
        public bool? SalesRole { get; set; }
        public bool? MarketingRole { get; set; }
        public bool? WarrantyRole { get; set; }
        [Required]
        public int ModifiedByID { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddressTwo { get; set; }
        public bool CreateNavContact { get; set; }
        public int? NavContactID { get; set; }

        public String AddressVerificationLevel { get; set; }

    }
}
