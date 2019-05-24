//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="Brett Beard">
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
    using BinaryBaseball.Common;

    /// <summary>
    /// Half inning enumerated type.
    /// </summary>
    public enum HalfInningEnum
    {
        /// <summary>
        /// Top of inning.
        /// </summary>
        Top = 0,

        /// <summary>
        /// Bottom of inning.
        /// </summary>
        Bottom
    }

    /// <summary>
    /// Game class.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The home team constant.
        /// </summary>
        public const Int32 HomeTeam = 0;

        /// <summary>
        /// The visitor team constant.
        /// </summary>
        public const Int32 VisitorTeam = 1;

        /// <summary>
        /// The logger.
        /// </summary>
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The game parameters.
        /// </summary>
        private GameParameters parameters;

        /// <summary>
        /// The scoreboard.
        /// </summary>
        private ScoreBoard scoreboard;

        /// <summary>
        /// The game results.
        /// </summary>
        private GameResults gameResults = new GameResults();

        /// <summary>
        /// The random number generator.
        /// </summary>
        private Random random;

        /// <summary>
        /// The game over flag.
        /// </summary>
        private Boolean gameOver = false;

        /// <summary>
        /// The bases.
        /// </summary>
        private Bases bases;       

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public Game(GameParameters parameters)
        {
            this.parameters = parameters;
        }

        /// <summary>
        /// Occurs when [announce].
        /// </summary>
        public event EventHandler<String> Announce;

        /// <summary>
        /// Gets the next batter.
        /// </summary>
        /// <value>
        /// The next batter.
        /// </value>
        private Player NextBatter
        {
            get
            {
                Player batter;

                // Get the next batter
                if (this.scoreboard.HalfInning == HalfInningEnum.Top)
                {
                    batter = this.parameters.Managers[Game.VisitorTeam].Lineup.BattingOrder.NextBatter();
                }
                else
                {
                    batter = this.parameters.Managers[Game.HomeTeam].Lineup.BattingOrder.NextBatter();
                }

                return batter;
            }
        }

        /// <summary>
        /// Gets the current pitcher.
        /// </summary>
        /// <value>
        /// The pitcher.
        /// </value>
        private Player Pitcher
        {
            get
            {
                Player pitcher;

                // Get the current pitcher
                if (this.scoreboard.HalfInning == HalfInningEnum.Bottom)
                {
                    pitcher = this.parameters.Managers[Game.VisitorTeam].Lineup.Pitcher;
                }
                else
                {
                    pitcher = this.parameters.Managers[Game.HomeTeam].Lineup.Pitcher;
                }

                return pitcher;
            }
        }

        /// <summary>
        /// Gets the pitcher statistics.
        /// </summary>
        /// <value>
        /// The pitcher statistics.
        /// </value>
        private PitchingStats PitcherStatistics
        {
            get
            {
                PitchingStats stats;

                // Get the current pitcher
                if (this.scoreboard.HalfInning == HalfInningEnum.Bottom)
                {
                    stats = this.gameResults.PitchingStats[Game.VisitorTeam];
                }
                else
                {
                    stats = this.gameResults.PitchingStats[Game.HomeTeam];                    
                }

                return stats;
            }
        }

        /// <summary>
        /// Plays the game.
        /// </summary>
        /// <returns>The game results.</returns>
        public GameResults PlayBall()
        {
            logger.Debug("PlayBall Start");

            // Initialization
            this.scoreboard = new ScoreBoard();
            this.random = new Random();
            this.bases = new Bases();
            this.gameResults.HomeTeamIdentifier = this.parameters.Teams[Game.HomeTeam].Identifier;
            this.gameResults.VisitorTeamIdentifier = this.parameters.Teams[Game.VisitorTeam].Identifier;

            // Managers determine starting pitchers and lineup
            this.ManagersPreGame();          

            this.AnnouncerOutput("Play ball!");

            // Play the game
            bool gameOver = false;
            while (!gameOver)
            {
                // Play an inning
                this.PlayInning();

                this.AnnouncerOutput(string.Format("End of inning #{0}.", this.scoreboard.Inning));
                this.AnnouncerOutput(string.Format("{0}: {1} {2}: {3}.", this.parameters.Teams[Game.VisitorTeam].Name, this.scoreboard.VisitorScore, this.parameters.Teams[Game.HomeTeam].Name, this.scoreboard.HomeScore));
                this.AnnouncerOutput("======================================================================");

                this.scoreboard.Inning++;

                // Is the game over?
                if (this.scoreboard.Inning >= 10)
                {
                    // Make sure we're not tied
                    if (this.scoreboard.VisitorScore != this.scoreboard.HomeScore)
                    {
                        gameOver = true;
                    }
                }
            }
            
            this.gameResults.ScoreBoard = this.scoreboard;
            this.gameResults.HandleEndOfGame();

            logger.Debug("PlayBall End");
            return this.gameResults;
        }

        /// <summary>
        /// Managers determine starting pitchers and starting lineups.
        /// </summary>
        private void ManagersPreGame()
        {
            // Managers determine starter pitchers
            this.parameters.Managers[Game.HomeTeam].DetermineStartingPitcher(this.parameters.RotationNumber[Game.HomeTeam]);
            this.parameters.Managers[Game.VisitorTeam].DetermineStartingPitcher(this.parameters.RotationNumber[Game.VisitorTeam]);

            // Add pitchers to game results
            this.gameResults.Pitchers[Game.HomeTeam].Add(this.parameters.Managers[Game.HomeTeam].Lineup.Pitcher);
            this.gameResults.Pitchers[Game.VisitorTeam].Add(this.parameters.Managers[Game.VisitorTeam].Lineup.Pitcher);

            // Managers build lineups
            this.parameters.Managers[Game.HomeTeam].DetermineStartingLineup(this.parameters.Managers[Game.VisitorTeam].Lineup.Pitcher);
            this.parameters.Managers[Game.VisitorTeam].DetermineStartingLineup(this.parameters.Managers[Game.HomeTeam].Lineup.Pitcher);
        }

        /// <summary>
        /// Plays the inning.
        /// </summary>
        private void PlayInning()
        {
            logger.Debug("PlayInning {0} Start", this.scoreboard.Inning);

            Boolean inningOver = false;

            while (inningOver == false)
            {
                this.PlayHalfInning();
                this.AnnouncerOutput(" ");

                if (this.scoreboard.HalfInning == HalfInningEnum.Top)
                {
                    // If the home team is winning in the top of the 9th
                    // then no need to play bottom half of inning - game over
                    if (this.scoreboard.Inning >= 9 && (this.scoreboard.HomeScore > this.scoreboard.VisitorScore))
                    {
                        inningOver = true;
                    }
                    else
                    {
                        this.scoreboard.HalfInning = HalfInningEnum.Bottom;
                    }
                }
                else
                {
                    this.scoreboard.HalfInning = HalfInningEnum.Top;
                    inningOver = true;
                }
            }

            logger.Debug("PlayInning {0} End", this.scoreboard.Inning);
        }

        /// <summary>
        /// Plays the half inning.
        /// </summary>
        private void PlayHalfInning()
        {
            this.scoreboard.Outs = 0;
            this.bases.Clear();

            // Change the pitcher?
            this.ConsiderPitchingChange();            

            // Loop until there are three outs and game is not over (by a walk off)
            while (this.scoreboard.Outs < 3 && this.gameOver == false)
            {
                this.OutputBases();

                Player batter = this.NextBatter;
                Player pitcher = this.Pitcher;

                // Pinch hit situation?
                var pinchHitter = this.ConsiderPinchHitter(batter);
                if (pinchHitter != null)
                {
                    batter = pinchHitter;
                    this.AnnouncerOutput(String.Format("Pinch hitter: {0}", batter.LastName));
                }

                // Steal a base?
                this.ConsiderStolenBase();

                if (this.scoreboard.Outs < 3)
                {
                    // Execute the at bat
                    Int32 preAtBatOuts = this.scoreboard.Outs;
                    var result = this.GetBattingResult(batter, pitcher);

                    this.AnnouncerOutput(string.Format("{0}-{1} Pitcher: {2} Batter: {3} Result: {4}", this.scoreboard.VisitorScore, this.scoreboard.HomeScore, pitcher.LastName, batter.LastName, result.ToString()));

                    this.HandleBaseRunners(result, batter);

                    Int32 outsOnPlay = this.scoreboard.Outs - preAtBatOuts;

                    this.UpdateBatterStatistics(batter, result);
                    this.UpdateStatistics(result);
                }
            }
        }

        /// <summary>
        /// Gets the batting result.
        /// </summary>
        /// <param name="batter">The batter.</param>
        /// <param name="pitcher">The pitcher.</param>
        /// <returns>
        /// The result.
        /// </returns>
        private BattingResult GetBattingResult(Player batter, Player pitcher)
        {
            var result = new BattingResult();

            Boolean isStarter = true;
            if (this.scoreboard.HalfInning == HalfInningEnum.Top)
            {
                if (this.gameResults.Pitchers[Game.HomeTeam].Count > 1)
                {
                    isStarter = false;
                }
            }
            else
            {
                if (this.gameResults.Pitchers[Game.VisitorTeam].Count > 1)
                {
                    isStarter = false;
                }
            }

            // Calculate pitcher's fatigue adjustment            
            var pitchCount = Utilities.GetPitchCount(this.PitcherStatistics);
            var expectedPitchCount = Utilities.CalculateExpectedPitchCount(this.Pitcher.PitchingStats, isStarter);

            var fatigueFactor = (Double)((Double)pitchCount / (Double)expectedPitchCount);
            var fatigueAdjustment = (Double)((0.175 * fatigueFactor) - 0.0965);

            var overallAdjustment = 1 + fatigueAdjustment;

            var dictionary = new System.Collections.Generic.Dictionary<BattingResult, Double>();

            Int32 plateAppearances = batter.BattingStats.AtBats + batter.BattingStats.Walks;

            // Walks            
            Double pitcherPercent = (Double)pitcher.PitchingStats.Walks / (Double)pitcher.PitchingStats.BattersFaced;
            Double batterPercent = (Double)batter.BattingStats.Walks / (Double)plateAppearances;
            Double percent = this.CalculateLog5(batterPercent, pitcherPercent, this.parameters.LeagueAverages.Walks);
            dictionary.Add(BattingResult.Walk, percent);
            Double baseline = percent;

            // Singles
            pitcherPercent = (Double)pitcher.PitchingStats.Singles / (Double)pitcher.PitchingStats.BattersFaced;
            Int32 singles = batter.BattingStats.Hits - (batter.BattingStats.Doubles + batter.BattingStats.Triples + batter.BattingStats.Homeruns);
            batterPercent = (Double)singles / (Double)plateAppearances;
            percent = this.CalculateLog5(batterPercent, pitcherPercent, this.parameters.LeagueAverages.Singles) + baseline;
            percent *= (1 - this.parameters.LeagueAverages.Singles) * (1 - overallAdjustment);
            dictionary.Add(BattingResult.Single, percent);
            baseline = percent;

            // Doubles
            pitcherPercent = (Double)pitcher.PitchingStats.Doubles / (Double)pitcher.PitchingStats.BattersFaced;
            batterPercent = (Double)batter.BattingStats.Doubles / (Double)plateAppearances;
            percent = this.CalculateLog5(batterPercent, pitcherPercent, this.parameters.LeagueAverages.Doubles) + baseline;
            percent *= (1 - this.parameters.LeagueAverages.Doubles) * (1 - overallAdjustment);
            dictionary.Add(BattingResult.Double, percent);
            baseline = percent;

            // Triples
            pitcherPercent = (Double)pitcher.PitchingStats.Triples / (Double)pitcher.PitchingStats.BattersFaced;
            batterPercent = (Double)batter.BattingStats.Triples / (Double)plateAppearances;
            percent = this.CalculateLog5(batterPercent, pitcherPercent, this.parameters.LeagueAverages.Triples) + baseline;
            percent *= (1 - this.parameters.LeagueAverages.Triples) * (1 - overallAdjustment);
            dictionary.Add(BattingResult.Triple, percent);
            baseline = percent;

            // Homeruns
            pitcherPercent = (Double)pitcher.PitchingStats.Homeruns / (Double)pitcher.PitchingStats.BattersFaced;
            batterPercent = (Double)batter.BattingStats.Homeruns / (Double)plateAppearances;
            percent = this.CalculateLog5(batterPercent, pitcherPercent, this.parameters.LeagueAverages.Homeruns) + baseline;
            percent *= (1 - this.parameters.LeagueAverages.Homeruns) * (1 - overallAdjustment);
            dictionary.Add(BattingResult.Homerun, percent);
            baseline = percent;

            // Strikeouts        
            pitcherPercent = (Double)pitcher.PitchingStats.Strikeouts / (Double)pitcher.PitchingStats.BattersFaced;
            batterPercent = (Double)batter.BattingStats.Strikeouts / (Double)plateAppearances;
            percent = this.CalculateLog5(batterPercent, pitcherPercent, this.parameters.LeagueAverages.Strikeouts) + baseline;
            dictionary.Add(BattingResult.Strikeout, percent);
            baseline = percent;

            // Get a random number            
            Double randomeValue = this.random.NextDouble();

            result = BattingResult.Out;
            foreach (var key in dictionary.Keys)
            {
                percent = dictionary[key];
                if (randomeValue < percent)
                {
                    result = key;
                    break;
                }
            }

            if (result == BattingResult.Out)
            {
                randomeValue = this.random.NextDouble();
                if (randomeValue < 0.55)
                {
                    result = BattingResult.GroundOut;
                }
                else if (randomeValue < 0.6)
                {
                    result = BattingResult.LineOut;
                }
                else if (randomeValue < 0.75)
                {
                    result = BattingResult.PopOut;
                }
                else
                {
                    result = BattingResult.FlyOut;
                }

                // Handle errors 
                randomeValue = this.random.NextDouble();
                if (randomeValue >= 0.90)
                {
                    if (result == BattingResult.FlyOut)
                    {
                        randomeValue = this.random.NextDouble();
                        if (randomeValue <= 0.5)
                        {
                            result = BattingResult.Double;
                        }
                        else if (randomeValue <= 0.9)
                        {
                            result = BattingResult.Triple;
                        }
                        else
                        {
                            result = BattingResult.Homerun;
                        }
                    }
                    else
                    {
                        result = BattingResult.Single;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the log5.
        /// </summary>
        /// <param name="hitterPercent">The hitter percent.</param>
        /// <param name="pitcherPercent">The pitcher percent.</param>
        /// <param name="leaguePercent">The league percent.</param>
        /// <returns>The log5 value.</returns>
        private Double CalculateLog5(Double hitterPercent, Double pitcherPercent, Double leaguePercent)
        {
            Double numerator = (hitterPercent * pitcherPercent) / leaguePercent;

            return numerator / (numerator + ((1 - hitterPercent) * (1 - pitcherPercent) / (1 - leaguePercent)));
        }

        /// <summary>
        /// Considers the pitching change.
        /// </summary>
        private void ConsiderPitchingChange()
        {            
            Boolean isHomeTeam = false;
            Int32 teamIndex = Game.VisitorTeam;
            if (this.scoreboard.HalfInning == HalfInningEnum.Top)
            {
                isHomeTeam = true;
                teamIndex = Game.HomeTeam;
            }

            if (this.parameters.Managers[teamIndex].PitchingChange(this.Pitcher, this.PitcherStatistics, this.scoreboard, isHomeTeam))
            {
                this.gameResults.HandlePitchingChange(this.parameters.Managers[teamIndex].Lineup.Pitcher, isHomeTeam);
                this.AnnouncerOutput(String.Format("Pitching change: {0}", this.Pitcher.LastName));

                logger.Debug("Pitching change {0} {1}", teamIndex, this.Pitcher.LastName);
            }            
        }

        /// <summary>
        /// Considers the pinch hitter.
        /// </summary>
        /// <param name="batter">The batter.</param>
        /// <returns>The pinch hitter if one is chosen otherwise null.</returns>
        private Player ConsiderPinchHitter(Player batter)
        {
            Player pinchHitter = null;

            Boolean isHome = true;
            Manager manager = this.parameters.Managers[Game.HomeTeam];
            if (this.scoreboard.HalfInning == HalfInningEnum.Top)
            {
                manager = this.parameters.Managers[Game.VisitorTeam];
                isHome = false;
            }

            pinchHitter = manager.PinchHitter(batter, this.scoreboard, this.bases, isHome);

            return pinchHitter;
        }

        /// <summary>
        /// Consider a stolen base.
        /// </summary>
        private void ConsiderStolenBase()
        {
            // TBD: For now, just handle runner on first base
            if (this.bases.State == BaseStates.First || this.bases.State == BaseStates.FirstAndThird)
            {
                var runner = this.bases.Occupied[1];
                Int32 singles = runner.BattingStats.Hits - (runner.BattingStats.Doubles + runner.BattingStats.Triples + runner.BattingStats.Homeruns);
                Double percent = (Double)(runner.BattingStats.StolenBases + runner.BattingStats.CaughtStealing) / (Double)(singles + runner.BattingStats.Walks);

                switch (runner.Speed)
                {
                    case 7:
                        percent *= 1.05;
                        break;
                    case 8:
                        percent *= 1.10;
                        break;
                    case 9:
                        percent *= 1.15;
                        break;
                }

                var randomValue = this.random.NextDouble();
                if (randomValue <= percent)
                {
                    percent = (Double)runner.BattingStats.StolenBases / (Double)(runner.BattingStats.StolenBases + runner.BattingStats.CaughtStealing);

                    randomValue = this.random.NextDouble();
                    if (randomValue <= percent)
                    {
                        this.AnnouncerOutput("Steal attempt...Safe");

                        // Stolen base
                        this.bases.Occupied[2] = this.bases.Occupied[1];
                        this.bases.Occupied[1] = null;
                    }
                    else
                    {
                        this.AnnouncerOutput("Steal attempt...Out");

                        // Caught stealing
                        this.bases.Occupied[1] = null;
                        this.AddOuts(1);
                    }

                    this.OutputBases();
                }
            }

            //// Only attempt steal if:
            //// 1st base only
            //// 2nd base only
            //// 1st & 2nd
            //// 1st & 3rd
            ////if (this.bases.Empty == false && 
            ////    this.bases.State != BaseStates.Loaded &&
            ////    this.bases.State != BaseStates.SecondAndThird &&
            ////    this.bases.State != BaseStates.Third)
            ////{
            ////    //Boolean attemptSteal = true;

            ////    //// Only try to steal 3rd with 1 out
            ////    //if (this.bases.State == BaseStates.Second &&
            ////    //    this.scoreboard.Outs != 1)
            ////    //{
            ////    //    attemptSteal = false;
            ////    //}
            ////}
        }

        /// <summary>
        /// Resets the statistics.
        /// </summary>
        private void ResetStatistics()
        {
            this.PitcherStatistics.Walks = 0;
            this.PitcherStatistics.Strikeouts = 0;
            this.PitcherStatistics.TotalOuts = 0;
            this.PitcherStatistics.Hits = 0;
        }

        /// <summary>
        /// Updates the statistics.
        /// </summary>
        /// <param name="result">The result.</param>
        private void UpdateStatistics(BattingResult result)
        {
            switch (result)
            {
                case BattingResult.Walk:
                    this.PitcherStatistics.Walks++;
                    break;
                case BattingResult.Strikeout:
                    this.PitcherStatistics.Strikeouts++;                    
                    break;
                case BattingResult.Single:
                case BattingResult.Double:
                case BattingResult.Triple:
                case BattingResult.Homerun:
                    this.PitcherStatistics.Hits++;
                    break;
                ////case BattingResult.FlyOut:
                ////case BattingResult.GroundOut:
                ////case BattingResult.LineOut:
                ////case BattingResult.PopOut:
                ////    this.PitcherStatistics.TotalOuts++;
                ////    break;                    
            }            
        }

        /// <summary>
        /// Updates the batter statistics.
        /// </summary>
        /// <param name="batter">The batter.</param>
        /// <param name="result">The result.</param>
        private void UpdateBatterStatistics(Player batter, BattingResult result)
        {
            Int32 index = Game.HomeTeam;
            if (this.scoreboard.HalfInning == HalfInningEnum.Top)
            {
                index = Game.VisitorTeam;
            }

            Player stats;
            if (this.gameResults.PlayerStatsDictionary[index].TryGetValue(batter.Identifier, out stats) == false)
            {
                stats = new Player();
                stats.Identifier = batter.Identifier;
                stats.FirstName = batter.FirstName;
                stats.LastName = batter.LastName;
                this.gameResults.PlayerStatsDictionary[index].Add(batter.Identifier, stats);
            }

            switch (result)
            {
                case BattingResult.Walk:
                    stats.BattingStats.Walks++;                    
                    break;
                case BattingResult.Strikeout:
                    stats.BattingStats.AtBats++;
                    stats.BattingStats.Strikeouts++;                    
                    break;
                case BattingResult.Single:
                    stats.BattingStats.AtBats++;
                    stats.BattingStats.Hits++;                    
                    break;
                case BattingResult.Double:
                    stats.BattingStats.AtBats++;
                    stats.BattingStats.Hits++;
                    stats.BattingStats.Doubles++;
                    break;
                case BattingResult.Triple:
                    stats.BattingStats.AtBats++;
                    stats.BattingStats.Hits++;
                    stats.BattingStats.Triples++;
                    break;
                case BattingResult.Homerun:
                    stats.BattingStats.AtBats++;
                    stats.BattingStats.Hits++;
                    stats.BattingStats.Homeruns++;
                    break;
                case BattingResult.FlyOut:
                case BattingResult.GroundOut:
                case BattingResult.LineOut:
                case BattingResult.PopOut:
                    stats.BattingStats.AtBats++;
                    break;
            }
        }

        /// <summary>
        /// Handles the base runners.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="batter">The batter.</param>
        private void HandleBaseRunners(BattingResult result, Player batter)
        {
            switch (result)
            {
                case BattingResult.Walk:
                    this.HandleWalk(batter);
                    break;
                case BattingResult.Single:
                    this.HandleSingle(batter);
                    break;
                case BattingResult.Double:
                    this.HandleDouble(batter);
                    break;
                case BattingResult.Triple:
                    this.HandleTriple(batter);
                    break;
                case BattingResult.Homerun:
                    this.HandleHomerun();
                    break;
                case BattingResult.GroundOut:
                    this.HandleGroundOut();
                    break;
                case BattingResult.PopOut:
                case BattingResult.LineOut:
                case BattingResult.FlyOut:
                case BattingResult.Strikeout:
                case BattingResult.Out:
                    this.AddOuts(1);
                    break;
            }
        }

        /// <summary>
        /// Handles a single.
        /// </summary>
        /// <param name="batter">The batter.</param>
        private void HandleSingle(Player batter)
        {
            Player runner = null;

            switch (this.bases.State)
            {
                case BaseStates.Empty:
                    this.bases.Occupied[1] = batter;
                    break;
                case BaseStates.First:
                    runner = this.bases.Occupied[1];
                    this.bases.Occupied[1] = batter;

                    double random = this.random.NextDouble();
                    if (random < 0.65)
                    {
                        // Runner goes to 2nd
                        this.bases.Occupied[2] = runner;
                    }
                    else if (random < .97)
                    {
                        // Runner advance to 3rd
                        this.bases.Occupied[3] = runner;
                    }
                    else if (random < .985)
                    {
                        // Runner scores
                        this.AddRuns(1);
                    }
                    else
                    {
                        // Runner out
                        this.AddOuts(1);
                    }

                    break;
                case BaseStates.Second:
                    runner = this.bases.Occupied[2];
                    this.bases.Occupied[1] = batter;
                    this.bases.Occupied[2] = null;
                    this.HandleRunnerOnSecond(runner);
                    break;
                case BaseStates.Third:
                    this.bases.Occupied[1] = batter;
                    this.bases.Occupied[2] = null;
                    this.bases.Occupied[3] = null;
                    this.AddRuns(1);
                    break;
                case BaseStates.FirstAndSecond:
                    {
                        runner = this.bases.Occupied[1];
                        var leadRunner = this.bases.Occupied[2];
                        this.bases.Occupied[1] = batter;
                        this.bases.Occupied[2] = runner;
                        this.HandleRunnerOnSecond(leadRunner);
                    }

                    break;
                case BaseStates.FirstAndThird:
                    runner = this.bases.Occupied[1];
                    this.bases.Occupied[1] = batter;
                    this.bases.Occupied[2] = runner;
                    this.bases.Occupied[3] = null;
                    this.AddRuns(1);
                    break;
                case BaseStates.SecondAndThird:
                    runner = this.bases.Occupied[2];
                    this.bases.Occupied[1] = batter;
                    this.bases.Occupied[2] = null;
                    this.AddRuns(1);
                    this.HandleRunnerOnSecond(runner);
                    break;
                case BaseStates.Loaded:
                    {
                        runner = this.bases.Occupied[1];
                        var leadRunner = this.bases.Occupied[2];
                        this.bases.Occupied[1] = batter;
                        this.bases.Occupied[2] = runner;
                        this.AddRuns(1);
                        this.HandleRunnerOnSecond(leadRunner);
                    }

                    break;
            }
        }

        /// <summary>
        /// Handles a double.
        /// </summary>
        /// <param name="batter">The batter.</param>
        private void HandleDouble(Player batter)
        {
            Player runner = null;

            switch (this.bases.State)
            {
                case BaseStates.Empty:
                    this.bases.Occupied[2] = batter;
                    break;
                case BaseStates.First:
                    runner = this.bases.Occupied[1];
                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = batter;
                    this.bases.Occupied[3] = runner;                    
                    break;
                case BaseStates.Second:
                    this.bases.Occupied[2] = batter;                    
                    this.AddRuns(1);                    
                    break;
                case BaseStates.Third:
                    this.bases.Occupied[2] = batter;
                    this.bases.Occupied[3] = null;
                    this.AddRuns(1);
                    break;
                case BaseStates.FirstAndSecond:
                    runner = this.bases.Occupied[1];
                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = batter;
                    this.bases.Occupied[3] = runner;
                    this.HandleRunnerOnSecond(runner);
                    break;
                case BaseStates.FirstAndThird:
                    runner = this.bases.Occupied[1];
                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = batter;
                    //// TBD: Runner scores from first?
                    this.bases.Occupied[3] = runner;
                    this.AddRuns(1);
                    break;                
                case BaseStates.SecondAndThird:                    
                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = batter;
                    this.bases.Occupied[3] = null;
                    this.AddRuns(2);
                    break;
                case BaseStates.Loaded:
                    runner = this.bases.Occupied[1];
                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = batter;
                    //// TBD: Runner scores from first?
                    this.bases.Occupied[3] = runner;
                    this.AddRuns(2);
                    break;                
            }
        }

        /// <summary>
        /// Handles a triple.
        /// </summary>
        /// <param name="batter">The batter.</param>
        private void HandleTriple(Player batter)
        {
            switch (this.bases.State)
            {
                case BaseStates.Empty:
                    this.bases.Occupied[3] = batter;
                    break;
                case BaseStates.First:
                case BaseStates.Second:
                case BaseStates.Third:
                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = null;
                    this.bases.Occupied[3] = batter;
                    this.AddRuns(1);
                    break;
                case BaseStates.FirstAndSecond:
                case BaseStates.FirstAndThird:
                case BaseStates.SecondAndThird:
                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = null;
                    this.bases.Occupied[3] = batter;
                    this.AddRuns(2);
                    break;

                case BaseStates.Loaded:
                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = null;
                    this.bases.Occupied[3] = batter;
                    this.AddRuns(3);
                    break;
            }
        }

        /// <summary>
        /// Handles a homerun.
        /// </summary>
        private void HandleHomerun()
        {
            switch (this.bases.State)
            {
                case BaseStates.Empty:
                    this.AddRuns(1);
                    break;
                case BaseStates.First:
                case BaseStates.Second:
                case BaseStates.Third:
                    this.AddRuns(2);
                    break;                
                case BaseStates.FirstAndSecond:
                case BaseStates.FirstAndThird:
                case BaseStates.SecondAndThird:
                    this.AddRuns(3);
                    break;                
                case BaseStates.Loaded:
                    // Grand Slam!
                    this.AddRuns(4);
                    break;
            }

            // Clear the bases
            this.bases.Clear();
        }

        /// <summary>
        /// Handles a walk.
        /// </summary>
        /// <param name="batter">The batter.</param>
        private void HandleWalk(Player batter)
        {
            switch (this.bases.State)
            {
                case BaseStates.Empty:
                case BaseStates.Second:
                case BaseStates.Third:
                case BaseStates.SecondAndThird:
                    this.bases.Occupied[1] = batter;                    
                    break;
                case BaseStates.First:
                    this.bases.Occupied[2] = this.bases.Occupied[1];
                    this.bases.Occupied[1] = batter;
                    break;
                case BaseStates.FirstAndSecond:
                    this.bases.Occupied[3] = this.bases.Occupied[2];
                    this.bases.Occupied[2] = this.bases.Occupied[1];
                    this.bases.Occupied[1] = batter;
                    break;
                case BaseStates.FirstAndThird:
                    this.bases.Occupied[2] = this.bases.Occupied[1];
                    this.bases.Occupied[1] = batter;
                    break;
                case BaseStates.Loaded:
                    this.bases.Occupied[3] = this.bases.Occupied[2];
                    this.bases.Occupied[2] = this.bases.Occupied[1];
                    this.bases.Occupied[1] = batter;
                    this.AddRuns(1);
                    break;
            }
        }

        /// <summary>
        /// Handles the ground out.
        /// </summary>
        private void HandleGroundOut()
        {
            this.AddOuts(1);

            switch (this.bases.State)
            {
                case BaseStates.First:
                    // Double play
                    this.bases.Occupied[1] = null;

                    this.DoublePlayOnGroundBall();
                    break;
                ////case BaseStates.Second:
                //// TBD
                ////bases.Occupied[2] = true;
                ////break;
                ////case BaseStates.Third:
                //// TBD                    
                ////bases.Occupied[3] = true;
                ////break;
                case BaseStates.FirstAndSecond:
                    this.DoublePlayOnGroundBall();

                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = null;                    
                    break;
                case BaseStates.FirstAndThird:
                    this.DoublePlayOnGroundBall();

                    this.bases.Clear();

                    if (this.scoreboard.Outs < 3)
                    {
                        this.AddRuns(1);
                    }

                    break;
                case BaseStates.SecondAndThird:
                    this.bases.Occupied[1] = null;
                    this.bases.Occupied[2] = null;                    

                    if (this.scoreboard.Outs < 3)
                    {
                        this.AddRuns(1);
                    }

                    break;
                case BaseStates.Loaded:
                    // Double play
                    this.bases.Occupied[3] = this.bases.Occupied[2];
                    this.bases.Occupied[2] = null;
                    this.bases.Occupied[1] = null;

                    this.DoublePlayOnGroundBall();

                    if (this.scoreboard.Outs < 3)
                    {
                        this.AddRuns(1);
                    }

                    break;
            }
        }

        /// <summary>
        /// Doubles the play on ground ball.
        /// </summary>
        private void DoublePlayOnGroundBall()
        {
            if (this.scoreboard.Outs < 3)
            {
                this.AddOuts(1);
                this.AnnouncerOutput("Double play");
            }
        }

        /// <summary>
        /// Handles the runner on second.
        /// </summary>
        /// <param name="runner">The runner.</param>
        private void HandleRunnerOnSecond(Player runner)
        {
            double randomValue = this.random.NextDouble();
            if (randomValue < 0.65)
            {
                // Nobody on third
                this.bases.Occupied[3] = null;

                // Runner scores from second                
                this.AddRuns(1);
            }
            else if (randomValue < 0.97)
            {
                // Runner stays on 3rd
                this.bases.Occupied[3] = runner;
            }
            else
            {
                // Nobody on third
                this.bases.Occupied[3] = null;

                // Runner is out at home                
                this.AddOuts(1);
            }
        }

        /// <summary>
        /// AddRuns - put runs on the scoreboard.  Check which half of the inning
        /// we're in before assigning the runs.
        /// </summary>        
        /// <param name="runs">The number of runs scored and to be put on the scoreboard</param>
        private void AddRuns(Int32 runs)
        {
            if (this.scoreboard.HalfInning == HalfInningEnum.Top)
            {
                this.scoreboard.VisitorScore += runs;
            }
            else
            {
                this.scoreboard.HomeScore += runs;

                // Did the winning run just score?
                if (this.scoreboard.Inning >= 9 && (this.scoreboard.HomeScore > this.scoreboard.VisitorScore))
                {
                    this.gameOver = true;
                }
            }
        }

        /// <summary>
        /// Adds the outs.
        /// </summary>
        /// <param name="outs">The outs.</param>
        private void AddOuts(int outs)
        {
            this.scoreboard.Outs += outs;
            
            //// TBD: Check for more than three outs?            

            this.PitcherStatistics.TotalOuts += outs;
        }

        /// <summary>
        /// Announcers the output.
        /// </summary>
        /// <param name="output">The output.</param>
        private void AnnouncerOutput(String output)
        {
            if (this.Announce != null)
            {
                this.Announce(this, output);
            }
        }

        /// <summary>
        /// Outputs the bases.
        /// </summary>
        private void OutputBases()
        {
            for (Int32 baseNumber = 1; baseNumber < 4; baseNumber++)
            {
                if (this.bases.Occupied[baseNumber] != null)
                {
                    String output = String.Format("{0}B: {1}", baseNumber, this.bases.Occupied[baseNumber].LastName);
                    this.AnnouncerOutput(output);
                }
                else
                {
                    String output = String.Format("{0}B: Empty", baseNumber);
                    this.AnnouncerOutput(output);
                }
            }
        }
    }   
}
