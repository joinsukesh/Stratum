namespace Stratum.Feature.Accounts
{
    using Sitecore.Data;

    public class Templates
    {
        public struct SignUpRenderingParameters
        {
            public static readonly ID ID = new ID("{95D30B35-6EF0-4D54-BDE3-65BCA2B54B60}");

            public struct Fields
            {
                public static readonly ID ConfirmSignUpPage = new ID("{97135390-CE19-4CDF-B49B-E9774D9A9EAE}");
            }
        }

        public struct SignInRenderingParameters
        {
            public static readonly ID ID = new ID("{13A6DD2C-188B-4A5C-A047-22DAE21A3E93}");

            public struct Fields
            {
                public static readonly ID ForgotPasswordPage = new ID("{CB48976A-1BB8-4103-85E1-C97DDB34125B}");
                public static readonly ID ForgotPasswordLabel = new ID("{2C15D829-598D-4499-AE2A-442DAC921849}");
            }
        }

        public struct ConfirmSignUpRenderingParameters
        {
            public static readonly ID ID = new ID("{009254DC-F2EA-4C58-BFF6-91CD4999AFF2}");

            public struct Fields
            {
                public static readonly ID Content = new ID("{B8B5A240-A0D0-4DF6-B3D9-FEC909613761}");
            }
        }

        public struct ForgotPasswordRenderingParameters
        {
            public static readonly ID ID = new ID("{66D4A0F6-F989-44A1-89C7-879027DB3381}");

            public struct Fields
            {
                public static readonly ID ResetPasswordPage = new ID("{146E5EBB-33E8-4864-BE27-C1B2119834BD}");
            }
        }

        public struct ResetPasswordRenderingParameters
        {
            public static readonly ID ID = new ID("{6BE97643-00F5-4C92-9811-89A67EECF73E}");

            public struct Fields
            {
                public static readonly ID SignInPage = new ID("{7A5EF0AA-A610-46F0-BC63-95CE0BDA3D76}");
            }
        }
    }
}