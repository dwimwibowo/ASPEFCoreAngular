import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from "../shared/dataService";

@Component({
    selector: "login",
    templateUrl: "login.component.html",
    styleUrls: []
})

export class Login {

    constructor(private data: DataService, private router: Router) {

    }

    public creds = {
        username: "",
        password: ""
    };

    errorMessage: string = "";

    onLogin() {
        // Call the login service
        alert(this.creds.username);

        this.creds.username += "!";
    }

}