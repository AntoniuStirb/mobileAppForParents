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
    [Activity(Label = "Take Test")]

    public class ActivityTakeTest : Activity
    {
        TextView question;
        TextView firstAnswear;
        TextView secondAnswear;
        TextView thirdAnswear;
        TextView forthAnswear;
        EditText correctAnswear;
        Button nextButton;
        TestResultsRepository testResultsRepository;
        TestRepostory testRepository;
        List<int> responses = new List<int>();

        int cnt = 0;
        public ActivityTakeTest()
        {
            testResultsRepository = new TestResultsRepository();
            testRepository = new TestRepostory();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.TakeTest);

            #region BindTextBoxes
            nextButton = FindViewById<Button>(Resource.Id.nextQuestionTakeTest);
            question = FindViewById<TextView>(Resource.Id.questionTakeTest);
            firstAnswear = FindViewById<TextView>(Resource.Id.Answer1TakeTest);
            secondAnswear = FindViewById<TextView>(Resource.Id.Answer2TakeTest);
            thirdAnswear = FindViewById<TextView>(Resource.Id.Answer3TakeTest);
            forthAnswear = FindViewById<TextView>(Resource.Id.Answer4TakeTest);
            correctAnswear = FindViewById<EditText>(Resource.Id.correctAnswerTakeTest);
            #endregion
            nextButton.Click += NextButton_Click;

            #region BindQuestions
            BindQuestions();
            #endregion

        }

        private void BindQuestions()
        {
            question.Text = Transporter.TestQuestions.ElementAt(cnt).Question;
            firstAnswear.Text = Transporter.TestQuestions.ElementAt(cnt).FirstAnswear;
            secondAnswear.Text = Transporter.TestQuestions.ElementAt(cnt).SecondAnswear;
            thirdAnswear.Text = Transporter.TestQuestions.ElementAt(cnt).ThirdAnswear;
            forthAnswear.Text = Transporter.TestQuestions.ElementAt(cnt).ForthAnswear;
            correctAnswear.Text = "";
        }
        private async void NextButton_Click(object sender, System.EventArgs e)
        {
            if(correctAnswear.Text == null || correctAnswear.Text == "")
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Error");
                dialog.SetMessage("Please complete your answear");
                dialog.Show();
                
            } 
            else
            {
                if (cnt < Transporter.TestQuestions.Count)
                {
                    responses.Add(Int32.Parse(correctAnswear.Text));
                    cnt++;
                    if(cnt < Transporter.TestQuestions.Count)
                    {
                        BindQuestions();
                    }
                    
                    
                }
                if(cnt == Transporter.TestQuestions.Count)
                {
                    int correctResponses = 0;
                    for(int i = 0; i < responses.Count; ++i)
                    {
                        if (responses[i] == Transporter.TestQuestions[i].CorrectAnswear)
                        {
                            correctResponses++;
                        }
                    }
                    await testResultsRepository.AddTestResult(Transporter.LogedInStudentId, Transporter.TestQuestions[0].TestId, correctResponses);
                    await testRepository.CompleteTest(Transporter.TestQuestions[0].TestId);
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("DONE !");
                    dialog.SetMessage($"Correct responses: {correctResponses} / {responses.Count}");
                    dialog.Show();

                    Intent nextActivity = new Intent(this, typeof(ActivityStudentSubjectsTests));
                    StartActivity(nextActivity);
                }
            }
           
        }
    }
}