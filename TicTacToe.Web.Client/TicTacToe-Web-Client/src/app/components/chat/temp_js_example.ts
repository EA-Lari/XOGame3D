// const connection = new signalR.HubConnectionBuilder()
// 	.withUrl("http://localhost:57785/messages")
// 	.withAutomaticReconnect()
// 	.build();

// connection.on('Send', (message) => {
// 	appendMessage(message.sender, message.text, 'black');
// });

// connection.onclose(error => {
// 	console.log('Connection closed. ', error)
// });

// connection.onreconnecting(error => {
// 	console.log('Connection reconnecting. ', error);
// });

// connection.onreconnected(connectionId => {
// 	console.log('Connectin reconnected with id: ', connectionId);
// });

// function appendMessage(sender, message, color) {
// 	document.querySelector('#messages-content').insertAdjacentHTML("beforeend", `<div style="color:${color}"><p>Sender: ${sender}</p><p>Text: ${message}</p></div><br>`);
// }

// async function connect() {
// 	if (connection.state === 'Disconnected') {
// 		try {
// 			await connection.start();
// 		}
// 		catch (error) {
// 			console.log(error);
// 		}
// 		if (connection.state === 'Connected') {
// 			document.querySelector('#conState').textContent = 'Connected';
// 			document.querySelector('#conState').style.color = 'green';
// 			document.querySelector('#connectButton').textContent = 'Disconnect';
// 		}
// 	} else if (connection.state === 'Connected') {
// 		await connection.stop();
// 		document.querySelector('#conState').textContent = 'Disconnected';
// 		document.querySelector('#conState').style.color = 'red';
// 		document.querySelector('#connectButton').textContent = 'Connect';
// 	}
// };

// async function sendMessage() {
// 	if (connection.state === 'Connected') {
// 		let textArea = document.querySelector('#message');
// 		let message = { text: textArea.value };
// 		try {
// 			await connection.send('SendToOthers', message);
// 			appendMessage('Me', message.text, 'green');
// 		}
// 		catch (error) {
// 			console.log(error);
// 		}
// 		document.querySelector('#message').value = '';
// 	}
// }

// async function getMyName() {
// 	if (connection.state === 'Connected') {
// 		try {
// 			let myName = await connection.invoke('GetMyName');
// 			document.querySelector('#name').value = myName;
// 		}
// 		catch (error) {
// 			console.log(error);
// 		}
// 	}
// }

// async function setMyName() {
// 	if (connection.state === 'Connected') {
// 		try {
// 			let name = document.querySelector('#name').value;
// 			await connection.send('SetMyName', name);
// 		}
// 		catch (error) {
// 			console.log(error);
// 		}
// 	}
// }