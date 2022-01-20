import { Component, OnInit } from '@angular/core';
import { NgForm, FormsModule } from '@angular/forms';

import { Product } from 'src/app/models/product.model';
import { ProductDetailService } from 'src/app/shared/services/product-detail.service';

@Component({
  selector: 'po-product-detail-form',
  templateUrl: './product-detail-form.component.html',
  styleUrls: ['./product-detail-form.component.css']
})
export class ProductDetailFormComponent implements OnInit {

  constructor(public service: ProductDetailService) { }

  ngOnInit(): void {
  }
  
  resetForm() {
    this.service.formData = new Product();
  }

  onSubmit(form: FormsModule) {
    
    let ngForm = (form as NgForm);
    var request = new Product;
    request.name = ngForm.value.productName;
    request.price = parseFloat(ngForm.value.price);
    request.quantity = parseFloat(ngForm.value.quantity);    

    if (this.service.formData.id == 0){

      this.insertRecord(request);

    }      
    else{

      request.id = this.service.formData.id;
      this.updateRecord(request);

    }

  }
  
  insertRecord(request: Product){  

    this.service.addProduct(request).subscribe(
      res => {
        this.resetForm();
        this.service.refreshList();
      },
      err => { 
        console.log(err); 
      }
    );

  }

  updateRecord(request: Product){
    
    this.service.updateProduct(request).subscribe(
      res => {
        this.resetForm();
        this.service.refreshList();
      },
      err => { 
        console.log(err); 
      }
    );

  }
}
