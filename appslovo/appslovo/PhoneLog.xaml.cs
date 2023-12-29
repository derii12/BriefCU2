using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
namespace appslovo
 
{
    public partial class PhoneLog : ContentPage
    {
        public PhoneLog()
        {

            App.Current.Properties.Remove("usertoken");
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        async Task<string> SendRequest(string text)
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = "https://apislovo.zevent.ru/slovo/Phone_number_checking?phone_number=" + text;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string found_user =  await response.Content.ReadAsStringAsync();

                res = found_user;
            
            }
            else
            {
                res = "error(something went wrong when we tried to find a user)";
            }

            return res;
        }




        async void getid(object idsender, EventArgs e)
        {

           

            var text = ((Entry)idsender).Text;
            var texts = text.Replace("+", string.Empty);
            var user_with_enered_phone = await SendRequest(texts);
            if (user_with_enered_phone == "found")
            {
                phone_input_error.Text = "";
                await Navigation.PushAsync(new confirm(texts), false);
            }
            if (user_with_enered_phone == "notfound")
            {
                phone_input_error.Text = "";
                await Navigation.PushAsync(new register(texts), false);
            }
            else
            {
                phone_input_error.Text =  user_with_enered_phone;
            }
        }
    }
}
