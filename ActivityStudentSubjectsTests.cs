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
using LicentaUTCN.Repositories;
using LicentaUTCN.Utils;

namespace LicentaUTCN
{
    [Activity(Label = "StudentSubjectsTests")]
    public class ActivityStudentSubjectsTests: Activity
    {
        Button btnMathAvailableTests;
        Button btnHistoryAvailableTests;
        Button btnGeographyAvailableTests;
        Button btnGrammarAvailableTests;
        Button btnBackStudentSubjNtests;
        SubjectsRepository subjectRepository;

        public ActivityStudentSubjectsTests()
        {
            subjectRepository = new SubjectsRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StudentSubjectsTests);

            btnMathAvailableTests = FindViewById<Button>(Resource.Id.buttonAvailableMathematics);
            btnHistoryAvailableTests = FindViewById<Button>(Resource.Id.buttonAvailableHistory);
            btnGeographyAvailableTests = FindViewById<Button>(Resource.Id.buttonAvailableGeography);
            btnGrammarAvailableTests = FindViewById<Button>(Resource.Id.buttonAvailableGrammar);
            btnBackStudentSubjNtests = FindViewById<Button>(Resource.Id.buttonBackStudentSubjNTests);

            btnMathAvailableTests.Click += btnMathAvailableTests_Click;
            btnHistoryAvailableTests.Click += btnHistoryAvailableTests_Click;
            btnGeographyAvailableTests.Click += btnGeographyAvailableTests_Click;
            btnGrammarAvailableTests.Click += btnGrammarAvailableTests_Click;
            btnBackStudentSubjNtests.Click += btnBackStudentSubjNtests_Click;
        }
        private async void btnMathAvailableTests_Click(object sender, System.EventArgs e)
        {
            var resp = await subjectRepository.GetSubjectByName(btnMathAvailableTests.Text);
            Transporter.SubjectId = resp.SubjectId;
            Intent nextActivity = new Intent(this, typeof(ActivityAvailableTests));
            StartActivity(nextActivity);

        }

        private async void btnHistoryAvailableTests_Click(object sender, System.EventArgs e)
        {
            var resp = await subjectRepository.GetSubjectByName(btnHistoryAvailableTests.Text);
            Transporter.SubjectId = resp.SubjectId;
            Intent nextActivity = new Intent(this, typeof(ActivityAvailableTests));
            StartActivity(nextActivity);

        }

        private async void btnGeographyAvailableTests_Click(object sender, System.EventArgs e)
        {
            var resp = await subjectRepository.GetSubjectByName(btnGeographyAvailableTests.Text);
            Transporter.SubjectId = resp.SubjectId;
            Intent nextActivity = new Intent(this, typeof(ActivityAvailableTests));
            StartActivity(nextActivity);

        }

        private void btnBackStudentSubjNtests_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityStudentMenu));
            StartActivity(nextActivity);

        }
        private async void btnGrammarAvailableTests_Click(object sender, System.EventArgs e)
        {
            var resp = await subjectRepository.GetSubjectByName(btnGrammarAvailableTests.Text);
            Transporter.SubjectId = resp.SubjectId;
            Intent nextActivity = new Intent(this, typeof(ActivityAvailableTests));
            StartActivity(nextActivity);

        }
    }
}