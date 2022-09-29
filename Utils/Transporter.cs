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
using LicentaUTCN.DTO;
using LicentaUTCN.Models;

namespace LicentaUTCN.Utils
{
    public static class Transporter
    {
        public static string LogedInParentId { get; set; }
        public static int SubjectId { get; set; }
        public static string LogedInStudentId { get; set; }
        public static bool IsParentLoggedIn { get; set; }

        public static List<QuestionModel> TestQuestions { get; set; }


    }
}