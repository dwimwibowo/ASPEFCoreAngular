import { __decorate } from "tslib";
import { Component } from "@angular/core";
let Cart = class Cart {
    constructor(data, router) {
        this.data = data;
        this.router = router;
        this.title = 'Cart';
    }
    onCheckout() {
        if (this.data.loginRequired) {
            // Force Login
            this.router.navigate(["login"]);
        }
        else {
            // Go to checkout
            this.router.navigate(["checkout"]);
        }
    }
};
Cart = __decorate([
    Component({
        selector: "cart-list",
        templateUrl: "cart.component.html",
        styleUrls: []
    })
], Cart);
export { Cart };
//# sourceMappingURL=cart.component.js.map