const GAME_STREAMER_URL = "https://localhost:5001";
const GAME_HUB = "/game";
const LOBBY_HUB = "/lobbies";

// Create live collections
const messagesItems = document.querySelector(".messages-list");
const messageTemplate = document.getElementById("chat-message-template");

function createHubConnection(hubUrl) {
    let connection = new signalR.HubConnectionBuilder()
        .withUrl(GAME_STREAMER_URL + hubUrl)
        .configureLogging(signalR.LogLevel.Information)
        .build();

    return connection;
};

function setupGameConnection(gameConnection) {
    gameConnection.on("TestBroadcastPublish", (message) => {
        // let newMessage = messageTemplate.content.cloneNode(true);
        // let msgHeader = newMessage.querySelector(".item-header");
        // msgHeader.textContent = message;

        // messagesItems.append(newMessage);
        console.log(message + ' Waiting for 5 sec...');
    });

    gameConnection.onclose(async () => {
        await startHubAsync(gameConnection, 3000);
    });
};

function setupLobbyConnection(lobbyConnection) {
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

var gameHubConection = createHubConnection(GAME_HUB);
var lobbyHubConnection = createHubConnection(LOBBY_HUB);

setupGameConnection(gameHubConection);
setupLobbyConnection(lobbyHubConnection);

startHubAsync(gameHubConection, 3000);
startHubAsync(lobbyHubConnection, 3000);
