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
        public MainWindow()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 1;
      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //строка для запроса
            String url = "https://www.nbrb.by/api/exrates/rates/";
            String url2 = "https://www.nbrb.by/api/exrates/rates/";
            //System.Windows.Controls.ListBoxItem
            
            switch (listBox1.SelectedItem.ToString())
            {
                case "System.Windows.Controls.ListBoxItem: RUB":
                    
                    url = url +"298";
                    break;
                case "System.Windows.Controls.ListBoxItem: USD":
                    url = url + "145";
                    break;
                case "System.Windows.Controls.ListBoxItem: EUR":
                    url = url + "19";
                    break;
                case "System.Windows.Controls.ListBoxItem: PLN":
                    url = url + "293";
                    break;                
                default:
                    break;
            }
            switch (listBox2.SelectedItem.ToString())
            {
                case "System.Windows.Controls.ListBoxItem: RUB":

                    url2 = url2 + "298";
                    break;
                case "System.Windows.Controls.ListBoxItem: USD":
                    url2 = url2 + "145";
                    break;
                case "System.Windows.Controls.ListBoxItem: EUR":
                    url2 = url2 + "19";
                    break;
                case "System.Windows.Controls.ListBoxItem: PLN":
                    url2 = url2 + "293";
                    break;
                default:
                    break;
            }

            double otnKRublu = 0;
            double otnKValute = 0;
            double vvodDeneg = 0;
            try
            {
                vvodDeneg = Convert.ToDouble(textBox1.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода количества денег");
            }
            
            
            
            //объект, который будет соединяться с сайтом
            System.Net.WebClient client = new System.Net.WebClient();
            //объект для десериализации строки (дешифратор)
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
            String json2 = client.DownloadString(url2);
            
            if (listBox1.SelectedItem.ToString() == "System.Windows.Controls.ListBoxItem: BYN")
            {
                otnKRublu = 1;
            }
            else
            {
                //Строка в формате json
                //{"Cur_ID":298,"Date":"2021-03-18T00:00:00","Cur_Abbreviation":"RUB
                String json = client.DownloadString(url);
                //преобразование строки в объект класса Rate
                Rate izChego = serializer.Deserialize<Rate>(json);
                otnKRublu = izChego.Cur_Scale;
            }
            Rate voCto = serializer.Deserialize<Rate>(json2);
            otnKValute = Convert.ToDouble( voCto.Cur_OfficialRate);
            textBox2.Text =(vvodDeneg * otnKRublu * otnKValute).ToString();





































        }
    }
}
