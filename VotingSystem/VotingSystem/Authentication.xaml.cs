using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VotingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Authentication : ContentPage
    {
        private string login;
        private string password;
        private string token;
        private List<User> users;

        public Authentication()
        {
            InitializeComponent();
            users = User.GetUsers();
        }

        protected override void OnAppearing()
        {
            login = null;
            loginEntry.Text = null;
            password = null;
            passwordEntry.Text = null;
        }

        private void Login_Entered(object sender, EventArgs e)
        {
            login = ((Entry)sender).Text;
        }
        private void Password_Entered(object sender, EventArgs e)
        {
            password = ((Entry)sender).Text;
        }
        private void LogIn_Clicked(object sender, EventArgs args)
        {
            if (login != null && password != null)
            {
                if (TryToLogIn(login, password))
                {
                    GetToken(login);
                    GoToVoting();
                }
                else
                    DisplayAlert("", "Nieprawidłowe dane.", "OK");
            }
            else
            {
                DisplayAlert("", "Wprowadź login oraz hasło.", "OK");
            }
        }

        private void GoToResults_Clicked(object sender, EventArgs e)
        {
            GoToResults();
        }
        public bool TryToLogIn(string login, string password)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Login == login && users[i].Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public void GetToken(string login)
        {
            int i = users.FindIndex(item => item.Login == login);
            token = users[i].Token;
        }

        public async void GoToVoting()
        {
            await Navigation.PushAsync(new Vote(token));
        }

        public async void GoToResults()
        {
            await Navigation.PushAsync(new Results());
        }


        public async void Post_Get()
        {
            /*        private static readonly string server = "server";
        private static readonly HttpClient client = new HttpClient();
        public static async System.Threading.Tasks.Task<bool> LoginAsync(User user)
        {
            string content =
                "{"
                + "\"login\": " + "\"" + user.Login + "\","
                + "\"password\": " + "\"" + user.Password + "\"" +
                "}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(server + "/login"),
                Headers =
                {
                    {HttpRequestHeader.ContentType.ToString(),"application/json"}
                },
                Content = new StringContent(content)
            };
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            String responseString = await response.Content.ReadAsStringAsync();
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
            if (values["token"].Length != 0)
            {
                user.Token = values["token"];
                return true;
            }
            return false;
        }*/

            using (var client = new HttpClient())
            {
                string content = "login=1&password=admin";
                StringContent httpContent = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");

                client.BaseAddress = new Uri("http://localhost:8545/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync("application/login", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("", "Zalogowano się. Token = " + responseContent + ".", "OK");
                }
                else
                {
                    await DisplayAlert("", "Nie udało się zalogować.", "OK");
                }
            }
        }
    }
}