//-----------------------------------------------------------------------
// <copyright file="ScoreBoard.cs" company="Brett Beard">
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

namespace BinaryBaseball.Simulation
{
    using System;

    /// <summary>
    /// Scoreboard class.
    /// </summary>
    public class ScoreBoard
    {     
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoard"/> class.
        /// </summary>
        public ScoreBoard()
        {
            this.Inning = 1;
            this.HalfInning = HalfInningEnum.Top;
            this.Outs = 0;
            this.HomeScore = 0;
            this.VisitorScore = 0;
        }

        /// <summary>
        /// Gets or sets the inning.
        /// </summary>
        /// <value>
        /// The inning.
        /// </value>
        public Int32 Inning { get; set; }

        /// <summary>
        /// Gets or sets the half inning.
        /// </summary>
        /// <value>
        /// The half inning.
        /// </value>
        public HalfInningEnum HalfInning { get; set; }

        /// <summary>
        /// Gets or sets the outs.
        /// </summary>
        /// <value>
        /// The outs.
        /// </value>
        public Int32 Outs { get; set; }

        /// <summary>
        /// Gets or sets the home score.
        /// </summary>
        /// <value>
        /// The home score.
        /// </value>
        public Int32 HomeScore { get; set; }

        /// <summary>
        /// Gets or sets the visitor score.
        /// </summary>
        /// <value>
        /// The visitor score.
        /// </value>
        public Int32 VisitorScore { get; set; }
    }
}
