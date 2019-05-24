//-----------------------------------------------------------------------
// <copyright file="Manager.cs" company="Brett Beard">
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
    using System.Linq;

    using BinaryBaseball.Common;    

    /// <summary>
    /// Manager class.
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// The team associated with this manager.
        /// </summary>
        private Team team;

        /// <summary>
        /// The lineup.
        /// </summary>
        private Lineup lineup = new Lineup();

        /// <summary>
        /// The bench.
        /// </summary>
        private Players bench;

        /// <summary>
        /// The bullpen.
        /// </summary>
        private Players bullpen;

        /// <summary>
        /// The random.
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// The starting pitcher.
        /// </summary>
        private Boolean startingPitcher = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="team">The team.</param>
        public Manager(Team team)
        {            
            this.team = team;

            // Copy players to bench
            this.bench = new Players();
            foreach (var player in this.team.Players)
            {
                this.bench.Add(player);
            }            
        }

        /// <summary>
        /// Gets the lineup.
        /// </summary>
        /// <value>
        /// The lineup.
        /// </value>
        public Lineup Lineup
        {
            get
            {
                return this.lineup;
            }
        }

        /// <summary>
        /// Determines the starting pitcher.
        /// </summary>
        /// <param name="rotationNumber">The rotation number.</param>
        public void DetermineStartingPitcher(Int32 rotationNumber)
        {
            var rotationPitcher = this.team.Rotation[rotationNumber].Player;

            // Remove rotation starters from bench
            foreach (var item in this.team.Rotation)
            {
                this.bench.Remove(item.Player);
            }

            // Sort players by games started
            this.team.Players.SortByStat(StatToSort.GamesStarted);            

            var potentialStarters = new Players();

            // Tabulate total games started
            Int32 totalGamesStarted = 0;
            Int32 totalGamesStartedNonRotation = 0;
            foreach (var player in this.team.Players)
            {
                // Has player started in games?
                if (player.PitchingStats.GamesStarted == 0)
                {                    
                    break;
                }

                // Add games started to total
                totalGamesStarted += player.PitchingStats.GamesStarted;

                // Is this a rotation pitcher?
                Boolean isRotationPitcher = false;
                foreach (var item in this.team.Rotation)
                {
                    if (item.Player.Equals(player))
                    {
                        isRotationPitcher = true;
                        break;
                    }
                }

                if (isRotationPitcher == false)
                {
                    if (player.Fatigue.DaysOff == 0)
                    {
                        totalGamesStartedNonRotation += player.PitchingStats.GamesStarted;
                        potentialStarters.Add(player);
                    }                    
                }
            }

            // Use spot starter?            
            var rotationStartPercent = ((Double)rotationPitcher.PitchingStats.GamesStarted / (Double)totalGamesStarted) * this.team.Rotation.Count;
            var number = this.random.NextDouble();
            if (number > rotationStartPercent || rotationPitcher.Fatigue.DaysOff > 0)
            {
                // Pick spot starter
                var potentialDictionary = new Dictionary<Player, Int32>();
                Int32 totalPercent = 0;
                foreach (var potential in potentialStarters)
                {                       
                    // Get the # of games this player started at the current position
                    var starts = potential.PitchingStats.GamesStarted;

                    // Determine percent of games this player played (at this position)
                    var percent = (Int32)Math.Round(starts == 0 ? 0 :
                            ((double)starts / (double)totalGamesStartedNonRotation * 100));

                    totalPercent += percent;

                    potentialDictionary.Add(potential, totalPercent);
                }

                var randomValue = this.random.Next(0, 99);
                foreach (var key in potentialDictionary.Keys)
                {
                    var percent = potentialDictionary[key];

                    if (randomValue <= percent)
                    {
                        this.lineup.Pitcher = key;
                        this.bench.Remove(key);

                        if (rotationPitcher.PitchingStats.Games != rotationPitcher.PitchingStats.GamesStarted)
                        {
                            this.bench.Add(rotationPitcher);
                        }

                        break;
                    }
                }
            }
            else
            {
                // Use rotation pitcher
                this.lineup.Pitcher = rotationPitcher;                
            }            

            // Determine bullpen
            this.bullpen = new Players();
            this.bench.SortByStat(StatToSort.GamesStarted);
            
            foreach (var player in this.bench)
            {                
                // Is the player a pitcher?
                if (player.PitchingStats.Games > 0)
                {
                    // Is the pitcher tired?
                    if (player.Fatigue.DaysOff == 0)
                    {
                        this.bullpen.Add(player);
                    }
                }
            }
        }

        /// <summary>
        /// Create starting lineup.
        /// </summary>
        /// <param name="opposingPitcher">The opposing pitcher.</param>
        public void DetermineStartingLineup(Player opposingPitcher)
        {
            // Initialize integer array
            Int32[] randomOrder = new Int32[9] { 0, 1, 2, 3, 4, 5, 6, 7, 0 };

            // Shuffle it up
            for (Int32 loopCount = 0; loopCount < 20; loopCount++)
            {
                var first = this.random.Next(0, 7);
                var second = this.random.Next(0, 7);

                // Swap
                var temp = randomOrder[first];
                randomOrder[first] = randomOrder[second];
                randomOrder[second] = temp;
            }            
            
            // Iterate through each lineup slot
            for (Int32 loopCount = 0; loopCount < 8; loopCount++)
            {
                // Get the next slot
                var slot = this.team.DefaultLineup[randomOrder[loopCount]];

                // Temporary hack
                if (slot.Position == 10)
                {
                    this.lineup.BattingOrder[slot.Order - 1] = slot.Player;

                    // Remove player from available list
                    this.bench.Remove(slot.Player);
                    continue;
                }
                //// Temporary hack

                // Determine the starter
                var starter = this.DetermineStarter(slot);

                // Assign starter to batting order
                this.lineup.BattingOrder[slot.Order - 1] = starter;
                
                // Remove player from available list
                this.bench.Remove(starter);
            }          

            // TBD: Check for DH
            this.lineup.BattingOrder[8] = this.lineup.Pitcher;                     
        }

        /// <summary>
        /// Handle a pitching change.
        /// </summary>
        /// <param name="pitcher">The pitcher.</param>
        /// <param name="stats">The stats.</param>
        /// <param name="scoreboard">The scoreboard.</param>
        /// <param name="isHome">if set to <c>true</c> [is home].</param>
        /// <returns>True if a pitching change was made otherwise false.</returns>
        public Boolean PitchingChange(Player pitcher, PitchingStats stats, ScoreBoard scoreboard, Boolean isHome)
        {
            Boolean newPitcher = false;

            if (this.bullpen.Count > 0)
            {
                // Has the pitcher already been removed?
                if (this.lineup.Pitcher == null)
                {
                    var reliefPitcher = this.SelectReliefPitcher(scoreboard, isHome);
                    this.RelievePitcher(reliefPitcher);
                    this.lineup.BattingOrder[8] = reliefPitcher;
                    newPitcher = true;
                }
                else
                {
                    var pitchCount = Utilities.GetPitchCount(stats);
                    var expectedPitchCount = Utilities.CalculateExpectedPitchCount(pitcher.PitchingStats, this.startingPitcher);

                    if (pitchCount > expectedPitchCount)
                    {
                        // Pitcher is out of gas - select reliever
                        var reliefPitcher = this.SelectReliefPitcher(scoreboard, isHome);

                        this.RelievePitcher(reliefPitcher);
                        newPitcher = true;
                    }
                }
            }

            return newPitcher;
        }        

        /// <summary>
        /// Pinches the hitter.
        /// </summary>
        /// <param name="batter">The batter.</param>
        /// <param name="scoreboard">The scoreboard.</param>
        /// <param name="bases">The bases.</param>
        /// <param name="isHome">if set to <c>true</c> [is home].</param>
        /// <returns>The pinch hitter.</returns>
        public Player PinchHitter(Player batter, ScoreBoard scoreboard, Bases bases, Boolean isHome)
        {
            Player pinchHitter = null;

            Int32 ourScore = 0;
            Int32 theirScore = 0;

            if (isHome)
            {
                ourScore = scoreboard.HomeScore;
                theirScore = scoreboard.VisitorScore;
            }
            else
            {
                ourScore = scoreboard.VisitorScore;
                theirScore = scoreboard.HomeScore;
            }

            Int32 runsBehind = 0;
            if (theirScore > ourScore)
            {
                runsBehind = theirScore - ourScore;
            }

            Int32 numberOfRunners = 0;
            for (Int32 baseNumber = 1; baseNumber < 4; baseNumber++)
            {
                if (bases.Occupied[baseNumber] != null)
                {
                    numberOfRunners++;
                }
            }

            Int32 number = runsBehind + scoreboard.Inning + numberOfRunners - scoreboard.Outs;
            if (number > 7)
            {
                // Is the pitcher batting?
                if (batter.Equals(this.lineup.Pitcher))
                {
                    pinchHitter = this.SelectPinchHitter();

                    for (Int32 slot = 0; slot < 9; slot++)
                    {
                        if (this.lineup.BattingOrder[slot].Equals(batter))
                        {
                            this.lineup.BattingOrder[slot] = pinchHitter;
                            break;
                        }
                    }

                    this.lineup.Pitcher = null;                    
                }                
            }

            if (pinchHitter != null)
            {
                this.bench.Remove(pinchHitter);
            }

            return pinchHitter;
        }

        /// <summary>
        /// Selects the relief pitcher.
        /// </summary>
        /// <param name="scoreboard">The scoreboard.</param>
        /// <param name="isHome">if set to <c>true</c> [is home].</param>
        /// <returns>The relief pitcher.</returns>
        private Player SelectReliefPitcher(ScoreBoard scoreboard, Boolean isHome)
        {
            Player pitcher = null;

            Int32 ourScore = 0;
            Int32 theirScore = 0;

            if (isHome)
            {
                ourScore = scoreboard.HomeScore;
                theirScore = scoreboard.VisitorScore;
            }
            else
            {
                ourScore = scoreboard.VisitorScore;
                theirScore = scoreboard.HomeScore;
            }

            // Time for the closer?
            Int32 scoreDifference = ourScore - theirScore;
            if (scoreboard.Inning > 7 && scoreDifference >= 0 && scoreDifference < 4)
            {
                this.bullpen.SortByStat(StatToSort.Saves);
                pitcher = this.bullpen[0];
            }
            else
            {
                // Get total reliefInnings
                Double totalReliefInnings = 0.0;

                this.bullpen.SortByStat(StatToSort.Saves);
                Boolean first = true;
                foreach (var reliefPitcher in this.bullpen)
                {
                    // Skip closer
                    if (first)
                    {
                        first = false;
                        continue;
                    }

                    // Skip tired pitchers
                    if (reliefPitcher.Fatigue.DaysOff > 0)
                    {
                        continue;
                    }

                    totalReliefInnings += (reliefPitcher.PitchingStats.Games - reliefPitcher.PitchingStats.GamesStarted) * 1.7;
                }

                var potentialDictionary = new Dictionary<Player, Int32>();
                Int32 totalPercent = 0;
                first = true;
                foreach (var reliefPitcher in this.bullpen)
                {
                    // Skip closer
                    if (first)
                    {
                        first = false;
                        continue;
                    }

                    // Skip tired pitchers
                    if (reliefPitcher.Fatigue.DaysOff > 0)
                    {
                        continue;
                    }

                    Double reliefInnings = (reliefPitcher.PitchingStats.Games - reliefPitcher.PitchingStats.GamesStarted) * 1.7;

                    // Determine percent of games this player played (at this position)
                    var percent = (Int32)Math.Round(reliefInnings == 0 ? 0 :
                            ((double)reliefInnings / (double)totalReliefInnings * 100));
                    if (percent > 0)
                    {
                        totalPercent += percent;
                        potentialDictionary.Add(reliefPitcher, totalPercent);
                    }
                }

                var randomValue = this.random.Next(0, 99);
                foreach (var key in potentialDictionary.Keys)
                {
                    var percent = potentialDictionary[key];

                    if (randomValue <= percent)
                    {
                        pitcher = key;
                        break;
                    }
                    else
                    {
                    }
                }
            }

            return pitcher;
        }

        /// <summary>
        /// Selects the pinch hitter.
        /// </summary>
        /// <returns>The pinch hitter.</returns>
        private Player SelectPinchHitter()
        {
            Player pinchHitter = null;

            this.bench.SortByStat(StatToSort.Batter_Hits);

            pinchHitter = this.bench[0];

            return pinchHitter;
        }

        /// <summary>
        /// Relieves the pitcher.
        /// </summary>
        /// <param name="pitcher">The pitcher.</param>
        private void RelievePitcher(Player pitcher)
        {
            this.startingPitcher = false;

            // Update the lineup
            this.lineup.Pitcher = pitcher;

            // Remove from bullpen
            this.bullpen.Remove(pitcher);

            // Remove from bench
            this.bench.Remove(pitcher);
        }

        /// <summary>
        /// Determine starter for specified lineup slot.
        /// </summary>
        /// <param name="slot">The slot.</param>
        /// <returns>
        /// The starter.
        /// </returns>
        private Player DetermineStarter(LineupItem slot)
        {
            Player starter = null;

            // Get all starters for the position
            var potentialStarters = this.bench.GetStartersForPosition(slot.Position);

            // Get the total starts for all players
            Int32 totalStarts = (from player in potentialStarters
                                 select player.FieldingStarts.Find(slot.Position).Starts).Sum();

            // Build a dictionary with player and their starting percentage
            var potentialDictionary = new Dictionary<Player, Int32>();
            Int32 totalPercent = 0;
            foreach (var potential in potentialStarters)
            {
                // Get the # of games this player started at the current position
                var fieldingStart = potential.FieldingStarts.Find(slot.Position);

                // Determine percent of games this player played (at this position)
                var percent = (Int32)Math.Round(fieldingStart.Starts == 0 ? 0 :
                        ((double)fieldingStart.Starts / (double)totalStarts * 100));

                totalPercent += percent;

                potentialDictionary.Add(potential, totalPercent);
            }

            // Pick a random number
            var randomValue = this.random.Next(0, 99);

            // See which player was chosen            
            foreach (var key in potentialDictionary.Keys)
            {
                // Get the percent
                var percent = potentialDictionary[key];

                // Is this the player?
                if (randomValue <= percent)
                {
                    // We got our starter
                    starter = key;
                    break;
                }
                else
                {
                }
            }

            return starter;
        }        
    }   
}
