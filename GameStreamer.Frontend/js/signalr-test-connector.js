/** Constants */
const GAME_STREAMER_URL = "https://localhost:5001";
const GAME_HUB = "/game";
const LOBBY_HUB = "/lobbies";

/** Const Functions */
const varNameToString = varObj => Object.keys(varObj)[0];

/** Common Variables */
var currentClientNickName = "";

/** Live collections */
const messagesItems = document.querySelector(".messages-list");
const roomsItems = document.querySelector(".room-list");
const playersItems = document.querySelector(".player-list");

// const roomsLiveCollection = new Map();
// const playersLiveCollection = new Map();

/** Connection id collection */
// const connectionIdDict = new Map();

/** Templates */
// const chatMessageTemplate = document.getElementById("chat-message-template");
const gameRoomTemplate = document.getElementById("game-room-template").content;
const playerTemplate = document.getElementById("player-item-template").content;

/** App EntryPoint */

var gameHubConnection = createHubConnection(GAME_HUB);
var lobbyHubConnection = createHubConnection(LOBBY_HUB);

setupGameConnection(gameHubConnection);
setupLobbyConnection(lobbyHubConnection);

startHubAsync(gameHubConnection);
startHubAsync(lobbyHubConnection);

/** Add Click Events To Buttons */

const nicknameButton = document.querySelector(".player-nickname-button");
const nicknameInput = document.querySelector(".player-nickname-input");

nicknameButton.onclick = function () {
    lobbyHubConnection.invoke("PlayerAddedLogin", nicknameInput.value);
    currentClientNickName = nicknameInput.value;
    // nicknameButton.disabled = true;
    // nicknameInput.disabled = true;
};

const dropRoomButton = document.querySelector(".drop-room-button");
const roomNameInput = document.querySelector(".room-name-input");

dropRoomButton.onclick = function () {
    if (roomNameInput.value) {
        var element = findElementByAttr("data-roomId", roomNameInput.value);
        deleteNode(element);
    }
};

/** Functions */

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
        var playerNode = findElementByAttr("data-playerId", changedPlayerDataDto.nickName);        
        // const playerNode = newPlayerItem.querySelector('.player-item');
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

        var element = findElementByAttr("data-playerId", playerDto.nickName);
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

function findElementByAttr(attributeName, value) {
    const elementForSearch = document.querySelector("["+ attributeName +"*="+ value +"]");
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