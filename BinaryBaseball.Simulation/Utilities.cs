using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryBaseball.Simulation
{
    using BinaryBaseball.Common;

    public class Utilities
    {
        public static Int32 GetPitchCount(PitchingStats stats)
        {
            Double pitchCount = (Double)stats.Strikeouts * 4.81;
            pitchCount += (Double)stats.Walks * 5.14;
            pitchCount += (Double)stats.Hits * 3.27;            
            pitchCount += (Double)(stats.TotalOuts - stats.Strikeouts) * 3.16;            

            return (Int32)Math.Round(pitchCount);
        }

        public static Int32 CalculateExpectedPitchCount(PitchingStats stats, Boolean isStarter)
        {
            Int32 expectedPitchCount = 0;

            Int32 totalOuts = (stats.Innings * 3) - stats.Strikeouts;
            Int32 totalPitches = (Int32)((Double)stats.Strikeouts * 5.0);
            totalPitches += (Int32)((Double)stats.Walks * 5.3);
            totalPitches += (Int32)((Double)stats.Hits * 3.4);
            totalPitches += (Int32)((Double)totalOuts * 3.3);

            Double startPercent = stats.GamesStarted / stats.Games;
            Boolean mostlyStarter = false;
            if (startPercent > 0.66)
            {
                mostlyStarter = true;
            }

            if (isStarter)
            {
                if (mostlyStarter == false)
                {
                    expectedPitchCount = 105;
                }
                else
                {
                    Double reliefInnings = 0;
                    Double startInnings = 0;
                    if (stats.GamesStarted == 0)
                    {
                        reliefInnings = stats.Innings;
                    }
                    else if (stats.GamesStarted == stats.Games)
                    {
                        startInnings = stats.Innings;
                    }
                    else
                    {
                        reliefInnings = (stats.Games - stats.GamesStarted) * 1.7;
                        startInnings = stats.Innings - reliefInnings;
                    }

                    Double startPitches = totalPitches * (startInnings / stats.Innings);
                    expectedPitchCount = (Int32)Math.Ceiling(startPitches / stats.GamesStarted);

                    if (expectedPitchCount < 64)
                    {
                        expectedPitchCount = 64;
                    }

                    if (expectedPitchCount > 145)
                    {
                        expectedPitchCount = 145;
                    }
                }
            }
            else
            {
                if (mostlyStarter)
                {
                    expectedPitchCount = 50;
                }
                else
                {
                    Double startInnings = stats.GamesStarted * 5.7;
                    Double reliefInnings = stats.Innings - startInnings;

                    Double reliefPitches = totalPitches * (reliefInnings / stats.Innings);
                    expectedPitchCount = (Int32)Math.Ceiling(reliefPitches / (stats.Games - stats.GamesStarted));

                }
            }

            return expectedPitchCount;
        }
    }
}
