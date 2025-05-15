namespace Dine.Web.Utility
{
    public class AllowedExtensionsAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

    }
}
