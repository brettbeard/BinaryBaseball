//-----------------------------------------------------------------------
// <copyright file="Fatigue.cs" company="Brett Beard">
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

namespace BinaryBaseball.Common
{
    using System;

    /// <summary>
    /// Fatigue class.
    /// </summary>
    public class Fatigue
    {
        /// <summary>
        /// Gets or sets the days off.
        /// </summary>
        /// <value>
        /// The days off.
        /// </value>
        public Int32 DaysOff { get; set; }

        /// <summary>
        /// Gets or sets the streak.
        /// </summary>
        /// <value>
        /// The streak.
        /// </value>
        public Int32 Streak { get; set; }
    }
}
