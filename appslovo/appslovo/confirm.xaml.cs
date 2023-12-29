using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;


namespace appslovo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class confirm : ContentPage
    {
        public int attempts = 7;
        public confirm(string phonenumber)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            image2.Source = ImageSource.FromResource("appslovo.message.png");
            phone_number_label.Text = phonenumber;
            info_label.Text = "Code has been sent to " + phonenumber;
        }
        async Task<string> SendRequestConfirmCheck(string phone, string confirm_code)
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = "https://apislovo.zevent.ru/Slovo/Confirmation?phone_number=" + phone + "&confirm_code=" + confirm_code;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string inserted_user = await response.Content.ReadAsStringAsync();

                res = inserted_user;

            }
            else
            {
                res = "error";
            }

            return res;
        }
        protected override bool OnBackButtonPressed()
        {
            backtolog();
            return true;
        }
        async void backtolog()
        {
            await Navigation.PushAsync(new PhoneLog(), false);
        }

        async void send_user_confirmation(object sender, EventArgs e)
        {
            var phone_number = phone_number_label.Text;
            var confirm_code = ((Entry)sender).Text;
            var confirmed_user = await SendRequestConfirmCheck(phone_number, confirm_code);
            if (confirmed_user == "bad_confirm_code")
            {
                confirm_input_error.Text = "Wrong confirmation format";
            }
            if (confirmed_user == "-1")
            {
                confirm_input_error.Text = "Wrong code \n attempts left:" + (attempts -= 1).ToString();
            }
            if (confirmed_user == "-2")
            {
                confirm_input_error.Text = "Lost Attempts";
                await Navigation.PushAsync(new PhoneLog(), false);
            }
            if (confirmed_user != "-2" && confirmed_user != "-1" && confirmed_user != "bad_confirm_code" && confirmed_user != "error")
            {
                try
                {
                    App.Current.Properties.Remove("usertoken");
                }
                catch
                {

                }
                App.Current.Properties.Add("usertoken", confirmed_user);
                await Navigation.PushAsync(new homepage(), false);
            }

        }
    }

}