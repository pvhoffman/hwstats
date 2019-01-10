using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using hwstats.app;

namespace hwstats.tests
{
    [TestClass]
    public class ObservationReaderTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid input.")] 
        public void TestInvalidInput()
        {
            ObservationReader.ReadObservations(null);
        }
        [TestMethod]
        public void TestEmptyTextReader()
        {
            List<Observation> os = ObservationReader.ReadObservations(new StringReader(""));
            Assert.IsTrue(os != null && os.Count == 0);
        }
        private List<Observation> GetValidObservationList()
        {
            List<Observation> osi  = new List<Observation>()
            {
                new Observation{Height = 60.4f, Weight = 150.0f}
                , new Observation{Height = 53.6f, Weight = 193.3f}
                , new Observation{Height = 72.2f, Weight = 227.5f}
            };
            return osi;
 
        }
        [TestMethod]
        public void TestAllValidObservations()
        {
            List<Observation> osi = GetValidObservationList();
            StringBuilder sb = new StringBuilder();
            foreach(Observation o in osi)
            {
                sb.AppendLine($"{o.Height} {o.Weight}");
            }

            List<Observation> oso = ObservationReader.ReadObservations(new StringReader(sb.ToString()));
            Assert.AreEqual(osi.Count, oso.Count);
            for(int i = 0; i < osi.Count; i++)
            {
                Assert.AreEqual(osi[i].Height, oso[i].Height, 0.01);
                Assert.AreEqual(osi[i].Weight, oso[i].Weight, 0.01);
            }
        }
        [TestMethod]
        public void TestOneInvalidAllOtherValidObservations()
        {
            List<Observation> osi = GetValidObservationList();
            StringBuilder sb = new StringBuilder();
            foreach(Observation o in osi)
            {
                sb.AppendLine($"{o.Height} {o.Weight}");
            }
            sb.AppendLine("-222.2 ABC");
            List<Observation> oso = ObservationReader.ReadObservations(new StringReader(sb.ToString()));
            Assert.AreEqual(osi.Count, oso.Count);
            for(int i = 0; i < osi.Count; i++)
            {
                Assert.AreEqual(osi[i].Height, oso[i].Height, 0.01);
                Assert.AreEqual(osi[i].Weight, oso[i].Weight, 0.01);
            }
        }
        [TestMethod]
        public void TestAllValidObservationsForWhitespaceSeps()
        {
            List<Observation> osi = GetValidObservationList();
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            foreach(Observation o in osi)
            {
                string ws1 = new string(' ', rand.Next(1, 25));
                string ws2 = new string('\t', rand.Next(1, 25));
                sb.AppendLine($"{o.Height}{ws1}{ws2}{o.Weight}");
            }
            List<Observation> oso = ObservationReader.ReadObservations(new StringReader(sb.ToString()));
            Assert.AreEqual(osi.Count, oso.Count);
            for(int i = 0; i < osi.Count; i++)
            {
                Assert.AreEqual(osi[i].Height, oso[i].Height, 0.01);
                Assert.AreEqual(osi[i].Weight, oso[i].Weight, 0.01);
            }
        }
    }
}
 
