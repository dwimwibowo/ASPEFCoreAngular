import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { ProductList } from './shop/productList.component';
import { Cart } from './shop/cart.component';
import { DataService } from './shared/dataService';

@NgModule({
    declarations: [
        AppComponent,
        ProductList,
        Cart
    ],
    imports: [
        BrowserModule,
        HttpClientModule
    ],
    providers: [
        DataService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
