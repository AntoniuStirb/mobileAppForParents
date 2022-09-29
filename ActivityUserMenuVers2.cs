
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;

namespace LicentaUTCN
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ActivityUserMenuVers2 : AppCompatActivity
    {
        Button btnStudent;
        Button btnParent;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.UserMenuVers2);
            btnStudent = FindViewById<Button>(Resource.Id.studentBtn);
            btnStudent.Click += BtnStudent_Click;

            btnParent = FindViewById<Button>(Resource.Id.parentBtn);
            btnParent.Click += BtnParent_Click;

        }



        private void BtnStudent_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityStudentLog));
            StartActivity(nextActivity);

        }

        private void BtnParent_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity2 = new Intent(this, typeof(ActivityParentLog));
            StartActivity(nextActivity2);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}