<script>
	import { onMount } from "svelte";
	import * as signalR from "@microsoft/signalr";
	
	/**
	 * @type {signalR.HubConnection}
	 */
	let connection;
	
	onMount(async () => {
		connection = new signalR.HubConnectionBuilder()
			.configureLogging(signalR.LogLevel.Trace)
			.withUrl("/chathub", {
				skipNegotiation: true,
				transport: signalR.HttpTransportType.WebSockets
			})
			.build();

		connection.on("messageReceived", (username, message) => {
			addMessage(username, message)
		});

		try {
			await connection.start()
		}
		catch (error) {
			console.error(error)
		}
	});

	/**
	* @type {string | undefined}
	*/
	let username

	/**
	* @type {string | undefined}
	*/
	let message

	/**
	* @type {boolean}
	*/
	$: canSendMessage = username != undefined && username.length > 0
		&& message != undefined && message.length > 0

	/**
	 * @type {Array<{id: number, username: string, message: string}>}
	 */
	let messages = []

	/**
	 * @returns {Promise<void>}
	 */
	const sendMessage = async () => {
		if (!message)
			return

		try {
			await connection.send("sendMessage", username, message)
			message = ""
		}
		catch (error) {
			throw error;
		}
	};

	/**
	 * Add message to list of messages.
	 * @param {string} username
	 * @param {string} message
	 */
	const addMessage = (username, message) => {
		messages = [
			...messages,
			{
				id: Math.random(),
				username: username,
				message: message
			}
		]
	};
</script>

<svelte:head>
	<title>Chat</title>
	<meta name="description" content="In-app chat" />
</svelte:head>

<div class="text-column">
	<h1>Chat</h1>

	<p>Using SignalR for realtime chat messaging.</p>
	<p>Visit <a href="https://learn.microsoft.com/en-us/aspnet/core/signalr">learn.microsoft.com/en-us/aspnet/core/signalr</a> to learn more.</p>

	<form on:submit|preventDefault={sendMessage}>
		<label for="username">Username</label>
		<input id="username" type="text" bind:value={username} />

		<label for="message">Send message</label>
		<input id="message" type="text" bind:value={message} />
		<button type="submit" disabled={!canSendMessage}>
			Send
		</button>
	</form>
	
	{#each messages as message}
		<p>{message.username}: {message.message}</p>
	{/each}
</div>
