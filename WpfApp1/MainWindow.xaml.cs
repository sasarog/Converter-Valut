using Newtonsoft.Json;
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
        List<Currency> GetCurrencyList()
        {
            String url = "https://www.nbrb.by/api/exrates/currencies";
            //объект, который будет соединяться с сайтом
            System.Net.WebClient client = new System.Net.WebClient();           
            String json = client.DownloadString(url);            
            List<Currency> currency = new List<Currency>();
            currency = JsonConvert.DeserializeObject<List<Currency>>(json);
            return currency;
        }

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
            //MessageBox.Show(rate.Cur_Abbreviation.ToString());
            return rate;
        }
        int GetIDByName(string name)
        {
            List<Currency> currencies = GetCurrencyList();
            return currencies.FindLast((x) => x.Cur_Abbreviation == name).Cur_ID;
        }

        public MainWindow()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 1;      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int indexOfIshValuta, indexOfKonValuta;
            double otnKRublu = 0, otnOtRubla = 0, rubli; 
            if (listBox1.SelectedIndex == 0 || listBox2.SelectedIndex == 0)
            {
                MessageBox.Show("Вы не выбрали валюту.");
                return;
            }
            indexOfIshValuta = listBox1.SelectedIndex;
            indexOfKonValuta= listBox2.SelectedIndex;
            Rate rateof, rateTo = new Rate();
            switch (indexOfIshValuta)
            {
                case 1:
                    otnKRublu = 1;
                    break;
                case 2:
                    rateof = GetRateById(GetIDByName("USD"));
                    otnKRublu = rateof.Cur_OfficialRate / rateof.Cur_Scale;
                    break; 
                case 3:
                    rateof = GetRateById(GetIDByName("EUR"));
                    otnKRublu = rateof.Cur_OfficialRate / rateof.Cur_Scale;
                    break; 
                case 4:
                    rateof = GetRateById(GetIDByName("PLN"));
                    otnKRublu = rateof.Cur_OfficialRate / rateof.Cur_Scale;
                    break; 
                case 5:
                    rateof = GetRateById(GetIDByName("RUB"));
                    otnKRublu = rateof.Cur_OfficialRate / rateof.Cur_Scale;
                    break;
                default:
                    break;
            }
            
            switch (indexOfKonValuta)
            {
                case 1:
                    otnOtRubla = 1;
                    break;
                case 2:
                    rateTo = GetRateById(GetIDByName("USD"));
                    otnOtRubla = rateTo.Cur_OfficialRate / rateTo.Cur_Scale;
                    break;
                case 3:
                    rateTo = GetRateById(GetIDByName("EUR"));
                    otnOtRubla = rateTo.Cur_OfficialRate / rateTo.Cur_Scale;
                    break;
                case 4:
                    rateTo = GetRateById(GetIDByName("PLN"));
                    otnOtRubla = rateTo.Cur_OfficialRate / rateTo.Cur_Scale;
                    break;
                case 5:
                    rateTo = GetRateById(GetIDByName("RUB"));
                    otnOtRubla = rateTo.Cur_OfficialRate / rateTo.Cur_Scale;
                    break;
                default:
                    break;
            }
            rubli = Convert.ToDouble(textBox1.Text) * otnKRublu / otnOtRubla;
            textBox2.Text = rubli.ToString();

        }


        private void CursesUpdate(object sender, EventArgs e)
        {

        }
    }
}
