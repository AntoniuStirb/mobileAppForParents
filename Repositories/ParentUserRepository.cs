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
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using LicentaUTCN.Models;

namespace LicentaUTCN.Repositories
{
    class ParentUserRepository
    {
        string webAPIKey = "AIzaSyAcChzONkAXtdaFdxrJL2CWG7TsytHGbMs";
        public static FirebaseClient firebaseClient = new FirebaseClient("https://licentadbfirebase-default-rtdb.firebaseio.com/");
  

        public ParentUserRepository()
        { 
            
        }


        public async Task<bool> Register(string email, string firstName, string lastName, string password)
        {


            try
            {
                await firebaseClient
                .Child("ParentUsers")
                .PostAsync(new ParentUserModel { Id = Guid.NewGuid().ToString(), Email = email, Password = password, FirstName = firstName, LastName = lastName  });
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public async Task<ParentUserModel> Login(string email, string password)
        {
            try
            {
                var user = (await firebaseClient.Child("ParentUsers").OnceAsync<ParentUserModel>()).Select(
                    item => new ParentUserModel
                    {
                        Email = item.Object.Email,
                        Password = item.Object.Password,
                        Id = item.Object.Id,
                        FirstName = item.Object.FirstName,
                        LastName= item.Object.LastName
                    }).Where(e => e.Email == email && e.Password == password).FirstOrDefault();
                return user;
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                
            }
            return null;
        }
        
    }
}