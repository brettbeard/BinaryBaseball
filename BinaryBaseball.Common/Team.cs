//-----------------------------------------------------------------------
// <copyright file="Team.cs" company="Brett Beard">
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
    using System.Collections.ObjectModel;

    /// <summary>
    /// Team class.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// The players variable.
        /// </summary>
        private Players players = new Players();

        /// <summary>
        /// The default lineup.
        /// </summary>
        private Collection<LineupItem> defaultLineup = new Collection<LineupItem>();

        /// <summary>
        /// The starting rotation.
        /// </summary>
        private Collection<RotationItem> rotation = new Collection<RotationItem>();

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public String Identifier { get; set; }

        /// <summary>
        /// Gets or sets the team's name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public Players Players
        {
            get
            {
                return this.players;
            }
        }

        /// <summary>
        /// Gets the default lineup.
        /// </summary>
        /// <value>
        /// The default lineup.
        /// </value>
        public Collection<LineupItem> DefaultLineup
        {
            get
            {
                return this.defaultLineup;
            }
        }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public Collection<RotationItem> Rotation
        {
            get
            {
                return this.rotation;
            }
        }        
    }

    /// <summary>
    /// Lineup item class.
    /// </summary>
    public class LineupItem
    {
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Int32 Position { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public Int32 Order { get; set; }
    }

    /// <summary>
    /// Rotation item class.
    /// </summary>
    public class RotationItem
    {
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public Int32 Order { get; set; }
    }
}
