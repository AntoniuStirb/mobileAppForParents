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
using LicentaUTCN.DTO;
using LicentaUTCN.Models;

namespace LicentaUTCN.Repositories
{
    public class TestRepostory
    {
        string webAPIKey = "AIzaSyAcChzONkAXtdaFdxrJL2CWG7TsytHGbMs";
        public static FirebaseClient firebaseClient = new FirebaseClient("https://licentadbfirebase-default-rtdb.firebaseio.com/");
        QuestionRepository questionRepository;
        public TestRepostory()
        {
            questionRepository = new QuestionRepository();
        }


        public async Task<bool> CompleteTest(string testId)
        {
            try
            {
                var toUpdate = (await firebaseClient.Child("Tests").OnceAsync<TestModel>()).Where(e => e.Object.Id == testId).FirstOrDefault();
                toUpdate.Object.Completed = true;
                
                await firebaseClient.Child("Tests").Child(toUpdate.Key).PutAsync(new TestModel
                {
                    Id = toUpdate.Object.Id,
                    SubjectId = toUpdate.Object.SubjectId,
                    StudentId = toUpdate.Object.StudentId,
                    Completed = toUpdate.Object.Completed
                });
                return true;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error:{e}");
                return false;
            }

        }
        public async Task<string> AddTest(int subjectId, string studentId)
        {
            try
            {
                string id = Guid.NewGuid().ToString();
                await firebaseClient
                .Child("Tests")
                .PostAsync(new TestModel { Id = id, StudentId = studentId, SubjectId = subjectId, Completed = false });
                return id;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error:{e}");
                return null;
            }
        }



        public async Task<List<TestQuestionDTO>> GetTestBySubjectId(int subjectId, string studentId)
        {
            try
            {
                var tests = (await firebaseClient.Child("Tests").OnceAsync<TestModel>()).Select(
                    item => new TestModel
                    {
                        StudentId = item.Object.StudentId,
                        SubjectId = item.Object.SubjectId,
                        Id = item.Object.Id, 
                        Completed = item.Object.Completed
                    }).Where(e => e.SubjectId == subjectId && e.StudentId == studentId && e.Completed == false).ToList();
                List<TestQuestionDTO> testQuestions = new List<TestQuestionDTO>();
                foreach(var test in tests)
                {
                    var questions = (await firebaseClient.Child("Questions").OnceAsync<QuestionModel>()).Select(
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
                        }).Where(e => e.TestId == test.Id);
                    if(questions != null)
                    {
                        TestQuestionDTO aux = new TestQuestionDTO();
                        aux.Questions = new List<QuestionModel>();
                        foreach(var x in questions)
                        {
                            aux.Questions.Add(x);
                        }
                        aux.Test = test;
                        testQuestions.Add(aux);

                    }
                }
                return testQuestions;
                
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }
            return null;
        }

    }
}