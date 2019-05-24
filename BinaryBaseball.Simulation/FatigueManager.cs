//-----------------------------------------------------------------------
// <copyright file="FatigueManager.cs" company="Brett Beard">
// Copyright 2019 Brett Beard
//
// Binary Baseball is free software: you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published by the 
// Free Software Foundation, either version 3 of the License, or (at your 
// option) any later version.
//
// Binary Baseball is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU General 
// Public License for more details.
//
// You should have received a copy of the GNU General Public License 
// along with Binary Baseball. If not, see http://www.gnu.org/licenses/.
// </copyright>
//-----------------------------------------------------------------------

namespace BinaryBaseball.Simulation
{
    using System;
    using System.Collections.Generic;

    using BinaryBaseball.Common;

    /// <summary>
    /// Fatigue manager class.
    /// </summary>
    public class FatigueManager
    {
        /// <summary>
        /// The players.
        /// </summary>
        private Dictionary<String, Fatigue> players = new Dictionary<String, Fatigue>();

        /// <summary>
        /// Handle pre-game fatigue values.
        /// </summary>
        /// <param name="teams">The teams.</param>
        public void PreGame(Team[] teams)
        {
            foreach (var team in teams)
            {
                for (Int32 loopCount = 0; loopCount < team.Players.Count; loopCount++)
                {
                    Fatigue fatigue = null;
                    if (this.players.TryGetValue(team.Players[loopCount].Identifier, out fatigue) == true)
                    {
                        team.Players[loopCount].Fatigue.DaysOff = fatigue.DaysOff;
                        team.Players[loopCount].Fatigue.Streak = fatigue.Streak;
                    }
                }
            }
        }

        /// <summary>
        /// Handles post game fatigue values.
        /// </summary>
        /// <param name="teams">The teams.</param>
        /// <param name="results">The results.</param>
        public void PostGame(Team[] teams, GameResults results)
        {
            for (Int32 loopCount = 0; loopCount < 2; loopCount++)
            {
                var pitchers = results.Pitchers[loopCount];
                Boolean starter = true;

                foreach (var pitcher in pitchers)
                {
                    // Get the pitcher's fatigue
                    Fatigue fatigue = this.GetPlayerFatigue(pitcher.Identifier);

                    // Increment streak
                    fatigue.Streak++;

                    // Find pitcher's gane stats
                    var stats = results.GetPitchersStats(pitcher.Identifier);
                    if (stats != null)
                    {
                        Double daysOff = 0;
                        Double inningsPitched = stats.TotalOuts / 3.0;
                        if (starter)
                        {
                            daysOff = Math.Sqrt((inningsPitched * 3) / 4) + 1;                            
                        }
                        else
                        {
                            daysOff = Math.Sqrt(inningsPitched * 4) - 1.4;
                        }

                        daysOff = Math.Floor(daysOff);

                        fatigue.DaysOff = (Int32)daysOff;
                    }

                    if (fatigue.Streak >= 3)
                    {
                        fatigue.DaysOff++;
                    }          

                    starter = false;
                }

                teams[loopCount].Players.SortByStat(StatToSort.Innings);
                foreach (var player in teams[loopCount].Players)
                {
                    if (player.PitchingStats.Innings > 0)
                    {
                        if (pitchers.Contains(player) == false)
                        {
                            // Get the pitcher's fatigue
                            Fatigue fatigue = this.GetPlayerFatigue(player.Identifier);
                            fatigue.Streak = 0;
                            if (fatigue.DaysOff > 0)
                            {
                                fatigue.DaysOff--;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the player fatigue.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The fatigue instance.</returns>
        private Fatigue GetPlayerFatigue(String id)
        {
            Fatigue fatigue = null;
            if (this.players.TryGetValue(id, out fatigue) == false)
            {
                fatigue = new Fatigue();
                this.players.Add(id, fatigue);
            }
            else
            {
            }

            return fatigue;
        }
    }
}
