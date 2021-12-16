import { __decorate } from "tslib";
import { Component } from "@angular/core";
let Login = class Login {
    constructor(data, router) {
        this.data = data;
        this.router = router;
        this.creds = {
            username: "",
            password: ""
        };
        this.errorMessage = "";
    }
    onLogin() {
        // Call the login service
        alert(this.creds.username);
        this.creds.username += "!";
    }
};
Login = __decorate([
    Component({
        selector: "login",
        templateUrl: "login.component.html",
        styleUrls: []
    })
], Login);
export { Login };
//# sourceMappingURL=login.component.js.map