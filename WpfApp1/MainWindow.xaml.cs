using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WpfApp1
{

   

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        Rate GetRateById(int id)
        {
            String url = "https://www.nbrb.by/api/exrates/Rates/" + Convert.ToString(id);
            //объект, который будет соединяться с сайтом
            System.Net.WebClient client = new System.Net.WebClient();
            //объект для десериализации строки (дешифратор)
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //Строка в формате json
            //{"Cur_ID":298,"Date":"2021-03-18T00:00:00","Cur_Abbreviation":"RUB"
            String json = client.DownloadString(url);
            //преобразование строки в объект класса Rate
            Rate rate = serializer.Deserialize<Rate>(json);
            MessageBox.Show(rate.Cur_Abbreviation.ToString());
            return rate;
        }
        void GetRateByName(string name)
        {
            String url = "https://www.nbrb.by/api/exrates/currencies";
            //объект, который будет соединяться с сайтом
            System.Net.WebClient client = new System.Net.WebClient();
            //объект для десериализации строки (дешифратор)
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //Строка в формате json
            //{"Cur_ID":298,"Date":"2021-03-18T00:00:00","Cur_Abbreviation":"RUB
            String json = client.DownloadString(url);
            //преобразование строки в объект класса Rate
            //Rate izChego = serializer.Deserialize<Rate>(json);
            List <Currency> currency = new List<Currency>();
            //Currency c = serializer.Deserialize<Currency>(json);

            // currency.Add( serializer.Deserialize<Currency>(json));
            MessageBox.Show(currency.Capacity.ToString());
            //return currency.First<Currency>;
        }

        public MainWindow()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 1;      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //строка для запроса
            //GetRateById(298);
            GetRateByName("RUB");

        }

        private void CursesUpdate(object sender, EventArgs e)
        {

        }
    }
}
