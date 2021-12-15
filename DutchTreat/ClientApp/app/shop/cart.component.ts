import { Component } from "@angular/core"
import { DataService } from "../shared/dataService";

@Component({
    selector: "cart-list",
    templateUrl: "cart.component.html",
    styleUrls: []
})

export class Cart {

    public title: string = 'Cart';

    constructor(public data: DataService) {

    }
}