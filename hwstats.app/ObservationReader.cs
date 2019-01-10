using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace hwstats.app
{
    public class ObservationReader
    {

        public static List<Observation> ReadObservations(TextReader reader)
        {
            if(reader == null)
            {
                throw new ArgumentException("Invalid input.");
            }
            List<Observation> result = new List<Observation>();
            string pattern = @"\s*[-+]?([0-9\.,]+)";
            string ins;

            while((ins = reader.ReadLine()) != null)
            {
                MatchCollection matches = Regex.Matches(ins, pattern, RegexOptions.IgnorePatternWhitespace);
                if(matches.Count >= 2)
                {
                    string sh = matches[0].Groups[1].Value;
                    string sw = matches[1].Groups[1].Value;
                    float h, w;
                    if(Single.TryParse(sh, out h) && Single.TryParse(sw, out w))
                    {
                        try
                        {
                            Observation o = new Observation{Height=h, Weight=w};
                            result.Add(o);
                        }
                        catch(ArgumentException)
                        {
                        }
                    }
                }
            }
            return result;
        }
    }
}

