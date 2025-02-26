﻿using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class ForumPostEasy
    {
        static void Main(string[] args)
        {
            Test(new[] { "12:12:12" }, new[] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            //Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            string res = GetCurrentTime(postTimes, showTimes);
            var result = res.Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int exactPostTimeLength = exactPostTime.Length;
            DateTime[] mintime = new DateTime[exactPostTime.Length];
            DateTime[] maxtime = new DateTime[exactPostTime.Length];
            DateTime currentTime = Convert.ToDateTime(exactPostTime[0]); 

            if (isValid(exactPostTime, showPostTime) == 1)
            {
                for (int i = 0; i < exactPostTime.Length; i++)
                {
                    DateTime exactPostTime_dt= Convert.ToDateTime(exactPostTime[i]);
                    String[] showPostTime_str = showPostTime[i].Split(' ');
                    switch (showPostTime_str[1])
                    {
                        case "seconds":
                            mintime[i] = exactPostTime_dt;
                            maxtime[i] = exactPostTime_dt.AddSeconds(59);
                            break;
                        case "minutes":
                            mintime[i] = exactPostTime_dt.AddMinutes(Convert.ToInt32(showPostTime_str[0]));
                            maxtime[i] = mintime[i].AddSeconds(59);
                            break;
                        case "hours":
                            mintime[i] = exactPostTime_dt.AddHours(Convert.ToInt32(showPostTime_str[0]));
                            maxtime[i] = mintime[i].AddMinutes(59);
                            maxtime[i] = maxtime[i].AddSeconds(59);
                            break;
                    }
                }
                currentTime = mintime[0];
                for (int i = 0; i < exactPostTime.Length; i++)
                {
                    if (mintime[i].TimeOfDay > currentTime.TimeOfDay)
                        currentTime = mintime[i];
                }

                return (currentTime.ToString("HH':'mm':'ss"));
            }
            else
            {
                return "impossible";
            }
        }


        //Below method is to check if the given input is valid or not.
        //For instance, if two exactPostTime are equal but their corresponding messages(showPostTime) are not the same, 
        //the given input is not valid and the method returns "impossible".
        public static int isValid(string[] exactPostTime, string[] showPostTime)
        {
            for (int current = 0; current < exactPostTime.Length; current++)
            {
                for (int next = current; next < exactPostTime.Length; next++)
                {
                    if (exactPostTime[current].Equals(exactPostTime[next]))
                        if (!showPostTime[current].Equals(showPostTime[next]))
                            return 0;

                }
            }
            return 1;
        }
    }
}