//-----------------------------------------------------------------------
// <copyright file="FieldingStart.cs" company="Brett Beard">
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Fielding start class.
    /// </summary>
    public class FieldingStart
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        [XmlAttribute("position")]
        public Int32 Position { get; set; }

        /// <summary>
        /// Gets or sets the starts.
        /// </summary>
        /// <value>
        /// The starts.
        /// </value>
        public Int32 Starts { get; set; }
    }

    /// <summary>
    /// Fielding starts collection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.Collection{BinaryBaseball.Common.FieldingStart}" />
    public class FieldingStarts : Collection<FieldingStart>
    {
        /// <summary>
        /// Finds the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The Fielding start for the specified position.</returns>
        public FieldingStart Find(Int32 position)
        {
            var items = (List<FieldingStart>)this.Items;
            return items.Find(item => item.Position == position);
        }
    }
}
