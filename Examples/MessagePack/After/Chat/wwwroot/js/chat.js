class Chat {
    constructor() {
        this.me = ko.observable();
        this.text = ko.observable();
        this.users = ko.observable(1);
        this.messages = ko.observableArray([]);
        this.count = ko.computed(() => this.messages().length);
        this.canSend = ko.computed(() => this.me() && this.text());
        this._connection = new signalR.HubConnectionBuilder()
            .withUrl("/chat")
            .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
            .build();

        this._connection.on('message', (message) => {
            this.messages.push(message);
        });

        this._connection.on("connected", (count) => {
            this.users(count);
        });

        this._connection.on("disconnected", (count) => {
            this.users(count);
        });

        this._connection.start().catch(error => console.error(error));
    }

    ownMessage(username) {
        return ko.computed(() => this.me() === username ? 'my-message' : 'other-message float-right')
    }

    send() {
        const message = new Message(this.me(), this.text());
        this._connection.invoke("SendMessage", message);
    }
}

class Message {
    constructor(username, text) {
        this.username = username;
        this.time = new Date();
        this.text = text;
    }
}

ko.applyBindings(new Chat());

