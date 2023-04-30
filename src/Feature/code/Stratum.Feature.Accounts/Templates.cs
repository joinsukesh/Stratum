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
            public static readonly ID ID = new ID("{3E05CFFB-E865-4F62-B4F8-8C54C90016AF}");

            public struct Fields
            {
                public static readonly ID ResetPasswordPage = new ID("{76575F7C-014D-480B-B9B4-61679C940441}");
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