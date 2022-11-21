// Create live collections
let socketMessagesItems = document.querySelector(".socket-messages-list");
let streamMessagesItems = document.querySelector(".stream-messages-list");
let messageTemplate = document.getElementById("chat-message-template");

// connection.onclose(async () => {
//     await start();
// });

let connection;

const startStreamButton = document.getElementById('startStream');
const watchStreamButton = document.getElementById('watchStream');

async function startAsync() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:5001/test_rooms")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    let subject;

    try {
        await connection.start();
        console.log("SignalR Connected.");
    }
    catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }

    connection.onclose(function (err) {
        subject.complete();
    });

    connection.on("ReceiveHelloWorld", (message) => {
        let newMessage = messageTemplate.content.cloneNode(true);
        let msgHeader = newMessage.querySelector(".item-header");
        msgHeader.textContent = message;

        socketMessagesItems.append(newMessage);
    });

};

startStreamButton.onclick = async function () {

    subject = new signalR.Subject();

    await connection.send("StartStream", "Gavno", subject);

}

watchStreamButton.onclick = function () { watchStreamAsync("Gavno"); };



async function watchStreamAsync(streamName) {
    const ISub = connection.stream("WatchStream", streamName).subscribe({
        next: (date) => {
            let newMessage = messageTemplate.content.cloneNode(true);
            let msgHeader = newMessage.querySelector(".item-header");
            msgHeader.textContent = date;
            streamMessagesItems.append(newMessage);
        },
        complete: () => {
            let newMessage = messageTemplate.content.cloneNode(true);
            newMessage.classList.add("message-danger");
            let msgHeader = newMessage.querySelector(".item-header");

            msgHeader.textContent = "Все, ебать, стриму конец!";
            streamMessagesItems.append(newMessage);
        },
        error: (err) => {
            console.log(err.message);
        },
    });

};

startAsync();

// connection.stream("StreamCurrentDate", 3, 3000)
//     .subscribe({
//         next: (date) => {
//             let newMessage = messageTemplate.content.cloneNode(true);
//             let msgHeader = newMessage.querySelector(".item-header");
//             msgHeader.textContent = date;
//             streamMessagesItems.append(newMessage);
//         },
//         complete: () => {
//             let newMessage = messageTemplate.content.cloneNode(true);
//             newMessage.classList.add("message-danger");
//             let msgHeader = newMessage.querySelector(".item-header");

//             msgHeader.textContent = "Все, ебать, стриму конец!";
//             streamMessagesItems.append(newMessage);
//         },
//         error: (err) => {
//             console.log(err.message);
//         },
// });

// Start the connection.