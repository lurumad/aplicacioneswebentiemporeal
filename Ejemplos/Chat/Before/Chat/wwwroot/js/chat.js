class Chat {
    constructor() {
        this.me = ko.observable();
        this.text = ko.observable();
        this.users = ko.observable(1);
        this.messages = ko.observableArray([]);
        this.count = ko.computed(() => this.messages().length);
        this.canSend = ko.computed(() => this.me() && this.text());
    }

    ownMessage(username) {
        return ko.computed(() => this.me() === username ? 'my-message' : 'other-message float-right');
    }

    send() {
        const message = new Message(this.me(), this.text());
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

