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
using Firebase.Database.Query;
using LicentaUTCN.Models;

namespace LicentaUTCN.Repositories
{
    public class QuestionRepository
    {
        string webAPIKey = "AIzaSyAcChzONkAXtdaFdxrJL2CWG7TsytHGbMs";
        public static FirebaseClient firebaseClient = new FirebaseClient("https://licentadbfirebase-default-rtdb.firebaseio.com/");
        public QuestionRepository()
        {

        }

        public async Task<bool> AddQuestion(QuestionModel question)
        {
            try
            {
                await firebaseClient
                .Child("Questions")
                .PostAsync(new QuestionModel { 
                    Id =  Guid.NewGuid().ToString(),
                    TestId = question.TestId,
                    Question = question.Question,
                    FirstAnswear = question.FirstAnswear,
                    SecondAnswear = question.SecondAnswear,
                    ThirdAnswear = question.ThirdAnswear,
                    ForthAnswear = question.ForthAnswear,
                    CorrectAnswear = question.CorrectAnswear

                });
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public async Task<int> GetNumberOfQuestions(string testId)
        {
            var noOfQuestions = (await firebaseClient.Child("Questions").OnceAsync<QuestionModel>()).Select(
                        item => new QuestionModel
                        {
                            CorrectAnswear = item.Object.CorrectAnswear,
                            FirstAnswear = item.Object.FirstAnswear,
                            SecondAnswear = item.Object.SecondAnswear,
                            ThirdAnswear = item.Object.ThirdAnswear,
                            ForthAnswear = item.Object.ForthAnswear,
                            Question = item.Object.Question,
                            Id = item.Object.Id,
                            TestId = item.Object.TestId
                        }).Where(e => e.TestId == testId).Count();
            return noOfQuestions;
            
        }

    }
}