using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Monte_Karlo.Models;
using System.Drawing;

namespace TestProject.Models
{
    [TestClass]
    public class PointsDataTests
    {
        // Проверка инициализации коллекций
        [TestMethod]
        public void PointsData_DefaultInitialization_CollectionsAreEmpty()
        {
            // Arrange & Act
            var data = new PointsData();

            // Assert
            Assert.AreEqual(0, data.Points.Count);
            Assert.AreEqual(0, data.IncludedPoints.Count);
            Assert.AreEqual(0, data.CuttedPoints.Count);
        }

        // Корректность добавления элементов в коллекции
        [TestMethod]
        public void PointsData_AddPoints_CollectionsContainItems()
        {
            // Arrange & Act
            var data = new PointsData
            {
                Points = new List<PointF> { new PointF(1, 2) },
                IncludedPoints = new List<PointF> { new PointF(3, 4) },
                CuttedPoints = new List<PointF> { new PointF(5, 6) }
            };

            // Assert
            Assert.AreEqual(1, data.Points.Count);
            Assert.AreEqual(1, data.IncludedPoints.Count);
            Assert.AreEqual(1, data.CuttedPoints.Count);
        }
    }
}