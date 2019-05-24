//-----------------------------------------------------------------------
// <copyright file="PitchingStats.cs" company="Brett Beard">
// Copyright 2017 Brett Beard
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

namespace BinaryBaseball.Common
{
    using System;

    /// <summary>
    /// Pitching stats class.
    /// </summary>
    public class PitchingStats
    {
        /// <summary>
        /// Gets or sets the batters faced.
        /// </summary>
        /// <value>
        /// The batters faced.
        /// </value>
        public Int32 BattersFaced { get; set; }

        /// <summary>
        /// Gets or sets the innings.
        /// </summary>
        /// <value>
        /// The innings.
        /// </value>
        public Int32 Innings { get; set; }

        /// <summary>
        /// Gets or sets the hits.
        /// </summary>
        /// <value>
        /// The hits.
        /// </value>
        public Int32 Hits { get; set; }

        /// <summary>
        /// Gets or sets the singles.
        /// </summary>
        /// <value>
        /// The singles.
        /// </value>
        public Int32 Singles { get; set; }

        /// <summary>
        /// Gets or sets the doubles.
        /// </summary>
        /// <value>
        /// The doubles.
        /// </value>
        public Int32 Doubles { get; set; }

        /// <summary>
        /// Gets or sets the triples.
        /// </summary>
        /// <value>
        /// The triples.
        /// </value>
        public Int32 Triples { get; set; }

        /// <summary>
        /// Gets or sets the homeruns.
        /// </summary>
        /// <value>
        /// The homeruns.
        /// </value>
        public Int32 Homeruns { get; set; }

        /// <summary>
        /// Gets or sets the walks.
        /// </summary>
        /// <value>
        /// The walks.
        /// </value>
        public Int32 Walks { get; set; }

        /// <summary>
        /// Gets or sets the strikeouts.
        /// </summary>
        /// <value>
        /// The strikeouts.
        /// </value>
        public Int32 Strikeouts { get; set; }

        /// <summary>
        /// Gets or sets the games.
        /// </summary>
        /// <value>
        /// The games.
        /// </value>
        public Int32 Games { get; set; }

        /// <summary>
        /// Gets or sets the games started.
        /// </summary>
        /// <value>
        /// The games started.
        /// </value>
        public Int32 GamesStarted { get; set; }

        /// <summary>
        /// Gets or sets the total outs.
        /// </summary>
        /// <value>
        /// The total outs.
        /// </value>
        public Int32 TotalOuts { get; set; }

        /// <summary>
        /// Gets or sets the saves.
        /// </summary>
        /// <value>
        /// The saves.
        /// </value>
        public Int32 Saves { get; set; }
    }
}
