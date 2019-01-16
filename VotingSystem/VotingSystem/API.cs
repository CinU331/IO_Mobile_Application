using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VotingSystem
{
    public class API
    {
        private static readonly string server = "http://85.89.190.151:3010";
        private static readonly string serverPng = "http://85.89.190.151:5905";
        private static HttpClient client = new HttpClient();

        public static async Task<bool> LoginAsync(User user)
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
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            String responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
            user.Token = values["token"];
            return true;
        }
        public static async Task<List<Ballot>> GetBallots()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(server + "/ballot"),
            };
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            String responseString = await response.Content.ReadAsStringAsync();
            List<Ballot> ballots = JsonConvert.DeserializeObject<List<Ballot>>(responseString);
            return ballots;
        }

        public static async Task<List<Candidate>> GetCandidateNamesForBallot(Ballot ballot)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(server + "/ballot/" + ballot.id),

            };
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            String responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Candidate>>(responseString);
        }

        public static async Task<bool> Vote(Ballot ballot, Candidate candidate, string token)
        {
            string content =
                "{"
                + "\"ballotId\": " + ballot.id + ","
                + "\"candidateId\": " + candidate.Id +
                "}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(server + "/vote"),
                Headers =
                {
                    {HttpRequestHeader.ContentType.ToString(),"application/json"},
                    {HttpRequestHeader.Authorization.ToString(),"Bearer " + token}
                },
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return false;
            }
            return true;
        }

        public static async Task<Image> GetResultsImage(Ballot ballot)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(serverPng + "/datavisualization/ballotGraph/" + ballot.id)
            };
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            byte[] responseBytes = await response.Content.ReadAsByteArrayAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            Image image = new Image
            {
                Source = ImageSource.FromStream(() => new MemoryStream(responseBytes))
            };
            return image;
        }
    }
}
