import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

import { Product } from "../shared/product"


@Injectable()
export class DataService {
    public products: Product[] = [];

    constructor(private http: HttpClient) {

    }

    public loadProducts(): Observable<boolean> {
        return this.http.get("api/products")
            .pipe(
                map(
                    (data: any) => {
                        this.products = data;
                        return true;
                    }
                )
            );
    }
}