using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace WebJsonExample
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var response = WebRequest.Create("https://cataas.com/cat?json=true").GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string jsonText = reader.ReadToEnd();
            JsonBox.Text = jsonText;
            Cat? cat = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonText, typeof(Cat)) as Cat;
            JsonInfoBox.Text = @$"Тэги: {String.Join(", ", cat!.tags)}
Cоздан: {cat!.createdAt}
Обновлён: {cat!.updatedAt}";
            Image.Source = new BitmapImage(new Uri("https://cataas.com"  + cat.url));
        }
    }
}