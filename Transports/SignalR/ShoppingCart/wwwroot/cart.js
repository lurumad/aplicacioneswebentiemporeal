const Status = {
    Submitted: 1,
    AwaitingValidation: 2,
    StockConfirmed: 3,
    Paid: 4,
    Shipped: 5
};
const formatCurrency = (value) => "€" + value.toFixed(2);

class CartLine {

    constructor() {
        this.category = ko.observable();
        this.product = ko.observable();
        this.quantity = ko.observable(1);
        this.subtotal = ko.computed(() => {
            return this.product() ? this.product().price * parseInt("0" + this.quantity(), 10) : 0;
        });

        this.category.subscribe(() => {
            this.product(undefined);
        });
    }
}

class Cart {

    constructor() {
        this.orderStatus = ko.observable(0);
        this.showShoppingCart = ko.observable(true);
        this.showOderStatusTracking = ko.observable(false);
        this.lines = ko.observableArray([new CartLine()]);
        this.grandTotal = ko.computed(() => {
            let total = 0;
            this.lines().map((line) => { total += line.subtotal(); });
            return total;
        });
        this.removeLine = this.removeLine.bind(this);
    }

    completed(status) {
        return ko.computed(() => this.orderStatus() >= status ? "progtrckr-done" : "progtrckr-todo");
    }
    addLine() { this.lines.push(new CartLine()); }
    removeLine(line) { this.lines.remove(line); }
    save() {
        const items = this.lines().map((line) => {
            return line.product() ? {
                productName: line.product().name,
                quantity: line.quantity(),
                price: line.product().price
            } : undefined;
        });

        this.showShoppingCart(false);
        this.showOderStatusTracking(true);

        const connection = new signalR.HubConnectionBuilder()
            .withUrl('/orderstatus')
            .configureLogging(signalR.LogLevel.Information)
            .build();

        connection.on('UpdateOrderStatus', (status) => {
            this.orderStatus(status);
        });

        connection.on("OnCheckoutDone", (basket) => {
            console.log(basket);
        });

        connection.start()
            .then(() => {
                connection.invoke("Checkout", { items } )
                    .catch(error => console.error(error));
            })
            .catch(error => console.error(error));
    }
}

ko.applyBindings(new Cart());