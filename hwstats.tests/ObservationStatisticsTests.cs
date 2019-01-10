using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using hwstats.app;

namespace hwstats.tests
{        
    [TestClass]
    public class ObservationStatisticsTests
    {
        private const int TEST_OBSERVATION_COUNT = 5000;
        private const int MIN_TEST_OBSERVATION_HEIGHT = 40;
        private const int MAX_TEST_OBSERVATION_HEIGHT = 80;
        private const int MIN_TEST_OBSERVATION_WEIGHT = 90;
        private const int MAX_TEST_OBSERVATION_WEIGHT = 310;

        private List<Observation> GetRandomOrderedObservationList()
        {
            Random rand = new Random();
            List<Observation> result  = new List<Observation>(TEST_OBSERVATION_COUNT);
            float h, w;
            for(int i = 0; i < TEST_OBSERVATION_COUNT;i++)
            {
                h = rand.Next(MIN_TEST_OBSERVATION_HEIGHT, MAX_TEST_OBSERVATION_HEIGHT);
                w = rand.Next(MIN_TEST_OBSERVATION_WEIGHT, MAX_TEST_OBSERVATION_WEIGHT);
                result.Add(new Observation(){Height = h, Weight = w});
            }
            return result;
        }
        private Observation GetObservationWithPredicate(List<Observation> obs, Func<Observation, Observation, bool> pred)
        {
            Observation r = null;
            foreach(Observation ob in obs)
            {
                if(r == null || pred(r, ob))
                {
                    r = ob;
                }
            }
            return r;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid input.")] 
        public void Test_ObservationStatistics_With_Emtpy_List()
        {
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(new List<Observation>());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid input.")] 
        public void Test_ObservationStatistics_With_Null_List()
        {
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(null);
        }
        //Requirement #1
        [TestMethod]
        public void Test_Largest_Height_Observation_With_Random_List()
        {
            List<Observation> obs = GetRandomOrderedObservationList();
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Observation ob = GetObservationWithPredicate(obs, (Observation lhs, Observation rhs) => {return lhs.Height < rhs.Height;});
            Assert.AreEqual(ob.Height, stats.GreatestHeightObservation.Height, 0.01);
        }
        //Requirement #2
        [TestMethod]
        public void Test_Largest_Weight_Observation_With_Random_List()
        {
            List<Observation> obs = GetRandomOrderedObservationList();
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Observation ob = GetObservationWithPredicate(obs, (Observation lhs, Observation rhs) => {return lhs.Weight < rhs.Weight;});
            Assert.AreEqual(ob.Weight, stats.GreatestWeightObservation.Weight, 0.01);
        }
        //Requirement #3
        [TestMethod]
        public void Test_Smallest_Height_Observation_With_Random_List()
        {
            List<Observation> obs = GetRandomOrderedObservationList();
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Observation ob = GetObservationWithPredicate(obs, (Observation lhs, Observation rhs) => {return lhs.Height > rhs.Height;});
            Assert.AreEqual(ob.Height, stats.LeastHeightObservation.Height, 0.01);
        }
         //Requirement #4
        [TestMethod]
        public void Test_Smallest_Weight_Observation_With_Random_List()
        {
            List<Observation> obs = GetRandomOrderedObservationList();
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Observation ob = GetObservationWithPredicate(obs, (Observation lhs, Observation rhs) => {return lhs.Weight > rhs.Weight;});
            Assert.AreEqual(ob.Weight, stats.LeastWeightObservation.Weight, 0.01);
        }
        //Requirement #5a
        [TestMethod]
        public void Test_Mean_Height_With_Known_List()
        {
            List<Observation> obs = new List<Observation>(){
                new Observation {Height = 65, Weight = 150}
                , new Observation {Height = 68, Weight = 160}
                , new Observation {Height = 70, Weight = 172}
                , new Observation {Height = 72, Weight = 168}
                , new Observation {Height = 76, Weight = 185}
            };
            // 65 + 68 + 70 + 72 + 76 = 351
            // 351 / 5 = 70.2
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Assert.AreEqual(70.2, stats.MeanHeight, 0.01);
        }
        //Requirement #5b
        [TestMethod]
        public void Test_Mean_Weight_With_Known_List()
        {
            List<Observation> obs = new List<Observation>(){
                new Observation {Height = 65, Weight = 150}
                , new Observation {Height = 68, Weight = 160}
                , new Observation {Height = 70, Weight = 172}
                , new Observation {Height = 72, Weight = 168}
                , new Observation {Height = 76, Weight = 185}
            };
            // 150 + 160 + 172 + 168 + 185 = 835
            //  835 / 5 = 167
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Assert.AreEqual(167, stats.MeanWeight, 0.01);
        }
        //Requirement #6a1
        [TestMethod]
        public void Test_Median_Height_With_Known_Even_Item_List()
        {
            List<Observation> obs = new List<Observation>(){
                new Observation {Height = 65, Weight = 150}
                , new Observation {Height = 68, Weight = 160}
                , new Observation {Height = 70, Weight = 172}
                , new Observation {Height = 72, Weight = 168}
                , new Observation {Height = 76, Weight = 185}
                , new Observation {Height = 77, Weight = 181}
            };
            // 70 + 72 = 142
            //  142 / 2 = 71
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Assert.AreEqual(71.0f, stats.MedianHeight, 0.01);
        }
        //Requirement #6a2
        [TestMethod]
        public void Test_Median_Height_With_Known_Odd_Item_List()
        {
            List<Observation> obs = new List<Observation>(){
                new Observation {Height = 65, Weight = 150}
                , new Observation {Height = 68, Weight = 160}
                , new Observation {Height = 70, Weight = 172}
                , new Observation {Height = 72, Weight = 168}
                , new Observation {Height = 76, Weight = 185}
            };
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Assert.AreEqual(70.0f, stats.MedianHeight, 0.01);
        }



        //Requirement #6b1
        [TestMethod]
        public void Test_Median_Weight_With_Known_Even_Item_List()
        {
            List<Observation> obs = new List<Observation>(){
                new Observation {Height = 65, Weight = 150}
                , new Observation {Height = 68, Weight = 160}
                , new Observation {Height = 70, Weight = 172}
                , new Observation {Height = 72, Weight = 168}
                , new Observation {Height = 76, Weight = 185}
                , new Observation {Height = 77, Weight = 181}
            };
            // 172 + 168 = 
            // 340 / 2 = 170
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Assert.AreEqual(170.0f, stats.MedianWeight, 0.01);
        }
        //Requirement #6b2
        [TestMethod]
        public void Test_Median_Weight_With_Known_Odd_Item_List()
        {
            List<Observation> obs = new List<Observation>(){
                new Observation {Height = 65, Weight = 150}
                , new Observation {Height = 68, Weight = 160}
                , new Observation {Height = 70, Weight = 172}
                , new Observation {Height = 72, Weight = 168}
                , new Observation {Height = 76, Weight = 185}
            };
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Assert.AreEqual(172.0f, stats.MedianWeight, 0.01);
        }

        //Requirement #7a
        [TestMethod]
        public void Test_Height_Standard_Deviation()
        {
            List<Observation> obs = new List<Observation>(){
                new Observation {Height = 65, Weight = 150}
                , new Observation {Height = 68, Weight = 160}
                , new Observation {Height = 70, Weight = 172}
                , new Observation {Height = 72, Weight = 168}
                , new Observation {Height = 76, Weight = 185}
            };
            // 65 + 68 + 70 + 72 + 76 = 351
            // 351 / 5 = 70.2 = mean
            // (65 - 70.2)^2 + (68 - 70.2)^2 + (70 - 70.2)^2 + (72 - 70.2)^2 + (76 - 70.2)^2 = 68.8
            // 68.8 / 5 = 13.76
            // sqrt 13.76 = 3.70944
            ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
            Assert.AreEqual(3.70944, stats.HeightStandardDeviation, 0.01);
        }





    }
}
