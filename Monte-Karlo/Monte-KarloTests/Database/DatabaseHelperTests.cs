using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monte_Karlo.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monte_Karlo.Models;

namespace Monte_Karlo.DataBase.Tests
{
    [TestClass()]
    public class DatabaseHelperTests
    {
        [ClassCleanup]
        public void ClassCleanup()
        {
            var context = new AppDbContext();
            context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public void SaveResultsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDataTest()
        {
            Circle circle = new Circle();
            PointsData totalPoints = new()
            {
                Points = new List<System.Drawing.PointF>(100),
                IncludedPoints = new List<System.Drawing.PointF>(75),
                CuttedPoints = new List<System.Drawing.PointF>(50),
            };

            DatabaseHelper dbHelper = new();
            dbHelper.SaveResults(circle, totalPoints, 50, 45);
            var result = dbHelper.GetData(circle, totalPoints.Points.Count());
            Console.WriteLine(result);
            foreach (var mem in result.GetType().GetMembers())
            { 
                Console.WriteLine($"{mem.ToString()} - {mem.Name}, {mem.MemberType}");
            }
                
        }
    }
}