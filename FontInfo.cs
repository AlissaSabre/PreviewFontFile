using System;
using System.Globalization;
using System.Windows.Media;

namespace PreviewFontFile
{
    public class FontInfo
    {
        private static readonly CultureInfo DefaultCulture = CultureInfo.GetCultureInfo("en-US");

        private readonly GlyphTypeface GlyphTypeface;

        public CultureInfo Culture { get; }

        private FontInfo(GlyphTypeface glyph_typeface, CultureInfo culture)
        {
            GlyphTypeface = glyph_typeface;
            Culture = culture;
        }

        public static FontInfo From(string filename, CultureInfo culture)
        {
            return new FontInfo(new GlyphTypeface(new Uri(filename)), culture);
        }

        public string FamilyName => GlyphTypeface.FamilyNames[DefaultCulture];

        public string LocalizedFamilyName => GlyphTypeface.FamilyNames[Culture];

        public string FaceName => GlyphTypeface.FaceNames[DefaultCulture];

        public string LocalizedFaceName => GlyphTypeface.FaceNames[Culture];

        public string ManufacturerName => GlyphTypeface.ManufacturerNames[DefaultCulture];

        public string LocalizedManufacturerName => GlyphTypeface.ManufacturerNames[Culture];

        public string Copyright => GlyphTypeface.Copyrights[DefaultCulture];

        public string LocalizedCopyright => GlyphTypeface.Copyrights[Culture];

        public int GlyphCount => GlyphTypeface.GlyphCount;

    }
}