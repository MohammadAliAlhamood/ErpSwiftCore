namespace ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels.ValueObjectModels
{
    public class ContactInfoDto
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Website { get; set; }
        public string? Other { get; set; }
        public override string ToString()
        {
            return $"Email: {Email}, Phone: {Phone}, Mobile: {Mobile}, Fax: {Fax}, Website: {Website}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is not ContactInfoDto other) return false;

            return Email == other.Email &&
                   Phone == other.Phone &&
                   Mobile == other.Mobile &&
                   Fax == other.Fax &&
                   Website == other.Website &&
                   Other == other.Other;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Email, Phone, Mobile, Fax, Website, Other);
        }
    }
}
