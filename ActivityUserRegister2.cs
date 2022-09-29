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

namespace LicentaUTCN
{
    [Activity(Label = "UserRegister2")]
    public class ActivityUserRegister2 : Activity
    {
        Button btnBackUserReg2;
        Button btnStudentRegister;
        EditText firstName;
        EditText lastName;
        EditText email;
        EditText password;
        EditText confirmPassword;

        StudentUserRepository studentUserRepository;

        public ActivityUserRegister2()
        {
            studentUserRepository = new StudentUserRepository();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.UserRegister2);
            btnStudentRegister = FindViewById<Button>(Resource.Id.buttonStudentRegister);
            btnStudentRegister.Click += btnStudentRegister_Click;

            btnBackUserReg2 = FindViewById<Button>(Resource.Id.buttonBackUserRegister2);
            btnBackUserReg2.Click += btnBackUserReg2_Click;

            #region Binding TextBoxes
            firstName = FindViewById<EditText>(Resource.Id.studentRegisterName);
            lastName = FindViewById<EditText>(Resource.Id.studentRegisterSourame);
            email = FindViewById<EditText>(Resource.Id.studentRegisterEmail);
            password = FindViewById<EditText>(Resource.Id.studentRegisterPassword1);
            confirmPassword = FindViewById<EditText>(Resource.Id.studentRegisterPassword2);

            #endregion
        }

        private async void btnStudentRegister_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityParentMenu));

            System.Diagnostics.Debug.WriteLine(firstName.Text);
            System.Diagnostics.Debug.WriteLine(lastName.Text);
            System.Diagnostics.Debug.WriteLine(email.Text);
            System.Diagnostics.Debug.WriteLine(password.Text);
            System.Diagnostics.Debug.WriteLine(confirmPassword.Text);

            if (password.Text.Equals(confirmPassword.Text))
            {
                await studentUserRepository.Register(email.Text, firstName.Text, lastName.Text, password.Text);
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("------------------");
                dialog.SetMessage("Succesfully registered!");
                dialog.Show();

                StartActivity(nextActivity);
            }

            else
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Register error");
                dialog.SetMessage("The passwords don't match. Please try again");
                dialog.Show();
            }

        }
        private async void btnBackUserReg2_Click(object sender, System.EventArgs e)
        {

            Intent nextActivity = new Intent(this, typeof(ActivityParentMenu));
            StartActivity(nextActivity);

        }
    }
}