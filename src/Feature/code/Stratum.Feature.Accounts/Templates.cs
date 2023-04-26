namespace Stratum.Feature.Accounts
{
    using Sitecore.Data;

    public class Templates
    {
        public struct SignUpRenderingParameters
        {
            public static readonly ID ID = new ID("{B03AA25C-8B03-4BD7-AD74-A24242601830}");

            public struct Fields
            {
                public static readonly ID ConfirmSignUpPage = new ID("{EF8EE554-04CB-45CC-932B-E7B1212BF0F0}");
            }
        }

        public struct SignInRenderingParameters
        {
            public static readonly ID ID = new ID("{380E1A0D-3952-4631-8D9E-859AFD375F38}");

            public struct Fields
            {
                public static readonly ID ForgotPasswordPage = new ID("{1C7C9B1D-5811-4AD0-866F-3373A0649B7E}");
                public static readonly ID ForgotPasswordLabel = new ID("{6D57AB79-E307-42B1-B668-224B1622EF3B}");
            }
        }

        public struct ConfirmSignUpRenderingParameters
        {
            public static readonly ID ID = new ID("{2FFAF179-9CBE-4A5F-B2BC-050ECFA96464}");

            public struct Fields
            {
                public static readonly ID Content = new ID("{3E46E8CF-EF80-42AB-8777-45F2D8850010}");
            }
        }

        public struct ForgotPasswordRenderingParameters
        {
            public static readonly ID ID = new ID("{7EBFED6E-8263-4C7A-84B2-7C0B9EF9B741}");

            public struct Fields
            {
                public static readonly ID ResetPasswordPage = new ID("{B4E04CAF-ACAC-4BD9-A916-F69049E4E10E}");
            }
        }

        public struct ResetPasswordRenderingParameters
        {
            public static readonly ID ID = new ID("{FC5D96EF-2F44-4B91-AF9C-DDEEA17DC0D5}");

            public struct Fields
            {
                public static readonly ID SignInPage = new ID("{C4196CD7-6926-46B0-90EA-EADB9199C6DB}");
            }
        }
    }
}