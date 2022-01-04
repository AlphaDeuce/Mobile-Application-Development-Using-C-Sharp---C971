﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using WGUPortalv2.Models;
using System.Linq;

namespace WGUPortalv2.Services
{
    public static class DatabaseHandler
    {
        static SQLiteAsyncConnection db;

        public static List<Course> courseList;
        public static List<Assessment> assessmentList;
        public static List<Term> termList;

        static async Task Init()
        {

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WGU.db3");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Term>();
            await db.CreateTableAsync<Course>();
            await db.CreateTableAsync<Assessment>();

            termList = await db.Table<Term>().ToListAsync();
            courseList = await db.Table<Course>().ToListAsync();
            assessmentList = await db.Table<Assessment>().ToListAsync();

        }


        public static async Task AddTerm(string title, DateTime startDate, DateTime endDate)
        {
            await Init();
            var term = new Term
            {
                TermTitle = title,
                TermStartDate = startDate,
                TermEndDate = endDate
            };

            await db.InsertAsync(term);
        }

        public static async Task UpdateTerm(int termId, string title, DateTime startDate, DateTime endDate)
        {
            await Init();
            var term = new Term
            {
                Id = termId,
                TermTitle = title,
                TermStartDate = startDate,
                TermEndDate = endDate
            };

            await db.UpdateAsync(term);
        }

        public static async Task RemoveTerm(int id)
        {
            await Init();

            // clear out Courses Table with all entries of termId

            var courses = await db.QueryAsync<Course>($"SELECT * FROM Courses WHERE TermId = {id}");

            foreach (var course in courses)
            {
                int cid = course.Id;
                await db.DeleteAsync<Course>(cid);
            }

            await db.DeleteAsync<Term>(id);
        }

        public static async Task<Term> GetTerm(int id)
        {
            await Init();

            var term = await db.Table<Term>().FirstOrDefaultAsync(t => t.Id == id);

            return term;
        }

        public static async Task<IEnumerable<Term>> GetTerms()
        {
            await Init();

            var terms = await db.Table<Term>().ToListAsync();

            return terms;
        }

        public static async Task UpdateCourse(int courseId, int termId, string title, DateTime startDate, DateTime endDate, string name, string phone, string email, string status, string notes, bool notify)
        {
            await Init();
            
            var course = new Course
            {
                Id = courseId,
                TermId = termId,
                CourseTitle = title,
                CourseStartDate = startDate,
                CourseEndDate = endDate,
                CourseInstructorName = name,
                CourseInstructorPhone = phone,
                CourseInstructorEmail = email,
                CourseNotes = notes,
                CourseNotification = notify,
                CourseStatus = status
            };

            await db.UpdateAsync(course);
        }

        public static async Task AddCourse(int termId, string title, DateTime startDate, DateTime endDate, string name, string phone, string email, string status, string notes, bool notify)
        {
            await Init();
            var course = new Course
            {
                TermId = termId,
                CourseTitle = title,
                CourseStartDate = startDate,
                CourseEndDate = endDate,
                CourseInstructorName = name,
                CourseInstructorPhone = phone,
                CourseInstructorEmail = email,
                CourseNotes = notes,
                CourseNotification = notify,
                CourseStatus = status
            };

            await db.InsertAsync(course);
        }

        public static async Task RemoveCourse(int id)
        {
            await Init();

            await db.DeleteAsync<Course>(id);
        }

        public static async Task<Course> GetCourse(int id)
        {
            await Init();

            var course = await db.Table<Course>().FirstOrDefaultAsync(t => t.Id == id);

            return course;
        }

        public static async Task<IEnumerable<Course>> GetCourses(int id)
        {
            await Init();

            var courses = await db.QueryAsync<Course>($"SELECT * FROM Courses WHERE TermId = {id}");

            return courses;
        }

        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();

            var courses = await db.Table<Course>().ToListAsync();

            return courses;
        }





        public static async Task AddAssessment(int courseId, string title, DateTime start, DateTime end, string type, bool notify)
        {
            await Init();
            var assessment = new Assessment
            {
                CourseId = courseId,
                AssessmentTitle = title,
                AssessmentStartDate = start,
                AssessmentEndDate = end,
                AssessmentType = type,
                NotificationEnabled = notify
            };

            await db.InsertAsync(assessment);
        }

        public static async Task UpdateAssessment(int assessmentId, int courseId, string title, DateTime start, DateTime end, string type, bool notify)
        {
            await Init();
            var assessment = new Assessment
            {
                Id = assessmentId,
                CourseId = courseId,
                AssessmentTitle = title,
                AssessmentStartDate = start,
                AssessmentEndDate = end,
                AssessmentType = type,
                NotificationEnabled = notify
            };

            await db.UpdateAsync(assessment);
        }

        public static async Task RemoveAssessment(int id)
        {
            await Init();

            await db.DeleteAsync<Assessment>(id);
        }

        public static async Task<Assessment> GetAssessment(int id)
        {
            await Init();

            var assessment = await db.Table<Assessment>().FirstOrDefaultAsync(t => t.Id == id);

            return assessment;
        }


        public static async Task<IEnumerable<Assessment>> GetAssessments(int id)
        {
            await Init();

            var assessments = await db.QueryAsync<Assessment>($"SELECT * FROM Assessments WHERE CourseId = {id}");

            return assessments;
        }


        public static async Task<IEnumerable<Assessment>> GetAssessments()
        {
            await Init();

            var assessments = await db.Table<Assessment>().ToListAsync();

            return assessments;
        }

        public static async Task AddDummyData()
        {
            await Init();

            if (termList.Count() == 0)
            {
                var dummyTerm = new Term
                {
                    TermTitle = "Term One",
                    TermStartDate = new DateTime(2022, 01, 01),
                    TermEndDate = new DateTime(2022, 06, 30)
                };
                await db.InsertAsync(dummyTerm);


                var dummyCourse = new Course
                {
                    TermId = dummyTerm.Id,
                    CourseTitle = "Mobile Application - Eval",
                    CourseStartDate = new DateTime(2022, 01, 4),
                    CourseEndDate = new DateTime(2022, 01, 30),
                    CourseInstructorName = "Thomas Klehn",
                    CourseInstructorEmail = "tklehn@my.wgu.edu",
                    CourseInstructorPhone = "931-933-1921",
                    CourseStatus = "In-Progress",
                    CourseNotes = "Dummy Notes Always Good to Share",
                    CourseNotification = true
                };
                await db.InsertAsync(dummyCourse);

                var dummyAssessment = new Assessment
                {
                    CourseId = dummyCourse.Id,
                    AssessmentTitle = "Eval Assessment",
                    AssessmentStartDate = new DateTime(2022, 01, 4),
                    AssessmentEndDate = new DateTime(2022, 01, 30),
                    AssessmentType = "Objective Assessment",
                    NotificationEnabled = true
                };
                await db.InsertAsync(dummyAssessment);

                var dummyAssessmentTwo = new Assessment
                {
                    AssessmentTitle = " Eval Assessment 2",
                    AssessmentStartDate = new DateTime(2022, 01, 04),
                    AssessmentEndDate = new DateTime(2022, 01, 30),
                    CourseId = dummyCourse.Id,
                    AssessmentType = "Performance Assessment",
                    NotificationEnabled = true
                };
                await db.InsertAsync(dummyAssessmentTwo);
            }
        }

        public static async Task WipeData()
        {
            await Init();
            await db.DropTableAsync<Assessment>();
            await db.DropTableAsync<Course>();
            await db.DropTableAsync<Term>();
        }

    }
}
