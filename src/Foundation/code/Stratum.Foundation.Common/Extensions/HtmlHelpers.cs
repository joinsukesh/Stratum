namespace Stratum.Foundation.Common.Extensions
{
    using System;
    using System.Web.Mvc;

    public static class HtmlHelpers
    {
        public static string AntiForgeryTokenForAjaxPost(this HtmlHelper helper)
        {
            var antiForgeryInputTag = helper.AntiForgeryToken().ToString();
            /// Above gets the following: <input name="__RequestVerificationToken" type="hidden" value="some-token" />
            var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
            var tokenValue = removedStart.Replace(@""" />", "");
            if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
                throw new InvalidOperationException("The Html.AntiForgeryToken() method seems to return something that is not expected");
            return tokenValue;
        }
    }
}
