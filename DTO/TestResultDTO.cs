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

namespace LicentaUTCN.DTO
{
    public class TestResultDTO
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string TestId { get; set; }
        public int NoOfCorrectResponses { get; set; }
        public int NoOfQuestions { get; set; }
        public string SubjectName { get; set; }
    }
}