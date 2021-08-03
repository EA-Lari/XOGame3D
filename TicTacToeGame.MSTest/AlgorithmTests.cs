using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToeGame.BLL.Interfaces;
using TicTacToeGame.WPF;
using TicTacToeGame.WPF.Models;

namespace TicTacToeGame.MSTest
{
    [TestClass]
    public class AlgorithmTests
    {
        private GameService _algorithm;
        private BigAreaModel _bigArea;

        [TestInitialize]
        public void InitTest()
        {
            this._algorithm = new GameService();
            this._bigArea = _algorithm.CreateGame();           
        }
        /// <summary>
        /// Метод проверяет главную диагональ на победителя
        /// </summary>
        [TestMethod]
        public void MainDiagonalWinner_Cross_Test()
        {
            // Arrange
            var expected = State.Cross;
            
            foreach (var miniArea in this._bigArea.CellsList)
            {
                if (miniArea.Coordinates.CoordX == miniArea.Coordinates.CoordY)
                {
                    miniArea.CellState = State.Cross;
                }
            }

            // Act
            this._algorithm.CheckWin(this._bigArea, State.Cross);            
            var actual = this._bigArea.AreaState;

            // Assert
            Assert.AreEqual( expected, actual );
        }

        [TestMethod]
        public void Check_Draw_Test()
        {
            // Arrange
            var expected = State.Draw;

            this._bigArea.CellsList = new List<Cell>()
            {
                new CellModel(0, 0, State.Cross),
                new CellModel(0, 1, State.Zero),
                new CellModel(0, 2, State.Cross),
                new CellModel(1, 0, State.Cross),
                new CellModel(1, 1, State.Zero),
                new CellModel(1, 2, State.Zero),
                new CellModel(2, 0, State.Zero),
                new CellModel(2, 1, State.Cross),
                new CellModel(2, 2, State.Cross)
            };
            
            // Act
            this._algorithm.CheckWin(this._bigArea, State.Cross);
            var actual = this._bigArea.AreaState;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        // Arrange
        // *Вставка кода для подготовки теста

        // Act
        // *Вставка кода для вызова тестируемого метода или свойства

        // Assert
        // *Вставка кода для проверки завершения теста ожидаемым образом

        /* Пример */
        // Arrange
        //var viewModel = GetViewModel();
        //var expectedFeedData = new List<FeedData>();
        //this.RssFeedService.GetFeedsAsyncDelegate = () =>
        //{
        //    return expectedFeedData;
        //};
        // Act
        //var actualFeedData = viewModel.FeedData;
        // Assert
        //Assert.AreSame(expectedFeedData, actualFeedData);       
    }
}
