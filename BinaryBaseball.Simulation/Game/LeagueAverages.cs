//-----------------------------------------------------------------------
// <copyright file="LeagueAverages.cs" company="Brett Beard">
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

    /// <summary>
    /// League averages class.
    /// </summary>
    public class LeagueAverages
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeagueAverages"/> class.
        /// </summary>
        public LeagueAverages()
        {
            Int32 battersFaced = (43613 * 3) + 41595 + 14020;
            this.Walks = 14020.0 / (Double)battersFaced;
            this.Singles = 28423.0 / (Double)battersFaced;
            this.Doubles = 8137.0 / (Double)battersFaced;
            this.Triples = 849.0 / (Double)battersFaced;
            this.Homeruns = 4186.0 / (Double)battersFaced;
            this.Strikeouts = 37441.0 / (Double)battersFaced;
        }

        /// <summary>
        /// Gets or sets the walks.
        /// </summary>
        /// <value>
        /// The walks.
        /// </value>
        public Double Walks { get; set; }

        /// <summary>
        /// Gets or sets the singles.
        /// </summary>
        /// <value>
        /// The singles.
        /// </value>
        public Double Singles { get; set; }

        /// <summary>
        /// Gets or sets the doubles.
        /// </summary>
        /// <value>
        /// The doubles.
        /// </value>
        public Double Doubles { get; set; }

        /// <summary>
        /// Gets or sets the triples.
        /// </summary>
        /// <value>
        /// The triples.
        /// </value>
        public Double Triples { get; set; }

        /// <summary>
        /// Gets or sets the homeruns.
        /// </summary>
        /// <value>
        /// The homeruns.
        /// </value>
        public Double Homeruns { get; set; }

        /// <summary>
        /// Gets or sets the strikeouts.
        /// </summary>
        /// <value>
        /// The strikeouts.
        /// </value>
        public Double Strikeouts { get; set; }
    }
}
