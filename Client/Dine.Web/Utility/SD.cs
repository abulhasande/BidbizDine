namespace Dine.Web.Utility
{
    public class SD
    {
        public static string AuthAPIBase {  get; set; }
        public static string CooponAPIBase {  get; set; }

        public static string  ProductAPIBase { get; set; }

        public static string ShoppingCartAPIBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "Customer";

        public const string TokenCookies = "JWTToken";
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
