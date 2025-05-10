//#region Globals
let connection;
let userId = "";
let username = "";
let receiverId = "";
let receiverName = "";
//#endregion

//#region Initialization
$(document).ready(function () {
    setupLogin();
    setupSendMessage();
    setupTypingNotification();
});
//#endregion

//#region Login
function setupLogin() {
    $("#loginForm").submit(function (e) {
        e.preventDefault();
        username = $("#username").val().trim();

        if (!username || username.length < 3 || username.length > 20) {
            alert("Username must be between 3 and 20 characters.");
            return;
        }

        connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7042/chat-hub")
            .build();

        registerSignalREvents();

        connection.start()
            .then(() => {
                console.log("SignalR connected");
                return connection.invoke("SetUsername", username);
            })
            .then(connectionId => {
                userId = connectionId;
                $("#chatMain").css("visibility", "visible");
                $("#deletedLogin").remove();
                return connection.invoke("GetUsers");
            })
            .catch(err => console.error("Login error:", err));
    });
}
//#endregion

//#region Register SignalR Event Handlers
function registerSignalREvents() {
    connection.on("ReceiveUsers", updateUsersList);
    connection.on("ReceiveMessage", message => addMessage(message, false));
    connection.on("ReceiveTypingNotification", showTypingIndicator);
    connection.on("UserOffline", function (disconnectedUserId) {
        $(`[data-id="${disconnectedUserId}"]`).remove();

        if (receiverId === disconnectedUserId) {
            $("#chatWith").text("Selected user went offline");
            receiverId = "";
            receiverName = "";
        }
    });
}
//#endregion

//#region Users List
function updateUsersList(users) {
    $('#users').empty();

    users.forEach(user => {
        if (user.connectionId !== userId) {
            const userItem = `
                <li class="user-item list-group-item d-flex align-items-center gap-2 py-2 px-3 mb-2 bg-white text-dark rounded shadow-sm"
                    data-id="${user.connectionId}" data-name="${user.name}" style="cursor:pointer;">
                    <img src="https://ui-avatars.com/api/?name=${user.name}&background=random&size=32"
                        class="rounded-circle" width="32" height="32" alt="${user.name}">
                    <span class="fw-semibold">${user.name}</span>
                </li>`;
            $('#users').append(userItem);
        }
    });

    $(".user-item").on("click", function () {
        receiverId = $(this).data("id");
        receiverName = $(this).data("name");
        $("#chatWith").text(`Chatting with ${receiverName}`);
        $(".user-item").removeClass("active");
        $(this).addClass("active");
    });
}
//#endregion

//#region Message Sending
function setupSendMessage() {
    $("#sendBtn").on("click", function () {
        const message = $("#messageInput").val().trim();
        if (!receiverId) return alert("Select a contact to chat.");
        if (!message) return;

        connection.invoke("SendPrivateMessage", receiverId, message)
            .then(() => {
                addMessage(message, true);
                $("#messageInput").val("");
            })
            .catch(err => console.error("Send error:", err));
    });
}
//#endregion

//#region Message UI
function addMessage(message, isMine) {
    const time = new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    const align = isMine ? 'justify-content-end' : 'justify-content-start';
    const style = isMine ? 'bg-primary text-white' : 'bg-white';

    const messageHtml = `
        <div class="d-flex ${align} mb-3">
            <div class="${style} p-2 rounded-3 shadow-sm" style="max-width: 70%;">
                <p class="mb-0">${message}</p>
                <small class="text-muted">${time}</small>
            </div>
        </div>`;

    $("#messageContainer").append(messageHtml);
    $("#messageContainer").scrollTop($("#messageContainer")[0].scrollHeight);
}
//#endregion

//#region Typing Indicator
function setupTypingNotification() {
    $("#messageInput").on("input", function () {
        const message = $(this).val().trim();
        if (receiverId && message.length > 0) {
            connection.invoke("NotifyTyping", receiverId);
        }
    });
}

function showTypingIndicator() {
    if ($("#typingIndicator").length === 0) {
        $(".input-group").before(`
            <div id="typingIndicator" class="text-muted ps-3 text-end" style="font-style: italic; color: gray;">
                ${receiverName} is typing...
            </div>`);

        setTimeout(() => {
            $("#typingIndicator").fadeOut(300, function () {
                $(this).remove();
            });
        }, 2000);
    }
}
//#endregion