var scrollDown = function () {
    var chatlogscroll = document.getElementById("chatlogscroll");
    chatlogscroll.scrollTop = chatlogscroll.scrollHeight - chatlogscroll.clientHeight;
};

function write(s, output) {
    var p = document.createElement("li");
    p.textContent = s;
    output.appendChild(p);
    scrollDown();
}

function doConnect(output) {
    var uri = "ws://" + window.location.host + "/ws";
    var socket = new WebSocket(uri);
    var wasOpen = false;
    socket.onopen = function (e) {
        wasOpen = true;
        write("Connected to " + uri, output);
        var chatform = document.getElementById("chatform");
        chatform.addEventListener("submit", function (event) {
            if (event.preventDefault) {
                event.preventDefault();
            }
            var chatinput = document.getElementById("chatinput");
            var message = chatinput.value;
            if (message !== "")
            {
                socket.send(message);
                chatinput.value = "";
            }
            return false;
        }, false);
    };
    socket.onclose = function (e) {
        write(wasOpen ? "Disconnected" : "Could not connect", output);
        setTimeout(function () {
            doConnect(output);
        }, 5000);
    };
    socket.onmessage = function (e) {
        write("Received: " + e.data, output);
    };
}

window.onload = function () {
    var chatlog = document.getElementById("chatlog");
    doConnect(chatlog);
    scrollDown();
};
