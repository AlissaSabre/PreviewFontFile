using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PreviewFontFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void fontButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".ttf",
                Filter = "Font file|*.ttf;*.ttc;*.otf",
            };
            if (dlg.ShowDialog(this) == true)
            {
                fontTextBlock.Text = dlg.FileName;
                sampleGlyphs.FontUri = new Uri(dlg.FileName);
                infoTextBox.Text = GetFontInformationTexts(dlg.FileName);
            }
        }

        private string GetFontInformationTexts(string filename)
        {
            var f = FontInfo.From(filename , CultureInfo.CurrentUICulture);
            var sb = new StringBuilder();
            sb.AppendLine($"Contains {f.GlyphCount} glyphs");
            sb.AppendLine(PossiblyLocalizedName(f.FamilyName, f.LocalizedFamilyName));
            sb.AppendLine(PossiblyLocalizedName(f.FaceName, f.LocalizedFaceName));
            sb.AppendLine(PossiblyLocalizedName(f.ManufacturerName, f.LocalizedManufacturerName));
            sb.AppendLine(f.LocalizedCopyright ?? f.Copyright);
            return sb.ToString();
        }

        private string PossiblyLocalizedName(string invariant, string localized)
        {
            if (localized is null || localized == invariant)
            {
                return invariant;
            }
            else
            {
                return invariant + " (" + localized + ")";
            }
        }
    }

    /// <summary>Turns an empty string into a single space.</summary>
    /// <remarks>
    /// <see cref="Glyphs"/> doesn't want its <see cref="Glyphs.UnicodeString"/> to be empty.
    /// This converter guarantees non-empty strings.
    /// </remarks>
    [ValueConversion(typeof(string), typeof(string))]
    public class NonEmptyString : IValueConverter
    {
        /// <summary>A stock instance of <see cref="NonEmptyString"/>.</summary>
        public static readonly NonEmptyString Instance = new NonEmptyString();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            if (s is null)
            {
                return DependencyProperty.UnsetValue;
            }
            else if (string.IsNullOrWhiteSpace(s))
            {
                return " ";
            }
            else
            {
                return s;
            }
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException($"{nameof(NonEmptyString)} doesn't provide ConvertBack");
        }
    }
}
