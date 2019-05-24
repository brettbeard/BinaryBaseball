//-----------------------------------------------------------------------
// <copyright file="SeasonResults.cs" company="Brett Beard">
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

    /// <summary>
    /// Season results class.
    /// </summary>
    public class SeasonResults
    {
        /// <summary>
        /// The total runs.
        /// </summary>
        private Int32 totalRuns = 0;

        /// <summary>
        /// The total hits.
        /// </summary>
        private Int32 totalHits = 0;

        /// <summary>
        /// The extra inning games.
        /// </summary>
        private Int32 extraInningGames = 0;

        /// <summary>
        /// The games played.
        /// </summary>
        private Int32 gamesPlayed = 0;

        /// <summary>
        /// The team results dictionary.
        /// </summary>
        private Dictionary<String, TeamResults> teamResultsDictionary = new Dictionary<string, TeamResults>();

        /// <summary>
        /// Gets or sets the total runs.
        /// </summary>
        /// <value>
        /// The total runs.
        /// </value>
        public Int32 TotalRuns
        {
            get
            {
                return this.totalRuns;
            }

            set
            {
                this.totalRuns = value;
            }
        }

        /// <summary>
        /// Gets or sets the extra inning games.
        /// </summary>
        /// <value>
        /// The extra inning games.
        /// </value>
        public Int32 ExtraInningGames
        {
            get
            {
                return this.extraInningGames;
            }

            set
            {
                this.extraInningGames = value;
            }
        }

        /// <summary>
        /// Gets or sets the games played.
        /// </summary>
        /// <value>
        /// The games played.
        /// </value>
        public Int32 GamesPlayed
        {
            get
            {
                return this.gamesPlayed;
            }

            set
            {
                this.gamesPlayed = value;
            }
        }

        /// <summary>
        /// Gets or sets the total hits.
        /// </summary>
        /// <value>
        /// The total hits.
        /// </value>
        public Int32 TotalHits
        {
            get
            {
                return this.totalHits;
            }

            set
            {
                this.totalHits = value;
            }
        }

        /// <summary>
        /// Gets the team results dictionary.
        /// </summary>
        /// <value>
        /// The team results dictionary.
        /// </value>
        public Dictionary<String, TeamResults> TeamResultsDictionary
        {
            get
            {
                return this.teamResultsDictionary;
            }
        }

        /// <summary>
        /// Updates the specified results.
        /// </summary>
        /// <param name="results">The results.</param>
        public void Update(GameResults results)
        {
            this.gamesPlayed++;

            this.totalRuns += results.ScoreBoard.HomeScore;
            this.totalRuns += results.ScoreBoard.VisitorScore;

            if ((results.ScoreBoard.Inning - 1) != 9)
            {
                this.extraInningGames++;
            }

            foreach (var team in results.PlayerStatsDictionary)
            {
                foreach (var key in team.Keys)
                {
                    var player = team[key];
                    this.TotalHits += player.BattingStats.Hits;
                }
            }
        }
    }
}
