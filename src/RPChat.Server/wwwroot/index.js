function write(s, output) {
    var p = document.createElement("p");
    p.textContent = s;
    output.appendChild(p);
}

function doConnect(output) {
    var uri = "ws://" + window.location.host + "/ws";
    var socket = new WebSocket(uri);
    socket.onopen = function (e) {
        write("opened " + uri, output);
        var text = "test echo";
        write("Sending: " + text, output);
        socket.send(text);
    };
    socket.onclose = function (e) {
        write("closed", output);
    };
    socket.onmessage = function (e) {
        write("Received: " + e.data, output);
        socket.close();
    };
    socket.onerror = function (e) {
        write("Error: " + e.data, output);
    };
}

window.onload = function () {
    output = document.getElementById("output");
    doConnect(output);
};
