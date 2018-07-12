class WhatsApp {
    constructor() {
        this.jwtToken = '';
        this.comment = ko.observable('');
        this.me = ko.observable(new Me());
        this.activeChat = ko.observable();
        this.chats = ko.observableArray([]);
        this._connection = new signalR.HubConnectionBuilder()
            .withUrl('/whatsapp', { accessTokenFactory: () => this.jwtToken.rawData })
            .configureLogging(signalR.LogLevel.Trace)
            .build();
        let logon = false;
        this.promiseWhile('', token => token === '', this.login)
            .then(token => {
                fetch("/api/whatsapp/users/me", {
                    headers: new Headers({ 'Authorization': `Bearer ${this.jwtToken.rawData}` })
                })
                .then(res => res.json())
                .then(data => {
                    this.me(data.me);
                    this.chats(data.chats);
                    this.activeChat(new Chat(data.chats[0]));
                    this.connect();
                });
            });
    }

    senderClass(userId, css) {
        return ko.computed(() => this.me().id === userId ? `${css}sender` : `${css}receiver`);
    }

    connect() {
        this._connection.on("NewMessage", message => {
            if (this.activeChat().id() === message.chatId) {
                var messages = this.activeChat().messages();
                messages.push(message);
                this.activeChat().messages(messages);
            }
            else {
                const chat = this.chats().find(c => c.id === message.chatId);
                chat.messages.push(message);
            }
        });

        this._connection.start().catch(error => console.error(error));
    }

    selectChat(data) {
        this.activeChat(new Chat(data));
    }

    send() {
        console.log('send');
        this._connection.invoke("SendMessage", {
            chatId: this.activeChat().id(),
            userId: this.me().id,
            userName: this.me().name,
            text: this.comment()
        });
        this.comment('');
    }

    openNewChat(data, event) {
        document.getElementsByClassName("side-two")[0].style.left = "0";
    }

    closeNewChat(data, event) {
        document.getElementsByClassName("side-two")[0].style.left = "-100%";
    }

    login() {
        let name = prompt('Choose a name [Vincent,Aiden,Mike,Erica,Ginger,Tracy,Christian,Monica,Dean,Peyton]')
        return fetch(`/api/whatsapp/token?name=${name}`)
            .then(res => res.json())
            .then(token => {
                return new Promise((resolve, reject) => {
                    resolve(token);
                });
            })
            .catch(() => {
                return new Promise((resolve, reject) => {
                    resolve('');
                });
            });
    }

    promiseWhile(data, condition, action) {
        var whilst = (data) => {
            this.jwtToken = data;
            console.log(data);
            return condition(data) ?
                action(data).then(whilst) :
                Promise.resolve(data);
        }
        return whilst(data);
    };
}

class Me {
    constructor() {
        this.id = ko.observable(-1);
        this.name = ko.observable('');
        this.avatar = ko.observable('');
    }
}

class Chat {
    constructor(chat) {
        this.id = ko.observable(chat.id);
        this.participants = ko.observable(chat.participants);
        this.messages = ko.observableArray(chat.messages);
        this.isGroup = ko.observable(chat.isgroup);
        this.unread = ko.observable(0);
        this.image = ko.observable(chat.image);
        this.name = ko.observable(chat.name);
    }
}

ko.applyBindings(new WhatsApp());