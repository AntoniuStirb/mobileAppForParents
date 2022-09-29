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
    [Activity(Label = "StudentLog")]
    public class ActivityStudentLog : Activity
    {

        Button btnStudentLogIn;
        Button btnStudentBack;
        EditText email;
        EditText password;

        StudentUserRepository studentUserRepository;
        public ActivityStudentLog()
        {
            studentUserRepository = new StudentUserRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StudentLog);
            btnStudentLogIn = FindViewById<Button>(Resource.Id.loginStudentBtn);
            btnStudentLogIn.Click += btnStudentLogIn_Click;

            btnStudentBack = FindViewById<Button>(Resource.Id.StudentBackBtn);
            btnStudentBack.Click += btnStudentBack_Click;

            email = FindViewById<EditText>(Resource.Id.studentEmail);
            password = FindViewById<EditText>(Resource.Id.studentPassword);

        }

        private async void btnStudentLogIn_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityStudentMenu));


            var resp = await studentUserRepository.Login(email.Text, password.Text);
            if (resp != null)
            {
                Transporter.LogedInStudentId = resp.Id;
                Transporter.IsParentLoggedIn = false;
                StartActivity(nextActivity);
            }
            else
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Login error");
                dialog.SetMessage("Wrong email or password. Try again");
                dialog.Show();
            }

        }


        private async void btnStudentBack_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityUserMenuVers2));
            StartActivity(nextActivity);
        }

    }
}