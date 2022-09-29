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

namespace LicentaUTCN.Models
{
    public class QuestionModel
    {
        private QuestionModel question;
        public string Id { get; set; }
        public string TestId { get; set; }
        public string Question { get; set; }
        public string FirstAnswear { get; set; }
        public string SecondAnswear { get; set; }
        public string ThirdAnswear { get; set; }
        public string ForthAnswear { get; set; }
        public int CorrectAnswear { get; set; }
    }
}