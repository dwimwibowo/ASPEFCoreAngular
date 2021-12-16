import { Component } from "@angular/core"
import { Router } from "@angular/router";
import { DataService } from "../shared/dataService";

@Component({
    selector: "cart-list",
    templateUrl: "cart.component.html",
    styleUrls: []
})

export class Cart {

    public title: string = 'Cart';

    constructor(public data: DataService, private router: Router) {

    }

    onCheckout() {
        if (this.data.loginRequired) {
            // Force Login
            this.router.navigate(["login"]);
        }
        else
        {
            // Go to checkout
            this.router.navigate(["checkout"]);
        }
    }
}