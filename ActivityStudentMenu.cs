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

namespace LicentaUTCN
{
    [Activity(Label = "StudentMenu")]
    public class ActivityStudentMenu : Activity
    {
        Button BtnStudentPrevResults;
        Button BtnStudentSubjectsTests;
        Button BtnStudentMenuBack;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StudentMenu);
            BtnStudentPrevResults = FindViewById<Button>(Resource.Id.buttonStudentPreviousResults);
            BtnStudentPrevResults.Click += BtnStudentPrevResults_Click;

            BtnStudentSubjectsTests = FindViewById<Button>(Resource.Id.buttonStudentSubjectsTests);
            BtnStudentSubjectsTests.Click += BtnStudentSubjectsTests_Click;

            BtnStudentMenuBack = FindViewById<Button>(Resource.Id.buttonBackStudentMenu);
            BtnStudentMenuBack.Click += BtnStudentMenuBack_Click;
        }
        private void BtnStudentPrevResults_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityPreviousResults));
            StartActivity(nextActivity);
        }

        private void BtnStudentMenuBack_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityUserMenuVers2));
            StartActivity(nextActivity);
        }

        private void BtnStudentSubjectsTests_Click(object sender, System.EventArgs e)
        {
           Intent nextActivity = new Intent(this, typeof(ActivityStudentSubjectsTests));
            StartActivity(nextActivity);
        }
    }
}