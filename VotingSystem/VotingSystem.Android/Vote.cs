using System.Collections.Generic;
using Android.App;
using Android.OS;

namespace VotingSystem.Droid
{
    [Activity(Label = "Vote")]
    public class Vote : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        // Create your application here
        private List<string> candidates;
        private Authentication authentication;

        public List<string> GetCandidatesList()
        {
            return candidates;
        }

        public bool CheckIfVoiceHasAlreadyBeenGiven(string user)
        {
            return false;
        }

        public bool SaveVote(string candidat, string user)
        {
            return true;
        }

        public void DisplayMessage()
        {

        }

        public void GoToResults()
        {

        }
    }
}