import { Component, OnInit } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Product } from "../shared/product";

@Component({
  selector: "product-list",
  templateUrl: "productList.component.html",
  styleUrls: ["productList.component.css"]
})
export class ProductList implements OnInit {
    
    public products: Product[] = [];

    constructor(private dataService: DataService) {
    }

    ngOnInit(): void {
        this.dataService
            .loadProducts()
            .subscribe(success => {
                if (success) {
                    this.products = this.dataService.products;
                }
            })
    }

    addProduct(product: Product) {
        this.dataService.AddToOrder(product);
    }
}