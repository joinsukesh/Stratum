namespace Stratum.Foundation.Common.Pipelines
{
    using HtmlAgilityPack;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines.RenderField;
    using System;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class RemoveImageAttributes
    {
        public void Process(RenderFieldArgs args)
        {
            try
            {
                if (args.FieldTypeKey != "image")
                {
                    return;
                }
                else
                {
                    string imageTag = args.Result.FirstPart;

                    if (!string.IsNullOrWhiteSpace(imageTag) && (imageTag.ToLower().Contains("width=") || imageTag.ToLower().Contains("height=")))
                    {
                        /// Approach 1:
                        HtmlDocument document = new HtmlDocument();
                        document.LoadHtml(imageTag);
                        HtmlNode image = document.DocumentNode.SelectNodes("//img")[0];

                        if (image != null)
                        {
                            if (image.Attributes["width"] != null)
                            {
                                image.Attributes["width"].Remove();
                            }
                            if (image.Attributes["height"] != null)
                            {
                                image.Attributes["height"].Remove();
                            }

                            args.Result.FirstPart = image.OuterHtml;
                        }

                        /// Approach 2: to remove attributes using Regex
                        //imageTag = Regex.Replace(imageTag, @"(<img[^>]*?)\s+width\s*=\s*\S+", "$1", RegexOptions.IgnoreCase);
                        //imageTag = Regex.Replace(imageTag, @"(<img[^>]*?)\s+height\s*=\s*\S+", "$1", RegexOptions.IgnoreCase);
                        //args.Result.FirstPart = imageTag;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("RemoveImageAttributes", ex, this);
            }
        }
    }
}
