import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule, NgForm } from '@angular/forms';

import { ProductDetailService } from 'src/app/shared/services/product-detail.service';
import { Product } from 'src/app/models/product.model';
import { ProductDetailFormComponent } from './product-detail-form.component';

describe('ProductDetailFormComponent', () => {
  let component: ProductDetailFormComponent;
  let fixture: ComponentFixture<ProductDetailFormComponent>;
  let service: ProductDetailService;
  let product: Product;
  let form: FormsModule;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductDetailFormComponent ],
      imports: [HttpClientTestingModule, FormsModule]
    })
    .compileComponents();

    service = TestBed.inject(ProductDetailService);
    product = new Product();
    form = TestBed.inject(FormsModule);
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductDetailFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('resetForm should reset form', () => {
		
    component.resetForm();

		expect(service.formData.id).toEqual(0);
    expect(service.formData.name).toEqual("");
    expect(service.formData.price).toEqual(0);
    expect(service.formData.quantity).toEqual(0);

	});

  it('insertRecord should call service to add product', () => {
		
		const spy = spyOn(service, 'addProduct').and.callThrough();

		component.insertRecord(product);

		expect(spy).toHaveBeenCalled();
	});

  it('updateRecord should call service to update product', () => {
		
		const spy = spyOn(service, 'updateProduct').and.callThrough();

		component.updateRecord(product);

		expect(spy).toHaveBeenCalled();
	});

});
