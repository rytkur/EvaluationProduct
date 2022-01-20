import { Component, OnInit } from '@angular/core';

import { Product } from 'src/app/models/product.model';
import { ProductDetailService } from 'src/app/shared/services/product-detail.service';

@Component({
  selector: 'po-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  ProductList: any[] = [];

  constructor(public service: ProductDetailService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Product) {
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record?')) {
      
      this.service.deleteProduct(id).subscribe(
        res => {
          this.service.refreshList();
        },
        err => { 
          console.log(err); 
        }
      );

    }
  }

}
