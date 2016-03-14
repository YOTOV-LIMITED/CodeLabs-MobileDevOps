﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using System.Fakes;
using MyHealth.Client.Core.Model;

namespace MyHealth.Client.Core.UnitTests.Helpers
{
    [TestClass]
    public class TimeOfDayHelperTests
    {
        [TestMethod]
        public void Test_GetTimeOffsetForNextPill_Breakfast_BeforeBreakfast_IsTodayBfast()
        {
            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2016, 2, 25, 7, 0, 0);

                var ts = TimeOfDayHelper.GetTimeOffsetForNextPill(TimeOfDay.Breakfast);
                Assert.AreEqual(1, ts.TotalHours);
            }
        }

        [TestMethod]
        public void Test_GetTimeOffsetForNextPill_Breakfast_AfterBreakfast_IsTomorrowBfast()
        {
            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2016, 2, 25, 10, 0, 0);

                var ts = TimeOfDayHelper.GetTimeOffsetForNextPill(TimeOfDay.Breakfast);
                Assert.AreEqual(22, ts.TotalHours);
            }
        }

        [TestMethod]
        public void Test_GetTimeOffsetForNextPill_Lunch_BeforeLunch_IsLunch()
        {
            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2016, 2, 25, 10, 0, 0);

                var ts = TimeOfDayHelper.GetTimeOffsetForNextPill(TimeOfDay.Lunch);
                Assert.AreEqual(4, ts.TotalHours);
            }
        }

        [TestMethod]
        public void Test_GetTimeOffsetForNextPill_Lunch_AfterLunch_IsTomorrowLunch()
        {
            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2016, 2, 25, 15, 0, 0);

                var ts = TimeOfDayHelper.GetTimeOffsetForNextPill(TimeOfDay.Lunch);
                Assert.AreEqual(23, ts.TotalHours);
            }
        }

        [TestMethod]
        public void Test_GetTimeOffsetForNextPill_Dinner_BeforeDinner_IsDinner()
        {
            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2016, 2, 25, 16, 0, 0);

                var ts = TimeOfDayHelper.GetTimeOffsetForNextPill(TimeOfDay.Dinner);
                Assert.AreEqual(4, ts.TotalHours);
            }
        }

        [TestMethod]
        public void Test_GetTimeOffsetForNextPill_Dinner_AfterDinner_IsTomorrowDinner()
        {
            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2016, 2, 25, 22, 0, 0);

                var ts = TimeOfDayHelper.GetTimeOffsetForNextPill(TimeOfDay.Dinner);
                Assert.AreEqual(22, ts.TotalHours);
            }
        }

        [TestMethod]
        public void Test_GetTimeBetween_B_L()
        {
            var ts = TimeOfDayHelper.GetTimeBetween(TimeOfDay.Breakfast, TimeOfDay.Lunch);
            Assert.AreEqual(5, ts.TotalHours);
        }

        [TestMethod]
        public void Test_GetTimeBetween_B_D()
        {
            var ts = TimeOfDayHelper.GetTimeBetween(TimeOfDay.Breakfast, TimeOfDay.Dinner);
            Assert.AreEqual(12, ts.TotalHours);
        }

        [TestMethod]
        public void Test_GetTimeBetween_L_D()
        {
            var ts = TimeOfDayHelper.GetTimeBetween(TimeOfDay.Lunch, TimeOfDay.Dinner);
            Assert.AreEqual(7, ts.TotalHours);
        }

        [TestMethod]
        public void Test_GetTimeBetween_D_B()
        {
            var ts = TimeOfDayHelper.GetTimeBetween(TimeOfDay.Dinner, TimeOfDay.Breakfast);
            Assert.AreEqual(12, ts.TotalHours);
        }

        [TestMethod]
        public void Test_GetTimeBetween_L_B()
        {
            var ts = TimeOfDayHelper.GetTimeBetween(TimeOfDay.Lunch, TimeOfDay.Breakfast);
            Assert.AreEqual(19, ts.TotalHours);
        }

        [TestMethod]
        public void Test_GetTimeBetween_L_L()
        {
            var ts = TimeOfDayHelper.GetTimeBetween(TimeOfDay.Lunch, TimeOfDay.Lunch);
            Assert.AreEqual(23, ts.Hours);
            Assert.AreEqual(59, ts.Minutes);
            Assert.AreEqual(59, ts.Seconds);
        }
    }
}
