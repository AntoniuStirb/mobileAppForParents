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
using LicentaUTCN.Utils;

namespace LicentaUTCN
{
    [Activity(Label = "ParentMenu")]
    public class ActivityParentMenu  :Activity
    {
        Button btnBackParentMenu;
        Button btnAddNewChild;
        Button btnAssignNewTest;
        Button btnParentPreviousResults;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ParentMenu);
            btnAddNewChild = FindViewById<Button>(Resource.Id.buttonParentAddChild);
            btnAddNewChild.Click += btnAddNewChild_Click;

            btnAssignNewTest = FindViewById<Button>(Resource.Id.buttonParentAssignTest);
            btnAssignNewTest.Click += btnAssignNewTest_Click;

            btnParentPreviousResults = FindViewById<Button>(Resource.Id.buttonParentPreviousResults);
            btnParentPreviousResults.Click += btnParentPreviousResults_Click;

            btnBackParentMenu = FindViewById<Button>(Resource.Id.buttonBackParentMenu);
            btnBackParentMenu.Click += btnBackParentMenu_Click;


        }

        private void btnAddNewChild_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityUserRegister2));
            StartActivity(nextActivity);
        }

        private void btnBackParentMenu_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityUserMenuVers2));
            StartActivity(nextActivity);
        }

        private void btnAssignNewTest_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityParentAssigntNewTest));
            StartActivity(nextActivity);
        }


        private void btnParentPreviousResults_Click(object sender, System.EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityPreviousResults));
            StartActivity(nextActivity);
        }
    }
}