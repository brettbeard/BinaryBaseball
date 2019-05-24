//-----------------------------------------------------------------------
// <copyright file="Bases.cs" company="Brett Beard">
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
    using BinaryBaseball.Common;

    /// <summary>
    /// Base states enumerated type.
    /// </summary>
    public enum BaseStates
    {
        /// <summary>
        /// No runners.
        /// </summary>
        Empty = 0,

        /// <summary>
        /// Runner on first.
        /// </summary>
        First = 1,

        /// <summary>
        /// Runner on second.
        /// </summary>
        Second = 10,

        /// <summary>
        /// Runners on first and second.
        /// </summary>
        FirstAndSecond = 11,

        /// <summary>
        /// Runner on third.
        /// </summary>
        Third = 100,

        /// <summary>
        /// Runners on first and third.
        /// </summary>
        FirstAndThird = 101,

        /// <summary>
        /// Runners on second and third.
        /// </summary>
        SecondAndThird = 110,

        /// <summary>
        /// Bases jammed.
        /// </summary>
        Loaded = 111
    }

    /// <summary>
    /// Bases class.
    /// </summary>
    public class Bases
    {
        /// <summary>
        /// The occupied variable.
        /// </summary>
        private Player[] occupied;        

        /// <summary>
        /// Initializes a new instance of the <see cref="Bases"/> class.
        /// </summary>
        public Bases()
        {
            this.occupied = new Player[4];            
        }        

        /// <summary>
        /// Gets a value indicating whether this <see cref="Bases"/> is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if empty; otherwise, <c>false</c>.
        /// </value>
        public bool Empty
        {
            get
            {
                bool basesEmpty = true;

                int baseNumber = 0;
                while (baseNumber < 4 && basesEmpty)
                {
                    if (this.occupied[baseNumber] != null)
                    {
                        basesEmpty = false;
                    }

                    baseNumber++;
                }

                return basesEmpty;
            }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public BaseStates State
        {
            get
            {
                int state = 0;

                if (this.occupied[1] != null)
                {
                    state += 1;
                }

                if (this.occupied[2] != null)
                {
                    state += 10;
                }

                if (this.occupied[3] != null)
                {
                    state += 100;
                }

                return (BaseStates)state;
            }
        }

        /// <summary>
        /// Gets or sets the occupied.
        /// </summary>
        /// <value>
        /// The occupied.
        /// </value>
        public Player[] Occupied
        {
            get
            {
                return this.occupied;
            }

            set
            {
                this.occupied = value;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < 4; ++i)
            {
                this.occupied[i] = null;
            }
        }
    }
}
