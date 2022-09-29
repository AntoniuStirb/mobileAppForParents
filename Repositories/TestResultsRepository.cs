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
    public class TestResultsRepository
    {
        string webAPIKey = "AIzaSyAcChzONkAXtdaFdxrJL2CWG7TsytHGbMs";
        public static FirebaseClient firebaseClient = new FirebaseClient("https://licentadbfirebase-default-rtdb.firebaseio.com/");
        public QuestionRepository questionRepository;
        public SubjectsRepository subjectRepository;
        public TestRepostory testRepostory;

        public TestResultsRepository()
        {
            questionRepository = new QuestionRepository();
            subjectRepository = new SubjectsRepository();
            testRepostory = new TestRepostory();
        }

        public async Task<bool> AddTestResult(string studentId, string testId, int noOfCorrectResponses)
        {
            try
            {
                await firebaseClient
                .Child("TestResults")
                .PostAsync(new TestResultModel { Id = Guid.NewGuid().ToString(), StudentId = studentId, 
                    NoOfCorrectResponses = noOfCorrectResponses, TestId = testId});
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public async Task<List<TestResultDTO>> GetTestResultsForChild(string studentId)
        {
            var testsResults = (await firebaseClient.Child("TestResults").OnceAsync<TestResultModel>()).Select(
                   item => new TestResultModel
                   {
                      Id = item.Object.Id,
                      NoOfCorrectResponses = item.Object.NoOfCorrectResponses,
                      StudentId = item.Object.StudentId, 
                      TestId = item.Object.TestId
                   }).Where(e => e.StudentId == studentId).ToList();

            var completeTestResults = new List<TestResultDTO>();
            foreach(var item in testsResults)
            {
                var noOfQuestions = await questionRepository.GetNumberOfQuestions(item.TestId);
                var test = (await firebaseClient.Child("Tests").OnceAsync<TestModel>()).Select(
                    item => new TestModel
                    {
                        StudentId = item.Object.StudentId,
                        SubjectId = item.Object.SubjectId,
                        Id = item.Object.Id,
                        Completed = item.Object.Completed
                    }).Where(e => e.Id == item.TestId).FirstOrDefault();
                var subject = await subjectRepository.GetSubjectById(test.SubjectId);
                completeTestResults.Add(new TestResultDTO
                {
                    Id = item.Id,
                    NoOfCorrectResponses = item.NoOfCorrectResponses,
                    StudentId = item.StudentId,
                    TestId = item.TestId,
                    NoOfQuestions = noOfQuestions,
                    SubjectName = subject.SubjectName

                });
                
            }

            return completeTestResults;
        }


    }
}