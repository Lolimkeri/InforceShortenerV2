namespace InforceShortener.Configuration
{
    public static class Constants
    {
        public const string ISSUER = "IssuerHere";
        public const string AUDIENCE = "AudienceHere";
        public const string KEY = "ThisIsMySuperSecretKeykey123456789!!";
        public const int LIFETIME = 60;

        public const int SHORT_URL_LENGHT = 6;
        public const int CREATE_SHORT_URL_RETRY_COUNT = 5;
    }
}
