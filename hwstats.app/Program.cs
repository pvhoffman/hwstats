using System;
using System.IO;
using System.Collections.Generic;

namespace hwstats.app
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if(args == null || args.Length <= 1 || !File.Exists(args[1]))
                {
                    throw new Exception("Invalid command line argument");
                }

                List<Observation> obs = null;
                using(StreamReader sr = new StreamReader(args[1]))
                {
                    obs = ObservationReader.ReadObservations(sr);
                }
                ObservationStatistics stats = ObservationStatistics.CalculateStatistics(obs);
                PrintStatistics(stats);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Unexpected exception occured: {ex.Message}.");
            }
        }
        static void PrintStatistics(ObservationStatistics stats)
        {
            Console.WriteLine($"Observation with greatest height:\tHeight = {stats.GreatestHeightObservation.Height}, Weight = {stats.GreatestHeightObservation.Weight}");
            Console.WriteLine($"Observation with greatest weight:\tHeight = {stats.GreatestWeightObservation.Height}, Weight = {stats.GreatestWeightObservation.Weight}");
            Console.WriteLine($"Observation with least height:\tHeight = {stats.LeastHeightObservation.Height}, Weight = {stats.LeastHeightObservation.Weight}");
            Console.WriteLine($"Observation with least weight:\tHeight = {stats.LeastWeightObservation.Height}, Weight = {stats.LeastWeightObservation.Weight}");
            Console.WriteLine($"Mean Height:\t{stats.MeanHeight}");
            Console.WriteLine($"Mean Weight:\t{stats.MeanWeight}");
            Console.WriteLine($"Median Height:\t{stats.MedianHeight}");
            Console.WriteLine($"Median Weight:\t{stats.MedianWeight}");
            Console.WriteLine($"Height Standard Deviation:\t{stats.HeightStandardDeviation}");
            Console.WriteLine($"Weight Standard Deviation:\t{stats.WeightStandardDeviation}");

        }

    }
}
