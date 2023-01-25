/** Constants */
const GAME_STREAMER_URL = "https://localhost:5001";
const GAME_HUB = "/game";
const LOBBY_HUB = "/lobbies";

/** Const Functions */
const varNameToString = varObj => Object.keys(varObj)[0];

/** Game Entities */
const gameCells = document.querySelectorAll(".cell");
const smallGameAreas = document.querySelectorAll(".field-small");
let isCurrentClientPlayCross = true;
let lastCellChoosedForTurn;

/** Common Variables */
var currentClientNickName = "Anon" + Math.floor(Math.random() * 100000);;

/** Live collections */
const messagesItems = document.querySelector(".messages-list");
const roomsItems = document.querySelector(".room-list");
const playersItems = document.querySelector(".player-list");

/** Data Inputs */
const changeNickButton = document.querySelector(".player-nickname-button");
const nicknameInput = document.querySelector(".player-nickname-input");
nicknameInput.value = currentClientNickName;

// const dropRoomButton = document.querySelector(".drop-room-button");
const roomNameInput = document.querySelector(".room-name-input");

/** Templates */
const gameRoomTemplate = document.getElementById("game-room-template").content;
const playerTemplate = document.getElementById("player-item-template").content;

/** Loader */
const loader = document.querySelector(".loader");

/** Sequence of game process menus */

const menuElementsDict = new Map([
    ["typeGameChoose", document.querySelector(".type-game-choose")],
    ["randomGameChosen", document.querySelector(".random-game-chosen")],
    ["dedicatedGameChosen", document.querySelector(".dedicated-game-chosen")],
    ["gameArea", document.querySelector(".game-area")]
]);

const startRandomGameButton = menuElementsDict.get('typeGameChoose').querySelector(".start-random-game-button");
const startDedicatedGameButton = menuElementsDict.get('typeGameChoose').querySelector(".start-dedicated-game-button");

const createRoomButton = menuElementsDict.get('dedicatedGameChosen').querySelector(".create-room-button");
const joinByRoomNameButton = menuElementsDict.get('dedicatedGameChosen').querySelector(".join-by-room-name-button");

/** Add Click Events To Buttons */

changeNickButton.onclick = function () {
    lobbyHubConnection.invoke("PlayerAddedLogin", nicknameInput.value);
    currentClientNickName = nicknameInput.value;
};

// dropRoomButton.onclick = function () {
//     if (roomNameInput.value) {
//         var element = findElementByAttr(document, "data-roomId", roomNameInput.value);
//         deleteNode(element);
//     }
// };

startRandomGameButton.onclick = async function () {
    toggleVisibility(menuElementsDict.get('typeGameChoose'));
    toggleVisibility(loader);
    await delay(2000);
    toggleVisibility(loader);
    toggleVisibility(menuElementsDict.get('randomGameChosen'));
    const isRandomGame = true;
    gameHubConnection.invoke("PlayerIsReady", isRandomGame);
};

startDedicatedGameButton.onclick = async function () {
    toggleVisibility(menuElementsDict.get('typeGameChoose'));
    toggleVisibility(loader);
    await delay(1000);
    toggleVisibility(loader);
    toggleVisibility(menuElementsDict.get('dedicatedGameChosen'));
};

createRoomButton.onclick = function () {
    toggleVisibility(menuElementsDict.get('dedicatedGameChosen'));
    toggleVisibility(loader);
};

joinByRoomNameButton.onclick = function () {
    toggleVisibility(menuElementsDict.get('dedicatedGameChosen'));
    toggleVisibility(loader);
};

gameCells.forEach(cell => {
    cell.addEventListener('click', async (event) => {
        await delay(500);
        setPlayerFigure(cell);
        lockPlayground();

        const cellCoordArr = event.target.dataset.cellCoordinates.split(',');
        const fieldCoordArr = event.target.parentElement.dataset.fieldCoordinates.split(',');

        const turnData = {
            smallAreaCoordinates: { x: fieldCoordArr[0], y: fieldCoordArr[1] },
            cellCoordinates: { x: cellCoordArr[0], y: cellCoordArr[1] }
        };

        gameHubConnection.invoke("PlayerDoTurn", turnData);

        lastCellChoosedForTurn = cell;
    });

});

/** App EntryPoint */
var gameHubConnection = createHubConnection(GAME_HUB);
var lobbyHubConnection = createHubConnection(LOBBY_HUB);

setupGameConnection(gameHubConnection);
setupLobbyConnection(lobbyHubConnection);

startHubAsync(gameHubConnection);
startHubAsync(lobbyHubConnection);

startGameProcessAsync();

/** Functions */

async function startGameProcessAsync() {
    toggleVisibility(loader);
    await delay(2000);
    toggleVisibility(loader);
    toggleVisibility(menuElementsDict.get('typeGameChoose'));
};

async function delay(ms) {
    return await new Promise((resolve) => setTimeout(resolve, ms));
};

/** Game Func */

function setPlayerFigure(cell) {

    if (isCurrentClientPlayCross) {
        cell.classList.add("cross-hold");
    }
    else {
        cell.classList.add("zero-hold");
    }
};

function setOpponentFigure(cell) {
    if (isCurrentClientPlayCross) {
        cell.classList.add("zero-hold");
    }
    else {
        cell.classList.add("cross-hold");
    }

};

function rollbackTurn(cell) {
    if (isCurrentClientPlayCross) {
        cell.classList.remove("cross-hold");
    }
    else {
        cell.classList.remove("zero-hold");
    }
};

function lockPlayground() {
    smallGameAreas.forEach(smallArea => {
        smallArea.classList.add("inactive-area");
    });
};

function unlockPlayground() {
    smallGameAreas.forEach(smallArea => {
        smallArea.classList.remove("inactive-area");
    });
};

function toggleVisibility(element) {
    element.classList.contains('visually-hidden') ? element.classList.remove("visually-hidden") : element.classList.add("visually-hidden");
};

function createHubConnection(hubUrl) {
    let connection = new signalR.HubConnectionBuilder()
        .withUrl(GAME_STREAMER_URL + hubUrl)
        .configureLogging(signalR.LogLevel.Warning)
        .build();

    return connection;
};

function setupGameConnection(gameConnection) {
    gameConnection.on("TestBroadcastPublish", (message) => {
        console.log(message);
    });

    gameConnection.on("GameIsStarted", async () => {
        console.log('Игра начинается...');

        await delay(2000);
        toggleVisibility(menuElementsDict.get('randomGameChosen'));
        toggleVisibility(loader);
        await delay(1000);
        toggleVisibility(loader);
        toggleVisibility(menuElementsDict.get('gameArea'));
    });

    gameConnection.on("TurnAccepted", async () => {
        console.log('Ход принят успешно!');
    });

    gameConnection.on("TurnDenied", async () => {
        console.log('Ход отменен, откатываем последний ход, разблокируем поле для повторного!');
        rollbackTurn(lastCellChoosedForTurn);
        unlockPlayground();
    });

    gameConnection.on("OpponentDoTurn", async (turnData) => {
        console.log('Ваш оппонент сходил!');

        const areaCoordStr = turnData.smallAreaCoordinates.x.toString().concat(",", turnData.smallAreaCoordinates.y.toString());
        const cellCoordStr = turnData.cellCoordinates.x.toString().concat(",", turnData.cellCoordinates.y.toString());

        const parentArea = findElementByAttr(document, "data-field-coordinates", areaCoordStr);
        const targetCell = findElementByAttr(parentArea, "data-cell-coordinates", cellCoordStr);;
        setOpponentFigure(targetCell);
        unlockPlayground();
    });

    gameConnection.onclose(async () => {
        setTimeout(await startHubAsync(gameConnection), 5000);
    });
};

function setupLobbyConnection(lobbyConnection) {
    lobbyConnection.on("NewPlayerJoined", (playerDto) => {
        console.log(playerDto.nickName + " with conn id: " + playerDto.connectionId + " joined To The Game Server!");

        var newPlayerElement = renderNewPlayerElement(playerDto);
        playersItems.appendChild(newPlayerElement);
    });

    lobbyConnection.on("PlayerChangedNickName", (changedPlayerDataDto) => {
        if (currentClientNickName === changedPlayerDataDto.nickName) {
            console.log("Логины совпадают! Клиент который сменил логин найден!");
        }
        var playerNode = findElementByAttr(document, "data-playerId", changedPlayerDataDto.nickName);
        const playerName = newPlayerItem.querySelector('.player-nickname');
        playerName.textContent = changedPlayerDataDto.nickName;
        playerNode.data = "";
        playerNode.setAttribute('', newPlayerDto.nickName);
    });

    lobbyConnection.on("UpdatePlayersWithoutRooms", (playersWithoutRoomsList) => {

        playersWithoutRoomsList.forEach((playerWithoutRooms) => {
            var newPlayerElement = renderNewPlayerElement(playerWithoutRooms);
            playersItems.appendChild(newPlayerElement);
        });
    });

    lobbyConnection.on("PlayerLeavedServer", (playerDto) => {
        console.log(playerDto.nickName + " with conn id: " + playerDto.connectionId + " left from The Game Server!");

        var element = findElementByAttr(document, "data-playerId", playerDto.nickName);
        deleteNode(element);
    });

    lobbyConnection.on("NewRoomAdded", (newRoomDto) => {
        console.log("NewGameRoom is added: " + newRoomDto.roomName);

        var newRoomElement = renderNewRoomElement(newRoomDto);
        roomsItems.appendChild(newRoomElement);
    });

    lobbyConnection.onclose(async () => {
        setTimeout(await startHubAsync(lobbyConnection), 5000);
    });
};

function findElementByAttr(parentElement, attributeName, value) {
    const elementForSearch = parentElement.querySelector("[" + attributeName + "*=" + value + "]");
    return elementForSearch;
};

function deleteNode(node) {
    if (node) {
        node.parentElement.removeChild(node);
    }
};

function renderNewRoomElement(newGameRoomDto) {
    const newRoomItem = gameRoomTemplate.cloneNode(true);
    const roomNode = newRoomItem.querySelector('.room-item');
    const roomName = newRoomItem.querySelector('.room-header');
    var textContent = document.createTextNode(newGameRoomDto.roomName);
    roomNode.setAttribute('data-roomId', newGameRoomDto.roomName);

    roomName.appendChild(textContent);
    const playerList = newRoomItem.querySelector('.player-list');

    newGameRoomDto.playersList.forEach((playerDto) => {
        const newPlayerItem = renderNewPlayerElement(playerDto);
        playerList.appendChild(newPlayerItem);
    });

    return newRoomItem;
};

function renderNewPlayerElement(newPlayerDto) {
    const newPlayerItem = playerTemplate.cloneNode(true);
    const playerNode = newPlayerItem.querySelector('.player-item');
    const playerName = newPlayerItem.querySelector('.player-nickname');
    var textContent = document.createTextNode(newPlayerDto.nickName);
    playerNode.setAttribute('data-playerId', newPlayerDto.nickName);

    playerName.appendChild(textContent);

    return newPlayerItem;
};

async function startHubAsync(hubConnection) {
    try {
        await hubConnection.start();
        console.log("Client " + hubConnection.connection.connectionId + " was connected to hub: " + hubConnection.connection.baseUrl);
    }
    catch (err) {
        console.log(err);
    }
};