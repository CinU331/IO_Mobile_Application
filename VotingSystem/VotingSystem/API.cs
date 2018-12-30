using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace VotingSystem
{
    public class API
    {
        private static readonly string server = "https://204412ff-e052-4d36-aae4-4f3488ac1cc1.mock.pstmn.io";
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
        }
        public static async Task<List<Ballot>> GetBallots(string token)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(server + "/ballot"),
                Headers =
                {
                    {HttpRequestHeader.Authorization.ToString(),"Bearer " + token}
                }
            };
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            String responseString = await response.Content.ReadAsStringAsync();
            List<Ballot> ballots = JsonConvert.DeserializeObject<List<Ballot>>(responseString);
            return ballots;
        }

        public static async Task<List<Candidate>> GetCandidateNamesForBallot(Ballot ballot, string token)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(server + "/ballot/" + ballot.id),
                Headers =
                {
                    {HttpRequestHeader.Authorization.ToString(),"Bearer " + token}
                }
            };
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            String responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Candidate>>(responseString);
        }

        public static async Task Vote(Ballot ballot, Candidate candidate, string token)
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
                Content = new StringContent(content)
            };
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
