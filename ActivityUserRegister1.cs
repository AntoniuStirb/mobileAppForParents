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
using LicentaUTCN.Repositories;

namespace LicentaUTCN
{
    [Activity(Label = "UserRegister1")]


    public class ActivityUserRegister1 : Activity
    {
        Button btnNextRegister;
        Button btnSignUp;
        EditText firstName;
        EditText lastName;
        EditText email;
        EditText password;
        EditText confirmPassword;
        ParentUserRepository parentUserRepository;

        public ActivityUserRegister1(){
            parentUserRepository = new ParentUserRepository();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.UserRegister1);
            btnNextRegister = FindViewById<Button>(Resource.Id.buttonNextRegister);
            btnSignUp = FindViewById<Button>(Resource.Id.adminSignInBtnInRegister);

            #region Binding TextBoxes
            firstName = FindViewById<EditText>(Resource.Id.parentRegisterName);
            lastName = FindViewById<EditText>(Resource.Id.parentRegisterSourame);
            email = FindViewById<EditText>(Resource.Id.parentRegisterEmail);
            password = FindViewById<EditText>(Resource.Id.parentRegisterPassword1);
            confirmPassword = FindViewById<EditText>(Resource.Id.parentRegisterPassword2);

            #endregion

            btnNextRegister.Click += BtnNextRegister_Click;
            btnSignUp.Click += btnSignUp_Click;
        }
        private async void BtnNextRegister_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityParentLog));
            System.Diagnostics.Debug.WriteLine(firstName.Text);
            System.Diagnostics.Debug.WriteLine(lastName.Text);
            System.Diagnostics.Debug.WriteLine(email.Text);
            System.Diagnostics.Debug.WriteLine(password.Text);
            System.Diagnostics.Debug.WriteLine(confirmPassword.Text);
            if (password.Text.Equals(confirmPassword.Text))
            {
                await parentUserRepository.Register(email.Text, firstName.Text, lastName.Text, password.Text);
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


        private async void btnSignUp_Click(object sender, System.EventArgs e)
        {

            Intent nextActivity = new Intent(this, typeof(ActivityParentLog));
            StartActivity(nextActivity);

        }
    }
}