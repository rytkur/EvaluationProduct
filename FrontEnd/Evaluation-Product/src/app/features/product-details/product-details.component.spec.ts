import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { ProductDetailService } from 'src/app/shared/services/product-detail.service';
import { Product } from 'src/app/models/product.model';
import { ProductDetailsComponent } from './product-details.component';

describe('ProductDetailsComponent', () => {
  let component: ProductDetailsComponent;
  let fixture: ComponentFixture<ProductDetailsComponent>;
  let service: ProductDetailService;
  let product: Product;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductDetailsComponent ],
      imports: [HttpClientTestingModule]
    })
    .compileComponents();

    service = TestBed.inject(ProductDetailService);
    product = new Product();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('ngOnInit should call the server to get all product', () => {
		
		const spy = spyOn(service, 'refreshList').and.callThrough();

		component.ngOnInit();

		expect(spy).toHaveBeenCalled();
	});

  it('populateForm should assign seleted product to form', () => {
		
    product.id = 1;
    product.name = "UnitTest";

		component.populateForm(product);

		expect(service.formData.id).toBe(1);
    expect(service.formData.name).toBe("UnitTest");
	});

  it('onDelete should call the server to delete a product if the user confirms', () => {
		spyOn(window, 'confirm').and.returnValue(true);
		const spy = spyOn(service, 'deleteProduct').and.callThrough();

		component.onDelete(1);

		expect(spy).toHaveBeenCalledWith(1);
	});

  it('onDelete should not call the server to delete a product if the user cancel', () => {
		spyOn(window, 'confirm').and.returnValue(false);
		const spy = spyOn(service, 'deleteProduct').and.callThrough();

		component.onDelete(1);

		expect(spy).not.toHaveBeenCalledWith(1);
	});
});
