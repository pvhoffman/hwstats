using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using hwstats.app;

namespace hwstats.tests
{
    [TestClass]
    public class ObservationTests
    {
        [TestMethod]
        public void TestSetHeight()
        {
            float Height = 60.0f;
            Observation o = new Observation
            {
                Height = Height
            };
            Assert.AreEqual(Height, o.Height, 0.01);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Height must be greater than 0.")] 
        public void TestSetNegativeHeight()
        {
            float Height = -60.0f;
            Observation o = new Observation
            {
                Height = Height
            };

        }
        [TestMethod]
        public void TestSetWeight()
        {
            float Weight = 60.0f;
            Observation o = new Observation
            {
                Weight = Weight
            };
            Assert.AreEqual(Weight, o.Weight, 0.01);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Weight must be greater than 0.")] 
        public void TestSetNegativeWeight()
        {
            float Weight = -60.0f;
            Observation o = new Observation
            {
                Weight = Weight
            };

        }

    }
}
