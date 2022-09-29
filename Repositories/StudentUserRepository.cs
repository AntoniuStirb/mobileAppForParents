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
using LicentaUTCN.Utils;

namespace LicentaUTCN.Repositories
{
    public class StudentUserRepository
    {
        string webAPIKey = "AIzaSyAcChzONkAXtdaFdxrJL2CWG7TsytHGbMs";
        public static FirebaseClient firebaseClient = new FirebaseClient("https://licentadbfirebase-default-rtdb.firebaseio.com/");
        public StudentUserRepository()
        {

        }

        public async Task<bool> Register(string email, string firstName, string lastName, string password)
        {
            try
            {
                await firebaseClient
                .Child("StudentUsers")
                .PostAsync(new StudentUserModel { Id = Guid.NewGuid().ToString(), Email = email, Password = password, FirstName = firstName, LastName = lastName, ParentId = Transporter.LogedInParentId });
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public async Task<StudentUserModel> Login(string email, string password)
        {
            try
            {
                var user = (await firebaseClient.Child("StudentUsers").OnceAsync<StudentUserModel>()).Select(
                    item => new StudentUserModel
                    {
                        Email = item.Object.Email,
                        Password = item.Object.Password,
                        Id = item.Object.Id,
                        FirstName = item.Object.FirstName,
                        LastName = item.Object.LastName,
                        ParentId = item.Object.ParentId
                    }).Where(e => e.Email == email && e.Password == password).FirstOrDefault();
                return user;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }
            return null;
        }
        public async Task<string> GetSudentNameById(string studentId)
        {
            try
            {
                var user = (await firebaseClient.Child("StudentUsers").OnceAsync<StudentUserModel>()).Select(
                    item => new StudentUserModel
                    {
                        Email = item.Object.Email,
                        Password = item.Object.Password,
                        Id = item.Object.Id,
                        FirstName = item.Object.FirstName,
                        LastName = item.Object.LastName,
                        ParentId = item.Object.ParentId
                    }).Where(e => e.Id.Equals(studentId)).FirstOrDefault();
                return user.FirstName + " " + user.LastName;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }
            return null;
        }
        public async Task<string> GetStudentByParentId(string parentId)
        {
            try
            {
                var user = (await firebaseClient.Child("StudentUsers").OnceAsync<StudentUserModel>()).Select(
                    item => new StudentUserModel
                    {
                        Email = item.Object.Email,
                        Password = item.Object.Password,
                        Id = item.Object.Id,
                        FirstName = item.Object.FirstName,
                        LastName = item.Object.LastName,
                        ParentId = item.Object.ParentId
                    }).Where(e => e.ParentId.Equals(parentId)).FirstOrDefault();
                return user.Id;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }
            return null;
        }
      
    }
}
