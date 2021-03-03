using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Word
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ComboBoxInter.ItemsSource = new List<double>() { 1.0, 1.15, 1.5, 2.0, 2.5, 3.0 };
            ComboBoxFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            ComboBoxFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

            ComboBoxFontFamily.SelectedIndex = 165; //203 комп
            ComboBoxFontSize.SelectedIndex = 5;
            ComboBoxInter.SelectedIndex = 2;
            Lang.Text = InputLanguage.CurrentInputLanguage.LayoutName;

            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();

        private void BackgroundTextColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (BackgroundTextColor == null || !BackgroundTextColor.SelectedColor.HasValue) return;
            var color = new SolidColorBrush(BackgroundTextColor.SelectedColor.Value);
            BackgroundColor.Foreground = color;
        }

        private void TextColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (TextColor == null || !TextColor.SelectedColor.HasValue) return;
            var color = new SolidColorBrush(TextColor.SelectedColor.Value);
            ForegroundColor.Foreground = color;
            Word.Selection.ApplyPropertyValue(Inline.ForegroundProperty, color);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var color = BackgroundColor.Foreground;
            Word.Selection.ApplyPropertyValue(Inline.BackgroundProperty, color);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var color = ForegroundColor.Foreground;
            Word.Selection.ApplyPropertyValue(Inline.ForegroundProperty, color);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) => Word.SelectAll();

        private void ComboBoxFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxFontFamily.SelectedItem != null)
                Word.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFontFamily.SelectedItem);
        }

        private void ComboBoxFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxFontSize.SelectedItem == null) return;
            Word.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxFontSize.SelectedItem);
            if (ComboBoxInter.SelectedItem == null) return;
            Paragraph paragraph = Word.Document.Blocks.FirstBlock as Paragraph;
            paragraph.LineHeight = ((double)ComboBoxInter.SelectedItem - 0.5) * (double)ComboBoxFontSize.SelectedItem;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int a = ComboBoxFontSize.SelectedIndex + 1;
            if (a > 15) return;
            ComboBoxFontSize.SelectedIndex = a;
            if (ComboBoxFontSize.SelectedItem == null) return;
            Word.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxFontSize.SelectedItem);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int a = ComboBoxFontSize.SelectedIndex - 1;
            if (a < 0) return;
            ComboBoxFontSize.SelectedIndex = a;
            if (ComboBoxFontSize.SelectedItem == null) return;
            Word.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxFontSize.SelectedItem);           
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (Bold.IsChecked != true) Word.Selection.ApplyPropertyValue(Inline.FontWeightProperty, "Normal");
            if (Bold.IsChecked == true) Word.Selection.ApplyPropertyValue(Inline.FontWeightProperty, "Bold");
        }

        private void ToggleButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (Italic.IsChecked != true) Word.Selection.ApplyPropertyValue(Inline.FontStyleProperty, "Normal");
            if (Italic.IsChecked == true) Word.Selection.ApplyPropertyValue(Inline.FontStyleProperty, "Italic");
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            if (Underline.IsChecked != true) Word.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            if (Underline.IsChecked == true) Word.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, "Underline");
        }

        private void Strikethrough_Click(object sender, RoutedEventArgs e)
        {
            if (Strikethrough.IsChecked != true) Word.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            if (Strikethrough.IsChecked == true) Word.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, "Strikethrough");
        }

        private void ComboBoxInter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxInter.SelectedItem == null) return;
            Paragraph paragraph = Word.Document.Blocks.FirstBlock as Paragraph;
            paragraph.LineHeight = ((double)ComboBoxInter.SelectedItem - 0.5) * (double)ComboBoxFontSize.SelectedItem;
        }


        private void Word_TextChanged(object sender, TextChangedEventArgs e)
        {
            string rrt = new TextRange(Word.Document.ContentStart, Word.Document.ContentEnd).Text;
            string[] words = rrt.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Words.Text = words.Length.ToString();

            Letters.Text = (rrt.Replace(" ", "").Length - 2).ToString();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (GridMenu.IsVisible == true)
                GridMenu.Visibility = Visibility.Hidden;
            else
                GridMenu.Visibility = Visibility.Visible;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Maximized;

        private void Button_Click_8(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() != true) return;
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.InitialDirectory = "c:/";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";
            string path = openFileDialog.FileName;
        }
    }

}
