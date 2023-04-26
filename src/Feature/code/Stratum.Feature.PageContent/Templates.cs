
namespace Stratum.Feature.PageContent
{
    using Sitecore.Data;

    public class Templates
    {
        public struct ColumnsSectionRenderingParameters
        {
            public static readonly ID ID = new ID("{3032B1E2-762F-47BC-B669-E9434A93C396}");

            public struct Fields
            {
                public static readonly ID NumberOfColumns = new ID("{382F3ABB-F899-416D-B111-9A4A146921FC}");
            }
        }

        public struct RichTextSection
        {
            public static readonly ID ID = new ID("{DC23F10F-8F25-427E-BB89-686188BFC7B4}");

            public struct Fields
            {
                public static readonly ID Content = new ID("{781AFC43-7ADB-4AE6-BBA7-6710F0FE9B2B}");
            }
        }

        public struct HeroBanner
        {
            public static readonly ID ID = new ID("{68967FB1-4AAA-4856-961B-92FCC387DEB5}");

            public struct Fields
            {
                public static readonly ID BannerImage = new ID("{D502633C-1EFE-486A-A1A4-98E4352C5A47}");
                public static readonly ID CTA = new ID("{ED3310F8-8E1D-4627-81B2-9E03685492F6}");
            }
        }

        public struct Testimonial
        {
            public static readonly ID ID = new ID("{00A372FA-5623-4519-8BED-38AB7537D718}");

            public struct Fields
            {
                public static readonly ID PersonName = new ID("{866F1144-7976-4950-9145-002E20DB800C}");
                public static readonly ID Designation = new ID("{E9BAB2D2-DDF8-48D7-83B9-19FF19FFB545}");
                public static readonly ID Image = new ID("{952EB599-92B4-411A-8770-3B54BD63105A}");
                public static readonly ID Comments = new ID("{C2A28C19-049C-4D86-9FFA-811D073B9490}");
            }
        }

        public struct TestimonialsSection
        {
            public static readonly ID ID = new ID("{1AC88EDF-7673-4A93-8CDE-12C9C9D0C123}");
        }

        public struct NumberedGridTile
        {
            public static readonly ID ID = new ID("{80DEED19-C441-43D2-A3B3-B299A100E4D6}");
        }

        public struct NumberedGridTilesSection
        {
            public static readonly ID ID = new ID("{897EDB7A-ABE2-46EE-AF56-3D721A9A37A4}");

            public struct Fields
            {
                public static readonly ID ShowTileNumbers = new ID("{42438F46-B49D-4D79-B783-4446B146853D}");
            }
        }

        public struct AccordionPanel
        {
            public static readonly ID ID = new ID("{A850D245-7C89-446D-B320-5FCB8CCD0BC0}");
        }

        public struct Accordion
        {
            public static readonly ID ID = new ID("{87E04431-63D7-4E87-94B1-4E72C0990799}");
        }

        public struct TeaserTile
        {
            public static readonly ID ID = new ID("{3EBA1A1E-56A3-44A3-A7C0-E741FCFAE8CE}");

            public struct Fields
            {
                public static readonly ID ContainerCssClass = new ID("{EB153899-F0FD-45F2-A390-8EC027199DFA}");
                public static readonly ID IconCSSClass = new ID("{1AA4FD51-D455-4A26-892E-59292E9507A3}");
                public static readonly ID CTA = new ID("{1E2814E1-4385-475A-9F89-42330BC7C53F}");
            }
        }

        public struct TeaserTilesSection
        {
            public static readonly ID ID = new ID("{8B365BB6-69FE-447C-B21E-CD30762FA04C}");
        }

        public struct GalleryImage
        {
            public static readonly ID ID = new ID("{874067F3-753D-47DD-B1C6-AAFE08AB43AF}");

            public struct Fields
            {
                public static readonly ID Image = new ID("{20917D71-0843-4CB9-BA63-44B971246058}");
                public static readonly ID CTA = new ID("{3C955A22-3968-4BD5-A786-B821088F8B8C}");
            }
        }

        public struct Gallery
        {
            public static readonly ID ID = new ID("{DB52B9AA-A816-4FAD-AFAC-F7C430968266}");

            public struct Fields
            {
                public static readonly ID GalleryImageFolders = new ID("{6A93A7CC-C26C-44FD-B0B5-66AE5DFB5474}");
            }
        }

        public struct ProductDetails
        {
            public static readonly ID ID = new ID("{310C0E99-8032-45FA-A873-DF9071C1FDF9}");

            public struct Fields
            {
                public static readonly ID Image = new ID("{9A5CA56B-A6D9-410E-B38F-BF5064E3C7B1}");
                public static readonly ID Category = new ID("{2B72B092-1ED1-4391-87AE-F450BBFBF91F}");
                public static readonly ID Price = new ID("{6DA5C444-C4F6-404E-90DF-9D4F723AFA6F}");
                public static readonly ID Tags = new ID("{73C614FA-7821-4D3C-92E4-FBC53D9E8840}");
            }
        }

        public struct ProductDetailsPage
        {
            public static readonly ID ID = new ID("{73D13705-EE8B-4C57-A6E6-E44A658ECF4E}");
        }

        public struct ProductsListingRenderingParameters
        {
            public static readonly ID ID = new ID("{1E6AA4A5-6034-4949-B3D3-DC26F6054117}");            
        }
    }
}