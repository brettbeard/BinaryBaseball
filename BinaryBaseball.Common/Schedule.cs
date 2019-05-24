//-----------------------------------------------------------------------
// <copyright file="Schedule.cs" company="Brett Beard">
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
    using System.Collections.ObjectModel;

    /// <summary>
    /// Schedule class.
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// The games collection.
        /// </summary>
        private Collection<ScheduledGame> games = new Collection<ScheduledGame>();

        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <value>
        /// The games.
        /// </value>
        public Collection<ScheduledGame> Games
        {
            get
            {
                return this.games;
            }            
        }
    }

    /// <summary>
    /// Scheduled game class.
    /// </summary>
    public class ScheduledGame
    {
        /// <summary>
        /// Gets or sets the when.
        /// </summary>
        /// <value>
        /// The when.
        /// </value>
        public DateTime When { get; set; }

        /// <summary>
        /// Gets or sets the visitor identifier.
        /// </summary>
        /// <value>
        /// The visitor identifier.
        /// </value>
        public String VisitorIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the home identifier.
        /// </summary>
        /// <value>
        /// The home identifier.
        /// </value>
        public String HomeIdentifier { get; set; }
    }
}
