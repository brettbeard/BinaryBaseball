//-----------------------------------------------------------------------
// <copyright file="TeamFile.cs" company="Brett Beard">
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

namespace BinaryBaseball.SBS
{
    using System;
    using System.IO;

    using BinaryBaseball.Common;

    /// <summary>
    /// Team file class.
    /// </summary>
    public class TeamFile
    {
        /// <summary>
        /// Loads the team from a SBS team file.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The team.</returns>
        public static Team Load(Stream stream)
        {
            var team = new Team();
            
            using (StreamReader reader = new StreamReader(stream))
            {
                var fileContents = reader.ReadToEnd();

                String[] lines = fileContents.Split(new String[] { "\r\n", "\n" }, StringSplitOptions.None);
                team = TeamFile.Parse(lines);                
            }

            return team;
        }

        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The team.</returns>
        public static Team Load(String fileName)
        {
            // Read in the data            
            var lines = System.IO.File.ReadAllLines(fileName);

            return TeamFile.Parse(lines);            
        }

        /// <summary>
        /// Parses the specified lines.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <returns>The team.</returns>
        private static Team Parse(String[] lines)
        {
            var team = new Team();            

            // Extract team name (with year)
            team.Name = lines[0].Substring(10, 18).TrimEnd(' ');

            // Add batting order
            for (Int32 loopCounter = 0; loopCounter < 9; loopCounter++)
            {
                if (lines[loopCounter + 1].Length > 8)
                {
                    String text = lines[loopCounter + 1];
                    var player = ParseBatter(text);

                    player.Identifier = String.Format("{0}-{1}-{2}", player.LastName, player.FirstName, team.Name);

                    team.Players.Add(player);

                    // Add to default lineup
                    String position = text.Substring(6, 2);
                    var item = new LineupItem();
                    item.Player = player;
                    item.Order = loopCounter + 1;
                    item.Position = Convert.ToInt32(position);
                    team.DefaultLineup.Add(item);
                }
            }

            // Add pitchers
            Int32 lineIndex = 11;
            Int32 benchIndex = 11;
            Boolean done = false;
            Int32 pitcherOrder = 1;
            while (!done)
            {
                if (lines[lineIndex].StartsWith("*Bench"))
                {
                    benchIndex = lineIndex + 1;
                    done = true;
                }
                else
                {
                    // Add the pitcher
                    var player = ParsePitcher(lines[lineIndex]);
                    player.Identifier = String.Format("{0}-{1}-{2}", player.LastName, player.FirstName, team.Name);
                    team.Players.Add(player);

                    // Add pitcher to rotation?
                    if (pitcherOrder <= 5)
                    {
                        var item = new RotationItem();
                        item.Player = player;
                        item.Order = pitcherOrder;
                        team.Rotation.Add(item);
                    }

                    pitcherOrder++;

                    lineIndex++;
                }
            }

            // Add bench
            done = false;
            while (!done)
            {
                if (lines[benchIndex].StartsWith("*") == false && lines[benchIndex] != String.Empty)
                {
                    var player = ParseBatter(lines[benchIndex]);
                    team.Players.Add(player);
                }

                benchIndex++;

                if (benchIndex >= lines.Length)
                {
                    done = true;
                }
            }

            return team;
        }

        /// <summary>
        /// Parses the batter.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The player.</returns>
        private static Player ParseBatter(String text)
        {
            var player = new Player();
            player.Identifier = Guid.NewGuid().ToString();            

            String buffer = text.Substring(9, 18).TrimEnd(' ');
            var names = buffer.Split(',');

            player.FirstName = names[1].TrimStart(' ');
            player.LastName = names[0];

            buffer = text.Substring(27, 3);
            player.BattingStats.AtBats = Convert.ToInt32(buffer);

            buffer = text.Substring(31, 3);
            player.BattingStats.Hits = Convert.ToInt32(buffer);

            buffer = text.Substring(36, 2);
            player.BattingStats.Doubles = Convert.ToInt32(buffer);

            buffer = text.Substring(40, 2);
            player.BattingStats.Triples = Convert.ToInt32(buffer);

            buffer = text.Substring(44, 2);
            player.BattingStats.Homeruns = Convert.ToInt32(buffer);            

            buffer = text.Substring(48, 2);
            player.BattingStats.Walks = Convert.ToInt32(buffer);

            buffer = text.Substring(51, 3);
            player.BattingStats.Strikeouts = Convert.ToInt32(buffer);

            buffer = text.Substring(59, 1);
            if (buffer.Equals("R"))
            {
                player.Bats = Bats.Right;
            }
            else if (buffer.Equals("L"))
            {
                player.Bats = Bats.Left;
            }
            else if (buffer.Equals("S"))
            {
                player.Bats = Bats.Switch;
            }

            buffer = text.Substring(64, 3);
            player.BattingStats.StolenBases = Convert.ToInt32(buffer);

            buffer = text.Substring(68, 2);
            player.BattingStats.CaughtStealing = Convert.ToInt32(buffer);

            for (Int32 loopCounter = 0; loopCounter < 4; loopCounter++)
            {
                Int32 startingIndex = (loopCounter * 6) + 75;
                if (startingIndex >= text.Length)
                {
                    break;
                }

                if ((startingIndex + 5) <= text.Length)
                {
                    buffer = text.Substring(startingIndex, 5);
                    var split = buffer.Split('-');
                    var fieldingStart = new FieldingStart();
                    if (split[1].ToLower() == "d")
                    {
                        fieldingStart.Position = 10;
                    }
                    else
                    {
                        fieldingStart.Position = Convert.ToInt32(split[1]);
                    }

                    fieldingStart.Starts = Convert.ToInt32(split[0]);
                    player.FieldingStarts.Add(fieldingStart);
                }
                else
                {
                }
            }

            Double factor1 = ((Double)(player.BattingStats.StolenBases + 3) / (Double)(player.BattingStats.StolenBases + player.BattingStats.CaughtStealing + 7) - 0.4) * 20;
            Int32 singles = player.BattingStats.Hits - (player.BattingStats.Doubles + player.BattingStats.Triples + player.BattingStats.Homeruns);
            Double factor2 = Math.Sqrt((Double)((Double)(player.BattingStats.StolenBases + player.BattingStats.CaughtStealing) / (Double)(singles + player.BattingStats.Walks))) / 0.07;            
            Double factor3 = (Double)player.BattingStats.Triples / (Double)(player.BattingStats.AtBats - (player.BattingStats.Homeruns + player.BattingStats.Strikeouts)) / 0.02 * 10;
            var speedRating = ((factor1 * 10) + (factor2 * 10) + (factor3 * 6)) / 26;

            player.Speed = Convert.ToInt32(Math.Floor(speedRating));

            return player;
        }

        /// <summary>
        /// Parses the pitcher.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The player.</returns>
        private static Player ParsePitcher(String text)
        {
            var player = new Player();
            player.Identifier = Guid.NewGuid().ToString();

            String buffer = text.Substring(9, 18).TrimEnd(' ');
            var names = buffer.Split(',');

            player.FirstName = names[1];
            player.LastName = names[0];            

            buffer = text.Substring(71, 3);
            player.PitchingStats.Games = Convert.ToInt32(buffer);

            buffer = text.Substring(75, 3);
            player.PitchingStats.GamesStarted = Convert.ToInt32(buffer);

            // Pitching stats
            buffer = text.Substring(27, 3);
            player.PitchingStats.Innings = Convert.ToInt32(buffer);

            buffer = text.Substring(31, 3);
            player.PitchingStats.Hits = Convert.ToInt32(buffer);

            buffer = text.Substring(44, 2);
            player.PitchingStats.Homeruns = Convert.ToInt32(buffer);

            buffer = text.Substring(48, 2);
            player.PitchingStats.Walks = Convert.ToInt32(buffer);

            buffer = text.Substring(51, 3);
            player.PitchingStats.Strikeouts = Convert.ToInt32(buffer);

            player.PitchingStats.BattersFaced = (player.PitchingStats.Innings * 3) + player.PitchingStats.Hits + player.PitchingStats.Walks;

            player.PitchingStats.Triples = (Int32)((Double)player.PitchingStats.Hits * .024);
            player.PitchingStats.Doubles = (Int32)((Double)player.PitchingStats.Hits * .174);
            player.PitchingStats.Singles = player.PitchingStats.Hits - (player.PitchingStats.Doubles + player.PitchingStats.Triples + player.PitchingStats.Homeruns);

            buffer = text.Substring(67, 3);
            player.PitchingStats.Saves = Convert.ToInt32(buffer);

            if (text.Length > 81)
            {
                // Batting stats
                buffer = text.Substring(82, 3);
                player.BattingStats.AtBats = Convert.ToInt32(buffer);

                buffer = text.Substring(88, 2);
                player.BattingStats.Hits = Convert.ToInt32(buffer);

                buffer = text.Substring(90, 2);
                player.BattingStats.Homeruns = Convert.ToInt32(buffer);

                buffer = text.Substring(94, 2);
                player.BattingStats.Walks = Convert.ToInt32(buffer);

                buffer = text.Substring(98, 2);
                player.BattingStats.Strikeouts = Convert.ToInt32(buffer);
            }

            return player;
        }
    }
}
