/** Constants */
const GAME_STREAMER_URL = "https://localhost:5001";
const GAME_HUB = "/game";
const LOBBY_HUB = "/lobbies";

/** Live collections */
const messagesItems = document.querySelector(".messages-list");
const roomsItems = document.querySelector(".room-list");

// const roomsLiveCollection = new Map();
// const playersLiveCollection = new Map();

/** Templates */
// const chatMessageTemplate = document.getElementById("chat-message-template");
const gameRoomTemplate = document.getElementById("game-room-template").content;
const playerTemplate = document.getElementById("player-item-template").content;

/** App EntryPoint */

var gameHubConection = createHubConnection(GAME_HUB);
var lobbyHubConnection = createHubConnection(LOBBY_HUB);

setupGameConnection(gameHubConection);
setupLobbyConnection(lobbyHubConnection);

startHubAsync(gameHubConection);
startHubAsync(lobbyHubConnection);

/** Add Click Events To Buttons */

const nicknameButton = document.querySelector(".player-nickname-button");
const nicknameInput = document.querySelector(".player-nickname-input");

nicknameButton.onclick = function () {
    lobbyHubConnection.invoke("PlayerAddedLogin", nicknameInput.value);
    nicknameButton.disabled = true;
    nicknameInput.disabled = true;
};

const dropRoomButton = document.querySelector(".drop-room-button");
const roomNameInput = document.querySelector(".room-name-input");

dropRoomButton.onclick = function () {
    // roomsLiveCollection.delete();
    
    if (roomNameInput.value) {
        const liForDelete = document.querySelector("[data-roomId*="+ roomNameInput.value +"]");
        if (liForDelete) {
            liForDelete.parentElement.removeChild(liForDelete);
        }
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
    });

    lobbyConnection.on("PlayerLeavedServer", (playerDto) => {
        console.log(playerDto.nickName + " with conn id: " + playerDto.connectionId + " left from The Game Server!");
    });

    lobbyConnection.on("NewRoomAdded", (newRoomDto) => {
        console.log("NewGameRoom is added: " + newRoomDto.roomName);

        var newRoomElement = renderNewRoomElement(newRoomDto);
        roomsItems.appendChild(newRoomElement);

        // updateRoomsCollection(newRoomDto.roomName, newRoomElement);
    });

    lobbyConnection.onclose(async () => {
        // await startHubAsync(lobbyConnection);
        setTimeout(await startHubAsync(lobbyConnection), 5000);
    });
};

function updatePlayersCollection(playerName, playerElement) {
    // playersLiveCollection.set(playerName, playerElement);
};

function renderNewRoomElement(gameRoomDto) {
    const newRoomItem = gameRoomTemplate.cloneNode(true);
    const roomNode = newRoomItem.querySelector('.room-item');
    const roomName = newRoomItem.querySelector('.room-header');
    var textContent = document.createTextNode(gameRoomDto.roomName);
    roomNode.setAttribute('data-roomId', gameRoomDto.roomName);

    roomName.appendChild(textContent);
    const playerList = newRoomItem.querySelector('.player-list');

    gameRoomDto.playersList.forEach((playerDto) => {
        const newPlayerItem = playerTemplate.cloneNode(true);
        const playerName = newPlayerItem.querySelector('.player-nickname');
        var textContent = document.createTextNode(playerDto.nickName);
        playerName.appendChild(textContent);

        playerList.appendChild(newPlayerItem);
    });

    return newRoomItem;
};

function updateRoomsCollection(roomName, htmlRoomElement) {
    // roomsLiveCollection.set(roomName, htmlRoomElement);
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