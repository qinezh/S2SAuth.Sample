namespace S2SAuth.Sample.SimpleAuthService
{
    public static class S2STokenDefaults
    {
        public readonly static string Authority = "https://sts.windows.net/common/";
        public readonly static string Audience = "https://management.azure.com";
        public readonly static string Policy = "S2STokenAuthentication";
        public readonly static string ObjectId = "oid";
    }
}
