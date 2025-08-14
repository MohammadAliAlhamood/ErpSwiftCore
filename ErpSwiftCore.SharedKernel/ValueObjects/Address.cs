using System.ComponentModel.DataAnnotations;

namespace ErpSwiftCore.SharedKernel.ValueObjects
{
    public class Address
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public Address(string street, string city, string state, string postalCode, string country)
        {
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }

        public Address()
        { }
        public override bool Equals(object? obj)
        {
            return obj is Address address &&
                   Street == address.Street &&
                   City == address.City &&
                   State == address.State &&
                   PostalCode == address.PostalCode &&
                   Country == address.Country;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, State, PostalCode, Country);
        }
        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {PostalCode}, {Country}";
        }
    }
}