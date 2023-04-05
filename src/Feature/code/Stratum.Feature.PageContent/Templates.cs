
namespace Stratum.Feature.PageContent
{
    using Sitecore.Data;

    public class Templates
    {
        public struct ColumnsSectionRenderingParameters
        {
            public static readonly ID ID = new ID("{8177ABD8-707D-4D4D-8125-FD95F111CC3F}");

            public struct Fields
            {
                public static readonly ID NumberOfColumns = new ID("{AF4C9B41-DB42-45E4-9E9C-BF99FEAA3B32}");
            }
        }

        public struct RichTextSection
        {
            public static readonly ID ID = new ID("{4C6B06F7-4962-405D-9C0B-FB73C3205E03}");

            public struct Fields
            {
                public static readonly ID Content = new ID("{500C7AAD-3596-4796-A103-3DAA84BEAD7C}");
            }
        }

        public struct HeroBanner
        {
            public static readonly ID ID = new ID("{651E3A86-870B-43BD-88C8-64D398F58960}");

            public struct Fields
            {
                public static readonly ID BannerImage = new ID("{4350FA48-261C-49FB-BE61-A949FF0D3670}");
                public static readonly ID Title = new ID("{F8C90ACE-DE00-4B7B-BDD0-5F6B9176DDD4}");
                public static readonly ID Description = new ID("{91ABF899-71EF-4A57-B972-89844E82170E}");
                public static readonly ID CTA = new ID("{10870063-17D9-48F6-9FA3-4574F6C36D68}");
            }
        }

        public struct Testimonial
        {
            public static readonly ID ID = new ID("{788A578D-85D8-44E6-8080-6E521CFC8F46}");

            public struct Fields
            {
                public static readonly ID PersonName = new ID("{85DCEB86-FE66-49C5-B034-854DB9AA6721}");
                public static readonly ID Designation = new ID("{0091404E-2AFD-40B0-B199-34663E3EE889}");
                public static readonly ID Image = new ID("{F0C553EF-8132-49C0-9D5B-F7B7BC7192FE}");
                public static readonly ID Comments = new ID("{38CC79E8-1B91-48B8-81AC-45424914E528}");
            }
        }

        public struct TestimonialsSection
        {
            public static readonly ID ID = new ID("{07AD3624-F7AC-45A1-836B-AC86871AED2B}");
        }

        public struct NumberedGridTile
        {
            public static readonly ID ID = new ID("{9CBF8323-B9DE-4B7D-A9B2-86E83D1FB885}");
        }

        public struct NumberedGridTilesSection
        {
            public static readonly ID ID = new ID("{113D72A0-B150-43F7-890A-589B7B62E7C9}");

            public struct Fields
            {
                public static readonly ID ShowTileNumbers = new ID("{9B0A4F9F-E9F6-4CAA-BE7E-794CBFB4035A}");
            }
        }

        public struct AccordionPanel
        {
            public static readonly ID ID = new ID("{612899AE-85E5-49DC-B63A-2FEBA973A669}");
        }

        public struct Accordion
        {
            public static readonly ID ID = new ID("{E9DB87BC-9EC5-4C89-88A8-6462C1068DDC}");
        }

        public struct TeaserTile
        {
            public static readonly ID ID = new ID("{B57139B4-A1C4-4BDC-ADE0-C14B33E15FEA}");

            public struct Fields
            {
                public static readonly ID ContainerCssClass = new ID("{24527341-304D-43AC-AA8F-3634A38AA9A8}");
                public static readonly ID IconCSSClass = new ID("{60BA6E00-CEA0-4FBB-8AB9-544F68E3A565}");
                public static readonly ID CTA = new ID("{8165818E-653F-42A8-8CF1-8BD41B38BC06}");
            }
        }

        public struct TeaserTilesSection
        {
            public static readonly ID ID = new ID("{76E8616A-500C-4A7F-8D4B-DE02D2F34C49}");
        }

        public struct GalleryImage
        {
            public static readonly ID ID = new ID("{DC5A7FCC-DE7B-4E62-BCA4-0048B567AC36}");

            public struct Fields
            {
                public static readonly ID Image = new ID("{7D6821AE-F56C-497C-B847-1C0734EB95ED}");
                public static readonly ID CTA = new ID("{27C46797-EE9A-4DFB-A541-462AADBE2D1A}");
            }
        }

        public struct Gallery
        {
            public static readonly ID ID = new ID("{2CD5BB0E-8A90-4E83-8AEE-6770A62224CA}");

            public struct Fields
            {
                public static readonly ID GalleryImagesFolder = new ID("{17F651FD-125E-4363-9602-72753C5D2E5F}");
            }
        }

        public struct ProductDetails
        {
            public static readonly ID ID = new ID("{1D565718-9033-4651-852B-C4CFAB7547D2}");

            public struct Fields
            {
                public static readonly ID Image = new ID("{60F5922F-7D23-4446-844A-962631528226}");
                public static readonly ID Category = new ID("{E9B64A12-BEA2-45A7-AD39-0F17BD1A0187}");
                public static readonly ID Price = new ID("{B2C21E83-3C39-4BC5-AF68-C3BC6DE80FF8}");
                public static readonly ID Tags = new ID("{FCCCB624-8455-4702-95A0-A2B7D3764494}");
            }
        }

        public struct ProductDetailsPage
        {
            public static readonly ID ID = new ID("{8296C862-90B7-4FD9-AB29-68FC58CA2285}");
        }

        public struct ProductsListingRenderingParameters
        {
            public static readonly ID ID = new ID("{6F0AB7B7-CD1C-48D7-8B0D-7B4AC558CEF9}");            
        }

    }
}