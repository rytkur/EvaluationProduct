import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Product } from 'src/app/models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductDetailService {

  formData: Product= new Product();
  readonly baseURL = "http://localhost:5000/api";
  ProductList: any[] = [];

  constructor(private http: HttpClient) { }

  getProducts():Observable<any[]>{

    return this.http.get<any>(this.baseURL+'/Product/GetProducts?count=10');

  }

  addProduct(request: Product){
    return this.http.post(`${this.baseURL}/Product/Product`, request);
  }

  updateProduct(request: Product){
    return this.http.put(`${this.baseURL}/Product/Product`, request);
  }

  deleteProduct(id: number) {
    return this.http.delete(`${this.baseURL}/Product/${id}`);
  }

  refreshList() {
    this.http.get<any>(this.baseURL+'/Product/Products').subscribe(data => {
      this.ProductList = data;
    });
  }
}
