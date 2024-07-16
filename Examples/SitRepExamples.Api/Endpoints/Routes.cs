// ReSharper disable MemberHidesStaticFromOuterClass

namespace SitRepExamples.Api.Endpoints;

public static class Routes
{
    public const string Base = "";

    public static class SanityChecks
    {
        public const string Base = $"{Routes.Base}/sanity-checks";

        public const string Ok = $"{Base}/ok";

        public const string NotFound = $"{Base}/not-found";
    }
}