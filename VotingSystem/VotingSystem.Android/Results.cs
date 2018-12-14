using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace VotingSystem.Droid
{
    [Activity(Label = "Results")]
    public class Results : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
        public void GetAttendance(string sciezka)
        {

        }

        public void GetDistributionOfVotes(string sciezka)
        {

        }

        public void DiplaysResults()
        {

        }
    }
}