namespace Stratum.Feature.Forms
{
    public class Constants
    {
        public static string ViewsFolderPath = "~/Views/Stratum/Forms/";

        public struct Forms
        {
            public struct ContactUs
            {
                public static readonly string ID = "{287DE272-D2DA-4986-AE70-ECFF1E0AC5D6}";

                public struct Fields
                {
                    public static readonly string Name = "{CF50644C-B069-495A-A771-0040B55D9385}";
                    public static readonly string Email = "{F76C4115-23B7-427C-827C-7451CFB70453}";
                    public static readonly string Subject = "{CF0CE2C5-FC12-4ABD-8C6D-638EEC767826}";
                    public static readonly string Message = "{3BE92D6E-5ADF-4A4B-BFAD-78C7721F9B07}";
                }
            }
        }
    }
}