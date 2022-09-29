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
    [Activity(Label = "ParentLog")]
    public class ActivityParentLog : Activity
    {
        Button btnParentRegister;
        Button btnBackParentLogin;
        Button btnParentLogIn;
        EditText email;
        EditText password;
        ParentUserRepository parentUserRepository;

        public ActivityParentLog()
        {
            parentUserRepository = new ParentUserRepository();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ParentLog);
            btnParentRegister = FindViewById<Button>(Resource.Id.registerUserBtn);
            btnParentRegister.Click += BtnParentRegister_Click;

            btnBackParentLogin = FindViewById<Button>(Resource.Id.parentBackBtn);
            btnBackParentLogin.Click += btnBackParentLogin_Click;

            btnParentLogIn = FindViewById<Button>(Resource.Id.loginParentBtn);
            btnParentLogIn.Click += async (sender, e) =>
            {
                var resp = await parentUserRepository.Login(email.Text, password.Text);
                if (resp != null)
                {
                    Transporter.LogedInParentId = resp.Id;
                    Transporter.IsParentLoggedIn = true;
                    Intent nextActivity = new Intent(this, typeof(ActivityParentMenu));
                    StartActivity(nextActivity);
                }
                else
                {
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Login error");
                    dialog.SetMessage("Wrong email or password. Try again");
                    dialog.Show();
                }
            };

            email = FindViewById<EditText>(Resource.Id.parentEmail);
            password = FindViewById<EditText>(Resource.Id.parentPassword);

        }

        private void BtnParentRegister_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityUserRegister1));
            StartActivity(nextActivity);
        }

        private void btnBackParentLogin_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityUserMenuVers2));
            StartActivity(nextActivity);
        }

        private void btnParentLogIn_ClickAsync(object sender, System.EventArgs e)
        {
            
            
        }
    }
}