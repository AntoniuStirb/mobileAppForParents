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
using LicentaUTCN.Models;
using LicentaUTCN.Repositories;
using LicentaUTCN.Utils;

namespace LicentaUTCN
{
    [Activity(Label = "ActivityAddQuestion")]
    class ActivityAddQuestion : Activity
    {
        TextView QuestionNumber;
        Button NextQuestionButton;

        TestRepostory testRepository;
        StudentUserRepository studentUserRepository;
        QuestionRepository questionRepository;


        TextView question;
        TextView firstAnswear;
        TextView secondAnswear;
        TextView thirdAnswear;
        TextView forthAnswear;
        TextView correctAnswear;

        private int questionNumber = 0;
        private string _testId;
        private readonly int _noOfQuestions = 3;

        public ActivityAddQuestion()
        {
            testRepository = new TestRepostory();
            studentUserRepository = new StudentUserRepository();
            questionRepository = new QuestionRepository();
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.AddNewQuestion);
            QuestionNumber = FindViewById<TextView>(Resource.Id.questionNumber);
            QuestionNumber.Text = "Question no 1";

            NextQuestionButton = FindViewById<Button>(Resource.Id.nextQuestion);
            NextQuestionButton.Click += NextButton_Click;

            #region BindTextBoxes
            question = FindViewById<EditText>(Resource.Id.question);
            firstAnswear = FindViewById<EditText>(Resource.Id.Answer1);
            secondAnswear = FindViewById<EditText>(Resource.Id.Answer2);
            thirdAnswear = FindViewById<EditText>(Resource.Id.Answer3);
            forthAnswear = FindViewById<EditText>(Resource.Id.Answer4);
            correctAnswear = FindViewById<EditText>(Resource.Id.correctAnswer);
            #endregion
        }
        private async void NextButton_Click(object sender, System.EventArgs e)
        {

            if(questionNumber == 0)
            {
                var studentId = await studentUserRepository.GetStudentByParentId(Transporter.LogedInParentId);
                var idTest = await testRepository.AddTest(Transporter.SubjectId, studentId);
                _testId = idTest;
            }
            if(questionNumber <= _noOfQuestions)
            {
                questionNumber++;
                QuestionNumber.Text = "Question no " + (questionNumber + 1).ToString();

                var questionModel = new QuestionModel
                {
                    Id = Guid.NewGuid().ToString(),
                    TestId = _testId,
                    Question = question.Text,
                    FirstAnswear = firstAnswear.Text,
                    SecondAnswear = secondAnswear.Text,
                    ThirdAnswear = thirdAnswear.Text,
                    ForthAnswear = forthAnswear.Text,
                    CorrectAnswear = Int32.Parse(correctAnswear.Text)
                };

                await questionRepository.AddQuestion(questionModel);
                ClearTextBoxes();

            }
            if(questionNumber == _noOfQuestions)
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Successfully added");
                dialog.SetMessage("Ai adaugat intrebari");
                dialog.Show();
                Intent nextActivity = new Intent(this, typeof(ActivityParentMenu));
                StartActivity(nextActivity);

            }
            else
            {
                // duma la alta pagina
            }
            
        }
        private void ClearTextBoxes()
        {
            question.Text = "";
            firstAnswear.Text = "";
            secondAnswear.Text = "";
            thirdAnswear.Text = "";
            forthAnswear.Text = "";
            correctAnswear.Text = "";
        }
    }
}