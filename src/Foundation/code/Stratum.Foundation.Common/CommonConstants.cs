
namespace Stratum.Foundation.Common
{
    using System.Configuration;

    public class CommonConstants
    {
        public static string GenericError = "A problem occurred while processing your request. Please try again later.";
        public static string One = "1";
        public static string Yes = "yes";
        public static string ParametersTemplate = "Parameters Template";        
        public static string TagsFolderItemId = "{7CBD7263-B211-42D2-9FC7-63B47D98FDAD}";

        public struct Databases
        {
            public const string Core = "core";
            public const string Master = "master";
            public const string Web = "web";
            public const string ExperienceForms = "experienceforms";
            public const string App_Logs = "app_logs";
        }

        public struct ColumnNames
        {
            public const string RowNum = "RowNum";
            public const string TotalRows = "TotalRows";
        }

        public struct ConnectionStrings
        {
            public static string Core = ConfigurationManager.ConnectionStrings[Databases.Core].ConnectionString;
            public static string ExperienceForms = ConfigurationManager.ConnectionStrings[Databases.ExperienceForms].ConnectionString;
        }

        public struct HttpResponseContentTypes
        {
            public const string Excel = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string Pdf = "application/pdf";
            public const string Html = "text/HTML";
            public const string Text = "text/plain";
            public const string Word = "Application/msword";
            public const string Jpg = "image/jpeg";
            public const string Gif = "image/GIF";
        }

        public struct Characters
        {
            public static string Dot = ".";
            public static string ForwardSlash = "/";
            public static char ForwardSlashChar = '/';
            public static string QuestionMark = "?";
            public static char Pipe = '|';
            public static string Hyphen = "-";
            public static char HyphenChar = '-';
            public static string Underscore = "_";
            public static string Colon = ":";
            public static string Ampersand = "&";
            public static string Hash = "#";
            public static string OpenParentheses = "(";
            public static string CloseParentheses = ")";
            public static string Space = " ";
            public static string Plus = "+";
            public static char Comma = ',';
            public static string CommaStr = ",";
            public static string Dollar = "$";
            public static char Equal = '=';
            public static string Asperand = "@";
        }

        public struct ImageFields
        {
            public static string Title = "Title";
        }

        public struct LinkTypes
        {
            public const string External = "external";
            public const string Internal = "internal";
        }

        public struct LinkTargetTypes
        {
            public const string Blank = "_blank";
            public const string Self = "_self";
        }

        public struct DateFormats
        {
            public static string YYYY_MM_DD = "yyyy-MM-dd";
            public static string MMDDYYYY = "MM/dd/yyyy";
        }

        public struct Items
        {
            public struct Content
            {
                public const string Website = "{813AEAB3-0FE0-4ACE-AF62-BA32CAD8FB49}";
            }
        }
    }
}
