import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';

@Component({
    selector: "checkout",
    templateUrl: "checkout.component.html",
    styleUrls: ['checkout.component.css']
})
export class Checkout {

    public title: string = 'Confirm Order';

    constructor(public data: DataService) {
    }

    onCheckout() {
    // TODO
    alert("Doing checkout");
    }
}