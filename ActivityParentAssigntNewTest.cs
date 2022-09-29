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
    [Activity(Label = "ActivityParentAssigntNewTest")]
    public class ActivityParentAssigntNewTest : Activity
    {
        Button btnBackAssignNewTest;
        RadioButton radioButtonMath;
        RadioButton radioButtonHistory;
        RadioButton radioButtonGeography;
        RadioButton radioButtonGrammar;
        RadioGroup radioGroup;
        Button nextButton;
        SubjectsRepository subjectsRepository;

        public ActivityParentAssigntNewTest()
        {
            subjectsRepository = new SubjectsRepository();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.ParentAssignNewTest);
            // Create your application here

            radioButtonMath = FindViewById<RadioButton>(Resource.Id.mathRadioButton);
            radioButtonHistory = FindViewById<RadioButton>(Resource.Id.historyRadioButton);
            radioButtonGeography = FindViewById<RadioButton>(Resource.Id.geographyRadioButton);
            radioButtonGrammar = FindViewById<RadioButton>(Resource.Id.grammarRadionButton);
            radioGroup = FindViewById<RadioGroup>(Resource.Id.radio_group);
            radioGroup.CheckedChange += myRadioGroup_CheckedChange;
            nextButton = FindViewById<Button>(Resource.Id.buttonNextAddNewTest);
            btnBackAssignNewTest = FindViewById<Button>(Resource.Id.buttonBackAssignNewTest);

            radioButtonMath.Click += RadioButtonClick;
            radioButtonHistory.Click += RadioButtonClick;
            radioButtonGeography.Click += RadioButtonClick;
            radioButtonGrammar.Click += RadioButtonClick;
            nextButton.Click += NextButton_Click;
            btnBackAssignNewTest.Click += btnBackAssignNewTest_Click;

            //radioGroup.AddView(radioButtonMath);
            //radioGroup.AddView(radioButtonHistory);
            //radioGroup.AddView(radioButtonGeography);
            //radioGroup.AddView(radioButtonGrammar);


        }

        private void RadioButtonClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            Toast.MakeText(this, rb.Text, ToastLength.Short).Show();
        }

        private void btnBackAssignNewTest_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityParentMenu));
            StartActivity(nextActivity);

        }

        void myRadioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            int checkedItemId = radioGroup.CheckedRadioButtonId;
            RadioButton checkedRadioButton = FindViewById<RadioButton>(checkedItemId);
            Toast.MakeText(this, Convert.ToString(checkedRadioButton.Text), ToastLength.Short).Show();
        }

        private async void NextButton_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityAddQuestion));
            int checkedItemId = radioGroup.CheckedRadioButtonId;
            RadioButton checkedRadioButton = FindViewById<RadioButton>(checkedItemId);
            var subject = await subjectsRepository.GetSubjectByName(checkedRadioButton.Text);
            if(subject != null)
            {
                Transporter.SubjectId = subject.SubjectId;
                StartActivity(nextActivity);
            }
            else
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Fetch error");
                dialog.SetMessage("Something went wrong. Try again");
                dialog.Show();
            }

            


        }
    }
}