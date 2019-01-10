using System;
using System.IO;
using System.Collections.Generic;

namespace hwstats.app
{
    public class ObservationStatistics
    {
        // Requirement #1 - Observation with greatest height
        public Observation GreatestHeightObservation {get;set;}
        // Requirement #2 - Observation with greatest weight
        public Observation GreatestWeightObservation {get;set;}
        // Requirement #3 - Observation with least height
        public Observation LeastHeightObservation {get;set;}
        // Requirement #4 - Observation with least weight
        public Observation LeastWeightObservation {get;set;}
        // Requirement #5a - Mean Height;
        public float MeanHeight {get;set;}
        // Requirement #5b - Mean Weight;
        public float MeanWeight {get;set;}
        // Requirement #6a - Median Height;
        public float MedianHeight {get;set;}
        // Requirement #6b - Median Weight;
        public float MedianWeight {get;set;}
        // Requirement #7a - Height Standard Deviation;
        public float HeightStandardDeviation {get;set;}
        // Requirement #7b - Weight Standard Deviation;
        public float WeightStandardDeviation {get;set;}

        // The only way by which to create an instance of this class is by calling the
        // CalculateStatistics method
        private ObservationStatistics()
        {
        }

        // Only method by which to obtain an instance of this class
        public static ObservationStatistics CalculateStatistics(List<Observation> obs)
        {
            if(obs == null || obs.Count == 0)
            {
                throw new ArgumentException("Invalid input.");
            }
            int m = (obs.Count / 2) - 1;
            float h = 0.0f, w = 0.0f;
            ObservationStatistics result = new ObservationStatistics();

            result.GreatestHeightObservation = result.GetObservationWithComparisonPredicate(obs
                    , (lhs, rhs) => lhs.Height.CompareTo(rhs.Height));
            result.GreatestWeightObservation = result.GetObservationWithComparisonPredicate(obs
                    , (lhs, rhs) => lhs.Weight.CompareTo(rhs.Weight));
            result.LeastHeightObservation = result.GetObservationWithComparisonPredicate(obs
                    , (lhs, rhs) => -1 * lhs.Height.CompareTo(rhs.Height));
            result.LeastWeightObservation = result.GetObservationWithComparisonPredicate(obs
                    , (lhs, rhs) => -1 * lhs.Weight.CompareTo(rhs.Weight));

            obs.ForEach((ob) => {
                    result.MeanHeight += ob.Height;
                    result.MeanWeight += ob.Weight;
            });
            result.MeanHeight /= (float)obs.Count;
            result.MeanWeight /= (float)obs.Count;

            if((obs.Count & 1) == 0)
            {
                result.MedianHeight = (obs[m].Height + obs[m+1].Height) / 2.0f;
                result.MedianWeight = (obs[m].Weight + obs[m+1].Weight) / 2.0f;
            }
            else
            {
                result.MedianHeight = obs[m].Height;
                result.MedianWeight = obs[m].Weight;
            }

            obs.ForEach((ob) => {
                    h = h + (float)Math.Pow((ob.Height - result.MeanHeight), 2.0);
                    w = w + (float)Math.Pow((ob.Weight - result.MeanWeight), 2.0);
            });
            h = h / (float)obs.Count;
            w = w / (float)obs.Count;
            result.HeightStandardDeviation = (float)Math.Sqrt(h);
            result.WeightStandardDeviation = (float)Math.Sqrt(w);
            return result;
        }

        private Observation GetObservationWithComparisonPredicate(List<Observation> obs, Comparison<Observation> comp)
        {
            obs.Sort(comp);
            return obs[obs.Count-1];
        }
    }
}

