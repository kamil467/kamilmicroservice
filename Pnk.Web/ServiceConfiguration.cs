namespace Pnk.Web
{
    public static class ServiceConfiguration
    {
        public static string ProductAPIBase { get; set; }

        public enum CallType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

    }
}
