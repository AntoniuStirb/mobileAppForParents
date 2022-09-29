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
using LicentaUTCN.Models;

namespace LicentaUTCN.DTO
{
    public class TestQuestionDTO
    {
        public TestModel Test { get; set; }
        public List<QuestionModel> Questions {get;set;}
    }
}