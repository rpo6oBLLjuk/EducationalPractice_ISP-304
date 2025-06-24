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
            int totalPoints = 100;
            DatabaseHelper.SaveResults(circle, totalPoints, 1, 50, 45, 1);
            var result = DatabaseHelper.GetData(circle, totalPoints);
            Console.WriteLine(result);
            foreach (var mem in result.GetType().GetMembers())
            { 
                Console.WriteLine($"{mem.ToString()} - {mem.Name}, {mem.MemberType}");
            }
                
        }
    }
}