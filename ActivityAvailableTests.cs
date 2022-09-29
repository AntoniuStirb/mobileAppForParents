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
using LicentaUTCN.DTO;
using LicentaUTCN.Repositories;
using LicentaUTCN.Utils;

namespace LicentaUTCN
{
    [Activity(Label = "MathAvailableTests")]
    public class ActivityAvailableTests : Activity
    {
        Button btnBackAvailableTests;
        ListView testsList;
        TestRepostory testRepository;
        List<TestQuestionDTO> testQuestions;
        List<string> tests = new List<string>();

        public ActivityAvailableTests()
        {
            testRepository = new TestRepostory();
        }
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.AvailableTests);

            btnBackAvailableTests = FindViewById<Button>(Resource.Id.buttonBackAvailableTests);
            btnBackAvailableTests.Click += btnBackAvailableTests_Click;

            #region FETCH_DATA
            var resp = await testRepository.GetTestBySubjectId(Transporter.SubjectId, Transporter.LogedInStudentId);
            int cnt = 0;
            foreach(var test in resp)
            {
                tests.Add("Test " + (++cnt));
            }
            testsList = (ListView)FindViewById<ListView>(Resource.Id.testsListView);

            testsList.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, tests);

            testsList.ItemClick += (s, e) => {
                Transporter.TestQuestions = resp.ElementAt(e.Position).Questions;
                var t = tests.ElementAt(e.Position);
                Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Long).Show();
                Intent nextActivity = new Intent(this, typeof(ActivityTakeTest));
                StartActivity(nextActivity);
            };


            #endregion



        }

        private void btnBackAvailableTests_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityStudentSubjectsTests));
            StartActivity(nextActivity);

        }

    }
}