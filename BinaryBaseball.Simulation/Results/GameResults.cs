//-----------------------------------------------------------------------
// <copyright file="GameResults.cs" company="Brett Beard">
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
    /// Game results class.
    /// </summary>
    public class GameResults
    {
        /// <summary>
        /// The pitching stats.
        /// </summary>
        private PitchingStats[] pitchingStats = new PitchingStats[2];

        /// <summary>
        /// The pitchers.
        /// </summary>
        private Players[] pitchers = new Players[2];

        /// <summary>
        /// The pitcher stats dictionary.
        /// </summary>
        private Dictionary<String, PitchingStats> pitcherStatsDictionary = new Dictionary<String, Common.PitchingStats>();

        /// <summary>
        /// The player stats dictionary.
        /// </summary>
        private Dictionary<String, Player>[] playerStatsDictionary = new Dictionary<String, Common.Player>[2];

        /// <summary>
        /// The score board
        /// </summary>
        private ScoreBoard scoreBoard;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameResults"/> class.
        /// </summary>
        public GameResults()
        {
            for (Int32 loopCount = 0; loopCount < 2; loopCount++)
            {
                this.pitchingStats[loopCount] = new PitchingStats();
                this.playerStatsDictionary[loopCount] = new Dictionary<string, Player>();
                this.Pitchers[loopCount] = new Players();
            }
        }

        /// <summary>
        /// Gets or sets the home team identifier.
        /// </summary>
        /// <value>
        /// The home team identifier.
        /// </value>
        public String HomeTeamIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the visitor team identifier.
        /// </summary>
        /// <value>
        /// The visitor team identifier.
        /// </value>
        public String VisitorTeamIdentifier { get; set; }

        /// <summary>
        /// Gets the pitching stats.
        /// </summary>
        /// <value>
        /// The pitching stats.
        /// </value>
        public PitchingStats[] PitchingStats
        {
            get
            {
                return this.pitchingStats;
            }
        }

        /// <summary>
        /// Gets the pitchers.
        /// </summary>
        /// <value>
        /// The pitchers.
        /// </value>
        public Players[] Pitchers
        {
            get
            {
                return this.pitchers;
            }
        }

        /// <summary>
        /// Gets or sets the score board.
        /// </summary>
        /// <value>
        /// The score board.
        /// </value>
        public ScoreBoard ScoreBoard
        {
            get
            {
                return this.scoreBoard;
            }

            set
            {
                this.scoreBoard = value;
            }
        }

        /// <summary>
        /// Gets or sets the player stats dictionary.
        /// </summary>
        /// <value>
        /// The player stats dictionary.
        /// </value>
        public Dictionary<string, Player>[] PlayerStatsDictionary
        {
            get
            {
                return this.playerStatsDictionary;
            }

            set
            {
                this.playerStatsDictionary = value;
            }
        }

        /// <summary>
        /// Handles a pitching change.
        /// </summary>
        /// <param name="newPitcher">The new pitcher.</param>
        /// <param name="isHomeTeam">if set to <c>true</c> [is home team].</param>
        public void HandlePitchingChange(Player newPitcher, Boolean isHomeTeam)
        {
            // Figure out which team we're dealing with
            Int32 teamIndex = Game.VisitorTeam;
            if (isHomeTeam)
            {
                teamIndex = Game.HomeTeam;
            }

            // Get the ID for the previous pitcher
            String id = this.pitchers[teamIndex][this.pitchers[teamIndex].Count - 1].Identifier;

            // Add previous pitcher stats to dictionary            
            this.pitcherStatsDictionary.Add(id, this.pitchingStats[teamIndex]);

            // Add the new pitcher to the list
            this.pitchers[teamIndex].Add(newPitcher);

            // Reset the stats
            this.pitchingStats[teamIndex] = new Common.PitchingStats();
        }

        /// <summary>
        /// Handles the end of a game.
        /// </summary>
        public void HandleEndOfGame()
        {
            for (Int32 loopCount = 0; loopCount < 2; loopCount++)
            {
                String id = this.pitchers[loopCount][this.pitchers[loopCount].Count - 1].Identifier;
                this.pitcherStatsDictionary.Add(id, this.pitchingStats[loopCount]);
            }            
        }

        /// <summary>
        /// Gets the pitchers stats.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The pitching stats.</returns>
        public PitchingStats GetPitchersStats(String id)
        {
            PitchingStats stats = null;

            this.pitcherStatsDictionary.TryGetValue(id, out stats);

            return stats;
        }
    }
}
