/** Constants */
const GAME_STREAMER_URL = "https://localhost:5001";
const GAME_HUB = "/game";
const LOBBY_HUB = "/lobbies";

/** Live collections */
const messagesItems = document.querySelector(".messages-list");
const messageTemplate = document.getElementById("chat-message-template");

/** App EntryPoint */

var gameHubConection = createHubConnection(GAME_HUB);
var lobbyHubConnection = createHubConnection(LOBBY_HUB);

setupGameConnection(gameHubConection);
setupLobbyConnection(lobbyHubConnection);

startHubAsync(gameHubConection, 3000);
startHubAsync(lobbyHubConnection, 3000);

/** Add Click Events To Buttons */

const nicknameButton = document.querySelector(".player-nickname-button");
const nicknameInput = document.querySelector(".player-nickname-input");

nicknameButton.onclick = function() {
    lobbyHubConnection.invoke("PlayerAddedLogin", nicknameInput.value);
    nicknameButton.disabled = true;
    nicknameInput.disabled = true;
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
        // let newMessage = messageTemplate.content.cloneNode(true);
        // let msgHeader = newMessage.querySelector(".item-header");
        // msgHeader.textContent = message;

        // messagesItems.append(newMessage);
        console.log(message);
    });

    gameConnection.onclose(async () => {
        await startHubAsync(gameConnection, 3000);
    });
};

function setupLobbyConnection(lobbyConnection) {
    lobbyConnection.on("NewPlayerJoined", (playerDto) => {
        console.log(playerDto.nickName + " joined To The Game!");
    });

    lobbyConnection.onclose(async () => {
        await startHubAsync(lobbyConnection, 3000);
    });
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