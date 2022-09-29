using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [Activity(Label = "PreviousResults")]
    public class ActivityPreviousResults : Activity
    {
        public TestResultsRepository testResultsRepository;
        public StudentUserRepository studentUserRepository;
        ListView testsList;
        Button btnBack;
        TextView studentNameView;
        public ActivityPreviousResults()
        {
            testResultsRepository = new TestResultsRepository();
            studentUserRepository = new StudentUserRepository();
        }
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.PreviousResults);
            testsList = (ListView)FindViewById<ListView>(Resource.Id.resultsListView);
            studentNameView = FindViewById<TextView>(Resource.Id.studentNameView);
            btnBack = FindViewById<Button>(Resource.Id.buttonBackTestResults);
            btnBack.Click += btnBack_Click;

            var results = await GetTestResults();
            int cnt = 0;
            var resultsViews = new List<string>();
            foreach(var result in results)
            {
                cnt++;
                string resultView = $"Test {cnt} {result.SubjectName}: {result.NoOfCorrectResponses}/{result.NoOfQuestions}";
                resultsViews.Add(resultView);
            }

            testsList.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, resultsViews);

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
           if (Transporter.IsParentLoggedIn== true)
            {
                Intent nextActivity = new Intent(this, typeof(ActivityParentMenu));
                StartActivity(nextActivity);
            }
           else
            {
                Intent nextActivity = new Intent(this, typeof(ActivityStudentMenu));
                StartActivity(nextActivity);
            }
        }

        private async Task<List<TestResultDTO>> GetTestResults()
        {
            
            var studentId = Transporter.IsParentLoggedIn ? await studentUserRepository.GetStudentByParentId(Transporter.LogedInParentId) : Transporter.LogedInStudentId;
            var studentName = await studentUserRepository.GetSudentNameById(studentId);
            studentNameView.Text = studentName;
            var testResults = await testResultsRepository.GetTestResultsForChild(studentId);
            return testResults;
        }
    }
}