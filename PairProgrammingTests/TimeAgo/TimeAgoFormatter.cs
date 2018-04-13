using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace TimeAgoFormatter
{
    /// <summary>
    /// Write a class that will be given a date of an event (like a social media post) and will return a string that describes how long ago it was from today.
    /// If more than a year ago it should return "X years ago"
    /// If more than a month ago it should return "X months ago"
    /// If more than a day ago it should return "X days go"
    /// If more than an hour ago it should return "X hours ago"
    /// If more than a minute ago it should return "X minutes ago"
    /// otherwise it should return "just now"
    /// if today is a Friday it should append :) at the end
    /// if today is a Monday it should return :( at the end
    /// </summary>
    public class TimeAgoFormatter
    {
        private readonly DateTime currentDate;
        public TimeAgoFormatter(DateTime currentDate)
        {
            this.currentDate = currentDate;
        }

        public string Format(DateTime dateOfEvent)
        {
            TimeSpan span = currentDate - dateOfEvent;

            double totalYears = span.TotalDays / 365d;
            double totalMonths = span.TotalDays / 30.5d;

            string result = "";

            if (totalYears >= 1)
            {
                result = $"{Math.Round(totalYears, 0)} years ago";
            }
            else if (totalMonths >= 1)
            {
                result = $"{Math.Round(totalMonths, 0)} months ago";
            }
            else if (span.TotalDays >= 1)
            {
                result = $"{Math.Round(span.TotalDays, 0)} days ago";
            }
            else if (span.TotalHours >= 1)
            {
                result = $"{Math.Round(span.TotalHours, 0)} hours ago";
            }
            else if (span.TotalMinutes >= 1)
            {
                result = $"{Math.Round(span.TotalMinutes, 0)} minutes ago";
            }
            else
            {
                result = "Just now";
            }

            if (currentDate.DayOfWeek == DayOfWeek.Monday)
                result += " :(";
            else if (currentDate.DayOfWeek == DayOfWeek.Friday)
                result += " :)";

            return result;
        }
    }
}
