using Monte_Karlo.Models;

namespace Monte_Karlo.DataBase.Tests
{
    [TestClass()]
    public class DatabaseHelperTests
    {
        [ClassCleanup]
        public static void ClassCleanup()
        {
            var context = new AppDbContext();
            context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public static void SaveResultsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public static void GetDataTest()
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