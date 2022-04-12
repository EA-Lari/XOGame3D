using MatchMake.Backend.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace MatchMake.Backend.Tests
{

    [TestFixture]
    public class GameRoomShould
    {

        [Test]
        [TestCase("", "")]
        [TestCase("Killer_1000", "Killer_1000")]
        [TestCase("Batman95", "Batman95")]
        [TestCase("Witch_87", "Witch_87")]
        public void AddPlayerToRoom(string testCaseUserName, string expectedResult)
        {
            // Arrange
            var fixtureClass = GetNewGameRoomInstance();
            // Act
            var actualResult = fixtureClass.Add(testCaseUserName);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("Killer_1000", "Killer_1000")]
        [TestCase("Batman95", "Batman95")]
        [TestCase("Witch_87", "Witch_87")]
        public void RemovePlayerFromRoom(string testCaseUserName, string expectedResult)
        {
            // Arrange
            var fixtureClass = GetNewGameRoomInstance();
            fixtureClass.Add(testCaseUserName);
            // Act
            var actualResult = fixtureClass.Remove(testCaseUserName);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase()]
        public void ClearRoom()
        {
            // Arrange
            var fixtureClass = GetNewGameRoomInstance();
            // Act
            var actualResult = fixtureClass.Clear();
            // Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void GetAllPlayersInRoom()
        {
            // Arrange
            var fixtureClass = GetNewGameRoomInstance();

            var expectedResult = new List<string>()
            {
                "",
            };

            fixtureClass.Add("");

            // Act
            var actualResult = fixtureClass.GetAllPlayers();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        #region Test Heplers

        private GameRoom GetNewGameRoomInstance()
            => new GameRoom(5);

        #endregion

    }
}