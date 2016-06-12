using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using NoteTaker.ViewModels;
using NoteTaker.Views.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NoteTaker.App
{
    public sealed partial class RichTextNoteControl : UserControl
    {
        // From http://stackoverflow.com/questions/33575969/list-installed-font-names-in-windows-10-universal-app
        public static string[] FontNames = {
            "Arial", "Calibri", "Cambria", "Cambria Math", "Comic Sans MS", "Courier New",
            "Ebrima", "Gadugi", "Georgia",
            "Javanese Text Regular Fallback font for Javanese script", "Leelawadee UI",
            "Lucida Console", "Malgun Gothic", "Microsoft Himalaya", "Microsoft JhengHei",
            "Microsoft JhengHei UI", "Microsoft New Tai Lue", "Microsoft PhagsPa",
            "Microsoft Tai Le", "Microsoft YaHei", "Microsoft YaHei UI",
            "Microsoft Yi Baiti", "Mongolian Baiti", "MV Boli", "Myanmar Text",
            "Nirmala UI", "Segoe MDL2 Assets", "Segoe Print", "Segoe UI", "Segoe UI Emoji",
            "Segoe UI Historic", "Segoe UI Symbol", "SimSun", "Times New Roman",
            "Trebuchet MS", "Verdana", "Webdings", "Wingdings", "Yu Gothic",
            "Yu Gothic UI"
        };

        public RichTextNoteControl()
        {            
            InitializeComponent();
            FontFamilyComboBox.ItemsSource = FontNames;
            FontSizeComboBox.ItemsSource = new List<float>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void RichEditBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedText = RichEditBox.Document.Selection;
            BoldButton.IsChecked = (selectedText.CharacterFormat.Bold == FormatEffect.On);
            ItalicsButton.IsChecked = (selectedText.CharacterFormat.Italic == FormatEffect.On);

            FontFamilyComboBox.SelectedItem = selectedText.CharacterFormat.Name;
            FontSizeComboBox.SelectedItem = selectedText.CharacterFormat.Size;
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null)
            {
                RichEditBox.Document.Selection.CharacterFormat.Name = FontFamilyComboBox.SelectedItem.ToString();
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            float fontSize;
            if (float.TryParse(FontSizeComboBox.SelectedItem.ToString(), out fontSize))
            {
                RichEditBox.Document.Selection.CharacterFormat.Size = fontSize;
            }
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            RichEditBox.Document.Selection.CharacterFormat.Bold = Windows.UI.Text.FormatEffect.Toggle; 
        }

        private void ItalicsButton_Click(object sender, RoutedEventArgs e)
        {
            RichEditBox.Document.Selection.CharacterFormat.Italic = Windows.UI.Text.FormatEffect.Toggle;
        }

        private void RichEditBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var viewModel = RichEditBox.DataContext as RichTextNoteViewModel;

            if (viewModel != null)
            {
                string text;
                RichEditBox.Document.GetText(TextGetOptions.FormatRtf, out text);
                viewModel.RtfText = text;
            }
        }

        private void RichEditBox_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is RichTextNoteViewModel)
            {
                var viewModel = (RichTextNoteViewModel)args.NewValue;
                RichEditBox.Document.SetText(TextSetOptions.FormatRtf, viewModel.RtfText);
            }
        }
    }
}
