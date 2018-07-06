class WhatsApp {
    constructor() {
        this.comment = ko.observable('');
        this.me = ko.observable(new Me());
        this.activeChat = ko.observable();
        this.chats = ko.observableArray([]);
        this._connection = new signalR.HubConnectionBuilder()
            .withUrl('/whatsapp')
            .build();
        fetch("/api/whatsapp/users/me")
            .then(res => res.json())
            .then(data => {
                this.me(data.me);
                this.chats(data.chats);
                this.activeChat(data.chats[0]);
                this.connect();
            });
    }

    connect() {
        this._connection.start().catch(error => console.error(error));
    }

    chat(contact) {
        this.activeChat(contact);
    }

    send() {
        console.log('send');
        this._connection.invoke("SendMessage", { chatId: this.activeChat().id, userName: this.me().name, userId: this.me().id, text: this.comment() });
        this.comment('');
    }

    openNewChat(data, event) {
        document.getElementsByClassName("side-two")[0].style.left = "0";
    }

    closeNewChat(data, event) {
        document.getElementsByClassName("side-two")[0].style.left = "-100%";
    }

}

class Me {
    constructor() {
        this.id = ko.observable(-1);
        this.name = ko.observable('');
        this.avatar = ko.observable('');
    }
}

class Message {
    constructor() {
        this.userid = ko.observable(0);
        this.username = ko.observable('');
        this.text = ko.observable('');
        this.time = ko.observable();
    }
}

class Chat {
    constructor() {
        this.id = ko.observable('');
        this.participants = ko.observable([]);
        this.messages = ko.observableArray([]);
        this.isGroup = ko.observable(false);
        this.unread = ko.observable(0);
        this.image = ko.observable('');
        this.name = ko.observable('');
    }
}

ko.applyBindings(new WhatsApp());