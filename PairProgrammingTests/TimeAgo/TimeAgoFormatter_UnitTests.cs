using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TimeAgoFormatter
{
	[TestFixture("2017-11-20 18:00:00")]
    class TimeAgoFormatter_UnitTestsMonday
    {

        private TimeAgoFormatter taf;
        private DateTime utcCurrentDate;

		public TimeAgoFormatter_UnitTestsMonday(string currentDate)
        {
            utcCurrentDate = DateTime.ParseExact(currentDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            taf = new TimeAgoFormatter(utcCurrentDate);
        }


		[TestCase("2015-10-03 12:00:00","2 years ago :(")]
        [TestCase("2014-10-03 12:00:00", "3 years ago :(")]
        [TestCase("2016-11-01 12:00:00", "1 years ago :(")]

        [TestCase("2017-10-20 12:00:00", "1 months ago :(")]
        [TestCase("2017-09-25 12:00:00", "2 months ago :(")]
        [TestCase("2016-11-25 12:00:00", "12 months ago :(")]

        [TestCase("2017-10-21 12:00:00", "30 days ago :(")]
        [TestCase("2017-11-19 12:00:00", "1 days ago :(")]

        [TestCase("2017-11-20 12:00:00", "6 hours ago :(")]
        [TestCase("2017-11-20 17:55:00", "5 minutes ago :(")]
        [TestCase("2017-11-20 17:59:20", "Just now :(")]
        public void TimeAgoFormatter_IsValid(string utcDateOfEvent,string expectedResult) 
        {
            DateTime dateOfEvent = DateTime.ParseExact(utcDateOfEvent, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            string result = taf.Format(dateOfEvent);
            Assert.AreEqual(expectedResult, result);
        }

    }

	[TestFixture("2017-11-17 18:00:00")]
	class TimeAgoFormatter_UnitTestsFriday
    {
        private TimeAgoFormatter taf;
        private DateTime utcCurrentDate;

        public TimeAgoFormatter_UnitTestsFriday(string currentDate)
        {
            utcCurrentDate = DateTime.ParseExact(currentDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            taf = new TimeAgoFormatter(utcCurrentDate);
        }


        [TestCase("2015-10-03 12:00:00", "2 years ago :)")]
        public void TimeAgoFormatter_IsValid(string utcDateOfEvent, string expectedResult)
        {
            DateTime dateOfEvent = DateTime.ParseExact(utcDateOfEvent, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            string result = taf.Format(dateOfEvent);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
