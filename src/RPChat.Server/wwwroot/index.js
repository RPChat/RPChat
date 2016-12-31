function write(s, output) {
    var p = document.createElement("li");
    p.textContent = s;
    output.appendChild(p);
    var chatlogscroll = document.getElementById("chatlogscroll");
    chatlogscroll.scrollTop = chatlogscroll.scrollHeight - chatlogscroll.clientHeight;
}

function doConnect(output) {
    var uri = "ws://" + window.location.host + "/ws";
    var socket = new WebSocket(uri);
    socket.onopen = function (e) {
        write("opened " + uri, output);
        var text = "test echo";
        write("Sending: " + text, output);
        socket.send(text);

        var chatform = document.getElementById("chatform");
        chatform.addEventListener("submit", function (event) {
            if (event.preventDefault) {
                event.preventDefault();
            }
            var chatinput = document.getElementById("chatinput");
            socket.send(chatinput.value);
            chatinput.value = "";
            return false;
        }, false);
    };
    socket.onclose = function (e) {
        write("closed", output);
    };
    socket.onmessage = function (e) {
        write("Received: " + e.data, output);
    };
    socket.onerror = function (e) {
        write("Error: " + e.data, output);
    };
}

window.onload = function () {
    var chatlog = document.getElementById("chatlog");
    doConnect(chatlog);
};
