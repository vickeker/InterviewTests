using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    //Test that the function HasGraduated return something
    [TestClass]
    public class GraduationTrackerTests
    {
        
        GraduationTracker tracker = new GraduationTracker();

        //Created a provider service class to get data used for UnitTest
        MyDataProvider mydataprovider = new MyDataProvider();

        [TestMethod]
        public void TestHasGraduated()
        {
            Diploma diploma = mydataprovider.getDiploma();
            Student[] students = mydataprovider.getStudents(); 
            var graduated = new List<Tuple<bool, Standing>>();

            foreach(var student in students)
            {
                graduated.Add(tracker.HasGraduated(diploma, student));         
            }
            
            Assert.IsTrue(graduated.Any());   //Fixed broken test by replacing IsFalse() with IsTrue()

        }

        //Test the consistency of standing calculation logic
        [TestMethod]
        public void TestStanding()
        {
            Diploma diploma = mydataprovider.getDiploma();
            Student[] students = mydataprovider.getStudents();
            var graduated = new List<Tuple<bool, Standing>>();
            Boolean res = true;
            foreach (var student in students)
            {
                var standing = tracker.HasGraduated(diploma,student).Item2;
                switch (student.Id)
                {
                    case 1:
                        {
                            if (standing != Standing.MagnaCumLaude)
                            {
                                res = false;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (standing != Standing.MagnaCumLaude)
                            {
                                res = false;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (standing != Standing.Average)
                            {
                                res = false;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (standing != Standing.Remedial)
                            {
                                res = false;
                            }
                            break;
                        }
                }
            }

            Assert.IsTrue(res);

        }

        //Test the consistency of credit calculation logic
        [TestMethod]
        public void TestCredits()
        {
            Diploma diploma = mydataprovider.getDiploma();
            Student[] students = mydataprovider.getStudents();
            var graduated = new List<Tuple<bool, Standing>>();
            Boolean res = true;
            foreach (var student in students)
            {
                var credit = tracker.HasGraduated(diploma, student).Item1;
                switch (student.Id)
                {
                    case 1:
                        {
                            if (!credit)
                            {
                                res = false;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (!credit)
                            {
                                res = false;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (!credit)
                            {
                                res = false;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (credit)
                            {
                                res = false;
                            }
                            break;
                        }
                }
            }

            Assert.IsTrue(res);

        }
    }
}
