using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace appslovo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class register : ContentPage
    {
        public register(string phonenumber)
        {
            
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            phone_number_label.Text = phonenumber;



        }

        async Task<string> SendRequestUserInsert(string phone, string username, string invite_code)
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = "https://apislovo.zevent.ru/Slovo/Registration?phone_number="+ phone + "&username="+ username + "&invitecode=" + invite_code;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string inserted_user = await response.Content.ReadAsStringAsync();

                res = inserted_user;

            }
            else
            {
                res = "error(something went wrong when we tried to insert a user)";
            }

            return res;
        }

        async void go_to_entry_invite(object sender, EventArgs e)
        {
            enrty_invitecode.Focus();
        }
        async void send_user_insertion(object sender, EventArgs e)
        {
            var phone_number = phone_number_label.Text;
            var username = enrty_username.Text;
            var invite_code = ((Entry)sender).Text;
            var inserted_user = await SendRequestUserInsert(phone_number, username, invite_code);
            if (inserted_user == "success")
            {
                await Navigation.PushAsync(new confirm(phone_number), false);
            }
            if (inserted_user == "bad_username")
            {
                input_errors.Text = "the username can only consist of Latin letters, numbers and '_'";
            }
            if (inserted_user == "-1")
            {
                input_errors.Text = "this username is alredy taken, chose another one";
            }
            if (inserted_user == "-2")
            {
                input_errors.Text = "This invite code does not exist yet";
            }
            if (inserted_user == "send_error")
            {
                input_errors.Text = "Sending error";
            }
            else
            {
                input_errors.Text = inserted_user;
            }

        }
    }
}