//-----------------------------------------------------------------------
// <copyright file="BattingStats.cs" company="Brett Beard">
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
    /// Batting stats class.
    /// </summary>
    public class BattingStats
    {
        /// <summary>
        /// Gets or sets at bats.
        /// </summary>
        /// <value>
        /// At bats.
        /// </value>
        public Int32 AtBats { get; set; }

        /// <summary>
        /// Gets or sets the hits.
        /// </summary>
        /// <value>
        /// The hits.
        /// </value>
        public Int32 Hits { get; set; }

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
        /// Gets or sets the stolen bases.
        /// </summary>
        /// <value>
        /// The stolen bases.
        /// </value>
        public Int32 StolenBases { get; set; }

        /// <summary>
        /// Gets or sets the caught stealing.
        /// </summary>
        /// <value>
        /// The caught stealing.
        /// </value>
        public Int32 CaughtStealing { get; set; }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static BattingStats operator +(BattingStats left, BattingStats right)
        {
            var stats = new BattingStats();
            stats.AtBats = left.AtBats + right.AtBats;
            stats.Hits = left.Hits + right.Hits;
            stats.Doubles = left.Doubles + right.Doubles;
            stats.Triples = left.Triples + right.Triples;
            stats.Homeruns = left.Homeruns + right.Homeruns;
            stats.Walks = left.Walks + right.Walks;
            stats.Strikeouts = left.Strikeouts + right.Strikeouts;
            stats.StolenBases = left.StolenBases + right.StolenBases;
            stats.CaughtStealing = left.CaughtStealing + right.CaughtStealing;

            return stats;
        }
    }
}
