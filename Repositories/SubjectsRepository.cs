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
using Firebase.Database;
using LicentaUTCN.Models;

namespace LicentaUTCN.Repositories
{
    public class SubjectsRepository
    {
        string webAPIKey = "AIzaSyAcChzONkAXtdaFdxrJL2CWG7TsytHGbMs";
        public static FirebaseClient firebaseClient = new FirebaseClient("https://licentadbfirebase-default-rtdb.firebaseio.com/");
        public SubjectsRepository()
        {

        }

        public async Task<SubjectModel> GetSubjectByName(string subjectName)
        {
            try
            {
                var subject = (await firebaseClient.Child("Subjects").OnceAsync<SubjectModel>()).Select(
                    item => new SubjectModel
                    {
                        SubjectName = item.Object.SubjectName,
                        SubjectId = item.Object.SubjectId
                    }).Where(e => e.SubjectName == subjectName).FirstOrDefault();
                return subject;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }
            return null;
        }

        public async Task<SubjectModel> GetSubjectById(int id) 
        {
            try
            {
                var subject = (await firebaseClient.Child("Subjects").OnceAsync<SubjectModel>()).Select(
                    item => new SubjectModel
                    {
                        SubjectName = item.Object.SubjectName,
                        SubjectId = item.Object.SubjectId
                    }).Where(e => e.SubjectId == id).FirstOrDefault();
                return subject;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }
            return null;
        }

    }
}