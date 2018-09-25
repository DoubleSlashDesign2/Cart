import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { map } from 'rxjs/operators';

import { Product } from './product';

@Injectable()
export class DataService {

    public products: Product[] = [];

    constructor(private http: HttpClient) {
    }

    loadProducts(): Observable<boolean> {
        return this.http.get('/api/products')
                .pipe(
                    map((data: Product[]) => {
                        this.products = data;
                        return true;
                 }));
    }
}