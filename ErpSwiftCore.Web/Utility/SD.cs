namespace ErpSwiftCore.Web.Utility
{
    public class SD
    {

        public const string Role_Admin = "admin";
        public const string Role_Company = "company";
        public const string Role_AccountingEmployee = "accounting_employee";
        public const string Role_ManagementEmployee = "management_employee";
        public const string Role_RegularEmployee = "regular_employee";
        public const string Role_WorkshopManager = "workshop_manager";
        public const string Role_SeniorManager = "senior_manager";
        public const string Role_HRManager = "hr_manager";
        public const string TokenCookie = "JWTToken";

        public static string ErpAPIBase { get; set; }

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH
        }
        public enum ContentType
        {
            Json,
            MultipartFormData
        }
    }
}