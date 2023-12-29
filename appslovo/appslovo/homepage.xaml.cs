using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading;

using System.Net.Http;
using System.Collections.Generic;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
namespace appslovo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class homepage : ContentPage
        
    {
        public bool timer_disabled = false;
        public string user_actual_post_txt = "";
        public bool activate = true;
        public string web_connection = "https://apislovo.zevent.ru/Slovo/";
        public Entry entryactive = null;
        async Task<string> GetUsername(string token) //получение нужной информации о пользователе (на вход подается токен и проверка токена)
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "username_get?token=" + token;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string username = await response.Content.ReadAsStringAsync();

                res = username;

            }
            else
            {
                res = "error(something went wrong when we tried to insert a user)";
            }

            return res;
        }

        async Task<string> NewPost(string token, string post_txt) //получение нужной информации о пользователе (на вход подается токен и проверка токена)
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "new_post?token="+ token + "&post_text=" + post_txt;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string status = await response.Content.ReadAsStringAsync();

                res = status;

            }
            else
            {
                res = "error(something went wrong when we tried to insert a post)";
            }

            return res;
        }


        async Task<string> SearchUsers(string token, string search_stroke) //search
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "search_user?token=" + token + "&search_stroke="+search_stroke;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result_stroke = await response.Content.ReadAsStringAsync();

                res = result_stroke;

            }
            else
            {
                res = "error";
            }

            return res;
        }


        async Task<string> LoadFriends(string token) //search
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "load_friends?token=" + token;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result_stroke = await response.Content.ReadAsStringAsync();

                res = result_stroke;

            }
            else
            {
                res = "error";
            }

            return res;
        }


        async Task<string> LoadFriendsPosts(string token) //search
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "load_friends_posts?token=" + token;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result_stroke = await response.Content.ReadAsStringAsync();

                res = result_stroke;

            }
            else
            {
                res = "error";
            }

            return res;
        }

        async Task<string> LoadFriendsRequests(string token) //search
        {

            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "load_requests?token=" + token;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result_stroke = await response.Content.ReadAsStringAsync();

                res = result_stroke;

            }
            else
            {
                res = "error";
            }

            return res;

        }

        async Task<string> AddFriend(string token, string username) //
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "add_friend?token=" + token + "&username="+ username;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result_stroke = await response.Content.ReadAsStringAsync();

                res = result_stroke;

            }
            else
            {
                res = "error";
            }

            return res;
        }

        async Task<string> AcceptFriend(string token, string username) //
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "accept_friend?token="+ token + "&username="+ username;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result_stroke = await response.Content.ReadAsStringAsync();

                res = result_stroke;

            }
            else
            {
                res = "error";
            }

            return res;
        }

        async Task<string> DeclineFriend(string token, string username) //
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "delete_friend?token=" + token + "&username=" + username;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result_stroke = await response.Content.ReadAsStringAsync();

                res = result_stroke;

            }
            else
            {
                res = "error";
            }

            return res;
        }

        async Task<string> EditUsernameSend(string token, string new_username) //
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "edit_username?token=" + token + "&new_username=" + new_username;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result_stroke = await response.Content.ReadAsStringAsync();
                res = result_stroke;
                if (res == "1" || username_label.Text == edit_username_entry.Text)
                {
                    error_alert.Text = "";
                    edit_username_entry.Unfocus();
                    edit_username_entry.IsVisible = false;
                    entryactive = null;
                    username_label.IsVisible = true;
                    username_label.Text = edit_username_entry.Text;
                    action_username_button.Text = "Edit";
                }
                else
                {
                    if (res == "bad_username")
                    {
                        if (edit_username_entry.Text.Length < 3)
                        {
                            error_alert.Text = "The username must consist more than 3 symbols";
                        }
                        else
                        {
                            error_alert.Text = "The username can only consist of Latin letters, numbers and '_'";
                        }
                    }
                    else
                    {
                        error_alert.Text = "The username has alredy taken";
                    }
                }
            }
            else
            {
                res = "error";
            }

            return res;
        }

        async Task<string> LoadUserPost(string token) //
        {
            string res = "";

            HttpClient client = new HttpClient();

            string Url = web_connection + "load_post?token=" + token;

            Uri uri = new Uri(string.Format(Url));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result_stroke = await response.Content.ReadAsStringAsync();

                res = result_stroke;

            }
            else
            {
                res = "error";
            }

            return res;
        }
        async void Add_friend_by_name(string username)
        {
            var token = App.Current.Properties["usertoken"].ToString();
            var search_result_str = await AddFriend(token, username);
            var search_user = AddFriendEntry.Text;
            Search_user_by_name(search_user);
        }
        async void EditUserPost(string post_txt)
        {
            var token = App.Current.Properties["usertoken"].ToString();
            if (Regex.IsMatch(post_txt, @"^[а-яА-Яa-zA-Z0-9_\n.,!?%\s-=+]+$") || post_txt == "")
            {
                if (post_txt.Length > 3 || post_txt == "")
                {
                    error_alert_post.Text = "";
                    timer_disabled = true;
                    await Task.Delay(1001);
                    var result_str = await NewPost(token, post_txt);
                    load_post(true);
                }
                else
                {
                    error_alert_post.Text = "Post should consist more than 3 symbols!";
                }
            }
            else
            {
                error_alert_post.Text = "Use only latin, russian letters, digits and punctuation";
                load_post(true);
            }
        }

        async void Accept_friend_by_name(string username)
        {
            var token = App.Current.Properties["usertoken"].ToString();
            var accept_result_str = await AcceptFriend(token, username);
        }
        async void Decline_friend_by_name(string username)
        {
            var token = App.Current.Properties["usertoken"].ToString();
            var decline_result_str = await DeclineFriend(token, username);
        }

        async void Edit_username(string new_username)
        {
            var token = App.Current.Properties["usertoken"].ToString();
            var decline_result_str = await EditUsernameSend(token, new_username);
        }

        public async void swaptolog()
        {
            await Navigation.PushAsync(new PhoneLog(), false);

        }
        public void OpenEditor(object sender, EventArgs e)
        {
             if (BlockEntry.IsVisible)
            {
                BlockEntry.IsVisible = false;
                TextPost.IsVisible = true;
                TextPostEntry.Focus();
                error_alert_post.Text = "";
            }
            else
            {
                if (TextPostEntry.Text != user_actual_post_txt || TextPostEntry.Text != "" && user_actual_post_txt == "")
                {
                    EditUserPost(TextPostEntry.Text);
                }
                
                if (TextPostEntry.Text == user_actual_post_txt && TextPostEntry.Text != "")
                {
                    exist_post.Text = TextPostEntry.Text;
                }
                TextPostEntry.Unfocus();
                BlockEntry.IsVisible = true;
                TextPost.IsVisible = false;
            }
        }
        public async void CopyInvite(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(invite_label.Text);
        }

        public async void load_post(bool events)
        {
            try
            {
                exist_post.TextColor = Color.FromHex("#D8D8D8");
                var token = App.Current.Properties["usertoken"].ToString();
                var user_post_txt = await LoadUserPost(token);
                user_post_txt = user_post_txt.Replace("}", "");
                user_post_txt = user_post_txt.Replace("{", "");
                string[] results = user_post_txt.Split(';');
                user_actual_post_txt = results[0];
                if (events)
                {
                    
                    exist_post.Text = results[0];
                    TextPostEntry.Text = exist_post.Text;
                    timer_disabled = false;
                    PostTimer(Convert.ToInt32(results[1]));
                    var countsmb = exist_post.Text.Length.ToString() + "/250";
                    counter.Text = countsmb;
                    symbcount.Text = countsmb;
                }
                else
                {
                    timer_disabled = true;
                    counter_static.Text = "";
                    await Task.Delay(1100);
                    timer_disabled = false;
                    PostTimer(Convert.ToInt32(results[1]));
                }
            }
            catch
            {
                exist_post.TextColor = Color.FromHex("#A9A9A9");
                exist_post.Text = "Post your quote.";
                timer_disabled = true;
                counter_static.Text = "";
                counter.Text = "0/250";
                symbcount.Text = "0/250";
            }
        }

        async void Load_User_Requests(string previous_result)
        {
            var token = App.Current.Properties["usertoken"].ToString();
            var friends_result_str = await LoadFriendsRequests(token);
            friends_result_str = friends_result_str.Replace("}", "");
            friends_result_str = friends_result_str.Replace("{", "");
            if (friends_result_str != "notfound" && friends_result_str != "error" && friends_result_str != previous_result)
            {
                RequestText.IsVisible = true;
                RequestsResultStack.Children.Clear();
                string[] results = friends_result_str.Split(';');

                StackLayout LoadRequestsLayout = RequestsResultStack;
                Frame FrameLoad = null;

                foreach (var username in results)
                {
                    Frame DeclineFriend = new Frame()
                    {
                        Content = new StackLayout()
                        {
                            Children = {
                                            new Label() { Text="Decline", HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions= LayoutOptions.CenterAndExpand, TextColor=Color.FromHex("#ED4A34"), FontSize=15, FontFamily="Inter", CharacterSpacing= 0}
                                        },
                            Padding = new Thickness(0, 0, 0, 0)
                        },
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Padding = new Thickness(2, 20, 6, 20),
                        Margin = new Thickness(0, -4, 0, 0),
                        HasShadow = false,
                        BackgroundColor = Color.FromHex("#222528")
                    };
                    Frame AcceptFriend = new Frame()
                    {
                            Content = new StackLayout()
                            {
                                Children = {
                                            new Label() { Text="Accept", HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions= LayoutOptions.CenterAndExpand, TextColor=Color.FromHex("#3472ED"), FontSize=15, FontFamily="Inter", CharacterSpacing= 0}
                                        },
                                Padding = new Thickness(0, 0, 0, 0)
                            },
                            HorizontalOptions = LayoutOptions.End,
                            Padding = new Thickness(2, 20, 0, 20),
                            Margin = new Thickness(0, -4, 0, 0),
                            HasShadow = false,
                            BackgroundColor = Color.FromHex("#222528")
                        

                    };
                    FrameLoad = new Frame()
                    {
                        Content = new StackLayout()
                        {
                            Children =
                            {
                                new Label() { Text = $"{username}", HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions= LayoutOptions.CenterAndExpand, TextColor=Color.FromHex("#D8D8D8"), FontSize=21, FontFamily="Inter", CharacterSpacing=1},
                               DeclineFriend,
                               AcceptFriend
                            },
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Center
                        },
                        BackgroundColor = Color.FromHex("#222528"),
                        CornerRadius = 10,
                        HeightRequest = 60,
                        Padding = new Thickness(20, 0, 20, 0)

                    };
                        var AcceptRecognizer = new TapGestureRecognizer();
                        AcceptRecognizer.Tapped += (s, e) =>
                        {
                            if (entryactive == null)
                            {
                                var accept_friend_username = username;
                                Accept_friend_by_name(accept_friend_username);
                            }
                            else
                            {
                                entryactive.Focus();
                            }
                        };
                        AcceptFriend.GestureRecognizers.Add(AcceptRecognizer);

                        var DeclineRecognizer = new TapGestureRecognizer();
                        DeclineRecognizer.Tapped += (s, e) =>
                        {
                            if (entryactive == null)
                            {
                                var accept_friend_username = username;
                                Decline_friend_by_name(accept_friend_username);
                            }
                            else
                            {
                                entryactive.Focus();
                            }
                        };
                        DeclineFriend.GestureRecognizers.Add(DeclineRecognizer);
                    

                    LoadRequestsLayout.Children.Add(FrameLoad);
                }
                RequestsResultStack = LoadRequestsLayout;
            }
            else
            {
                if (friends_result_str == "notfound" || friends_result_str == "error")
                {
                    RequestText.IsVisible = false;
                    RequestsResultStack.Children.Clear();
                }
            }
            if (activate)
            {
                await Task.Delay(1111);
                Load_User_Requests(friends_result_str);
            }
        }












        async void Load_User_Friends_Posts(string previous_result)
        {
            var token = App.Current.Properties["usertoken"].ToString();
            var posts_result_str = await LoadFriendsPosts(token);
            if (posts_result_str != "notfound" && posts_result_str != "error")
            {
                PostsResultStack.Children.Clear();
                string[] post_info_list = posts_result_str.Split('•');
                string[] post_info_list_time = post_info_list[1].Split('|');
                string[] results_info_list = post_info_list[0].Split('~');

                StackLayout LoadFriendsPostsLayout = PostsResultStack;
                Frame FrameLoad = null;
                for (int i = 0; i < results_info_list.Length; i++)
                {
                    var text_info = results_info_list[i];
                    var time_info = post_info_list_time[i]; // количество секунд до удаления
                    var posttxt = text_info.Split('|')[0]; // текст поста
                    var postauthor = text_info.Split('|')[1]; // автор поста
                    var time_info_int = Convert.ToInt32(time_info);

                    FrameLoad = new Frame()
                    {
                        Content = new StackLayout()
                        {
                            Children =
                            {
                                new Frame()
                                {
                                    Content = new StackLayout()
                                    {
                                        Children =
                                        {
                                            new Label(){Text=$"{postauthor}", FontFamily="Inter", FontSize=14, Margin = new Thickness(5,0,0,2),VerticalOptions=LayoutOptions.CenterAndExpand,TextColor= Color.FromHex("#A9A9A9")},
                                            new Label(){Text=$"{TimeChecker(Convert.ToInt32(time_info))}", FontFamily="Inter", FontSize=12, Margin = new Thickness(0,2,5,0),HorizontalOptions=LayoutOptions.EndAndExpand,TextColor= Color.FromHex("#A9A9A9")}
                                        },
                                        Orientation = StackOrientation.Horizontal
                                    },
                                    BackgroundColor = Color.FromHex("#111417"),
                                    CornerRadius = 10,
                                    Padding = new Thickness(5,5,5,5)
                                    
                                },
                                new Label() { Text=$"{posttxt}", FontSize=20, FontFamily="Inter", Margin= new Thickness(10,5,0,0), TextColor= Color.FromHex("#D8D8D8")}

                            },
                            VerticalOptions = LayoutOptions.Center
                        },
                        BackgroundColor = Color.FromHex("#222528"),
                        CornerRadius = 13,
                        Padding = new Thickness(7, 7, 7, 15),
                        Margin = new Thickness(3, 10, 3, 10),
                        MinimumHeightRequest = 100

                    };
                    LoadFriendsPostsLayout.Children.Add(FrameLoad);
                }
                PostsResultStack = LoadFriendsPostsLayout;
            }
            else
            {
                if (posts_result_str == "notfound")
                {
                    PostsResultStack.Children.Clear();
                }
            }
            await Task.Delay(1000);
            if (activate)
            {
                Load_User_Friends_Posts(posts_result_str.Split('•')[0]);
            }
        }












        async void Load_User_Friends(string previous_result)
        {
            var token = App.Current.Properties["usertoken"].ToString();
            var friends_result_str = await LoadFriends(token);
            friends_result_str = friends_result_str.Replace("}", "");
            friends_result_str = friends_result_str.Replace("{", "");
            if (friends_result_str != "notfound" && friends_result_str != "error" && friends_result_str != previous_result)
            {
                FriendsText.IsVisible = true;
                FriendsResultStack.Children.Clear();
                string[] results = friends_result_str.Split(';');

                StackLayout LoadFriendsLayout = FriendsResultStack;
                Frame FrameLoad = null;
                foreach (var username in results)
                {
                    Frame DeleteFriend = new Frame()
                    {
                        Content = new StackLayout()
                        {
                            Children = {
                                            new Label() { Text="Delete", HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions= LayoutOptions.CenterAndExpand, TextColor=Color.FromHex("#ED4A34"), FontSize=15, FontFamily="Inter", CharacterSpacing= 0}
                                        },
                            Padding = new Thickness(0, 0, 0, 0)
                        },
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Padding = new Thickness(2, 20, 0, 20),
                        Margin = new Thickness(0, -4, 0, 0),
                        HasShadow = false,
                        BackgroundColor = Color.FromHex("#222528")
                    };
                    FrameLoad = new Frame()
                    {
                        Content = new StackLayout()
                        {
                            Children =
                            {
                                new Label() { Text = $"{username}", HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions= LayoutOptions.CenterAndExpand, TextColor=Color.FromHex("#D8D8D8"), FontSize=21, FontFamily="Inter", CharacterSpacing=1},
                                DeleteFriend
                            },
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Center
                        },
                        BackgroundColor = Color.FromHex("#222528"),
                        CornerRadius = 10,
                        HeightRequest = 60,
                        Padding = new Thickness(20, 0, 20, 0)

                    };
                        var DeleteRecognizer = new TapGestureRecognizer();
                        DeleteRecognizer.Tapped += (s, e) =>
                        {
                            if (entryactive == null)
                            {
                                var delete_friend_username = username;
                                Decline_friend_by_name(delete_friend_username);
                            }
                            else
                            {
                                entryactive.Focus();
                            }
                        };

                        DeleteFriend.GestureRecognizers.Add(DeleteRecognizer);
                    LoadFriendsLayout.Children.Add(FrameLoad);
                }
                FriendsResultStack = LoadFriendsLayout;
            }
            else
            {
                if (friends_result_str == "notfound")
                {
                    FriendsText.IsVisible = false;
                    FriendsResultStack.Children.Clear();
                }
            }
                await Task.Delay(1111);
            if (activate)
            {
                Load_User_Friends(friends_result_str);
            }
        }

        async void Search_user_by_name(string search_stroke)
        {
            var token = App.Current.Properties["usertoken"].ToString();
            var search_result_str = await SearchUsers(token, search_stroke);
            SearchResultStack.Children.Clear();
            if (search_result_str != "notfound" && search_result_str != "error")
            {
                search_result_str = search_result_str.Replace("}", "");
                search_result_str = search_result_str.Replace("{", "");
                string[] results = search_result_str.Split(';');

                StackLayout LoadSearchRes = SearchResultStack;
                Frame FrameLoad = null;
                foreach (var username in results)
                {
                    FrameLoad = new Frame()
                    {
                        Content = new StackLayout()
                        {
                            Children =
                            {
                                new Label() { Text = $"{username}", HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions= LayoutOptions.CenterAndExpand, TextColor=Color.FromHex("#D8D8D8"), FontSize=20, FontFamily="Inter", CharacterSpacing=1},
                                new Label() { Text="Add", HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions= LayoutOptions.CenterAndExpand, TextColor=Color.FromHex("#3472ED"), FontSize=15, FontFamily="Inter", CharacterSpacing= 1}
                            },
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Center
                        },
                        BackgroundColor =Color.FromHex("#222528"),
                        CornerRadius = 10,
                        HeightRequest = 60,
                        Padding = new Thickness(20,0,20,0)
                        
                    };
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) => {
                        var add_friend_username = username;
                        Add_friend_by_name(add_friend_username);
                    };
                    FrameLoad.GestureRecognizers.Add(tapGestureRecognizer);
                    LoadSearchRes.Children.Add(FrameLoad);
                }
                SearchResultStack = LoadSearchRes;
            }
            else
            {
                SearchResultStack.Children.Clear();
            }

        }

        public void SearchPeople(object sender, EventArgs e)
        {
            var search_user = AddFriendEntry.Text;
            Search_user_by_name(search_user);
        }

        public void OpenUsernameEdit(object sender, EventArgs e)
        {
            if (username_label.IsVisible)
            {
                if (entryactive==null)
                {
                    edit_username_entry.IsVisible = true;
                    edit_username_entry.Text = username_label.Text;
                    edit_username_entry.Focus();
                    entryactive = edit_username_entry;
                    username_label.IsVisible = false;
                    action_username_button.Text = "Save";
                }
                else
                {
                    entryactive.Focus();
                }
            }
            else
            {
                Edit_username(edit_username_entry.Text);
            }
        }

        public void OpenSearcher(object sender, EventArgs e)
        {
            if (AddFriendBlock.IsVisible)
            {
                if (entryactive == null)
                {
                    entryactive = AddFriendEntry;
                    AddFriendBlock.IsVisible = false;
                    AddFriendEntryBlock.IsVisible = true;
                    AddFriendEntry.Focus();
                }
                else
                {
                    entryactive.Focus();
                }
            }
            else
            {
                entryactive = null;
                AddFriendEntry.Text = "";
                AddFriendEntry.Unfocus();
                SearchResultStack.Children.Clear();
                AddFriendBlock.IsVisible = true;
                AddFriendEntryBlock.IsVisible = false;
            }
        }


        public void countsymbols(object sender, EventArgs e)
        {
            var a = TextPostEntry.Text.Length.ToString() + "/250";
            counter.Text = a;
            symbcount.Text = a;
        }
        private async void logout(object sender, EventArgs e)

        {
            activate = false;
            await Task.Delay(1200);
            App.Current.Properties.Remove("usertoken");
            await Navigation.PushAsync(new PhoneLog(), false);


        }
        async void load_info(string token)
        {
            var username = await GetUsername(token);
            string [] static_info = username.Split(';');
            username_label.Text = static_info[0];
            invite_label.Text = static_info[1];
        }
        public homepage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            object name = "";
            if (App.Current.Properties.TryGetValue("usertoken", out name))
            {
                // выполняем действия, если в словаре есть ключ "name"
                string exist_token = App.Current.Properties["usertoken"].ToString();
                if (Connectivity.NetworkAccess == NetworkAccess.Internet) { 
                    InitializeComponent();
                    imagemenu.Source = ImageSource.FromResource("appslovo.menu.png");
                    closemenuimage.Source = ImageSource.FromResource("appslovo.images.right.png");
                    load_post(true);
                    load_info(exist_token);
                    Load_User_Friends("");
                    Load_User_Requests("");
                    Load_User_Friends_Posts("");
                }
                else
                {
                    DisplayAlert("Lost Internet Connection", "404", "ОK");
                }
            }
            else
            {
                swaptolog();
            }

            var displauheight = DeviceDisplay.MainDisplayInfo.Height;
            var displauwidth = DeviceDisplay.MainDisplayInfo.Width;
            

        }

        private void openmenu(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.07));
            menu.IsVisible = true;
            posts.IsVisible = false;
            button_open_menu.IsVisible = false;
            button_close_menu.IsVisible = true;
        }
        private void closemenu(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.07));
            button_open_menu.IsVisible = true;
            button_close_menu.IsVisible = false;
            menu.IsVisible = false;
            posts.IsVisible = true;
        }
        public bool alive = true;
        
        // таймер для поста
        public async void PostTimer(int seconds_count)
        {
            while (seconds_count > 0 && timer_disabled == false)
            {
                if (timer_disabled)
                {
                    seconds_count = 1;
                }

                else
                {
                    seconds_count -= 1;
                    counter_static.Text = TimeChecker(seconds_count);
                    
                }
                if (seconds_count < 1)
                {
                    load_post(false);
                    TextPostEntry.Text = "";
                }
                await Task.Delay(1000);
            }
        }

        // делаем из времени в секундах время в utc
        public static string TimeChecker(int countseconds)
        {
            string res = "";

            string seconds_separator = "";
            string minutes_separator = "";
            string hours_separator = "";
            int seconds = countseconds % 60;
            int minutes = ((countseconds - seconds) / 60) % 60;
            int hours = (((countseconds - seconds) / 60) - minutes) / 60;
            if (seconds < 10)
            {
                seconds_separator = "0";
            }
            if (minutes < 10)
            {
                minutes_separator = "0";
            }
            if (hours < 10)
            {
                hours_separator = "0";
            }
            res = hours_separator + hours + ":" + minutes_separator + minutes + ":" + seconds_separator + seconds;
            return res;
        }
    }

}

