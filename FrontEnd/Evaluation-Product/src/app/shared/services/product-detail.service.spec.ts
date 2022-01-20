import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';

import { ProductDetailService } from './product-detail.service';
import { Product } from 'src/app/models/product.model';

describe('ProductDetailService', () => {
  let service: ProductDetailService;
  let httpService: HttpClient;
  let product: Product;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(ProductDetailService);
    httpService = TestBed.inject(HttpClient);
    product = new Product();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should invoke http method and return data when getProducts method is called', () => {
    spyOn(httpService, 'get').and.returnValue(of([{ dummyKey: "DummyValue" }]));

    let result = service.getProducts();

    expect(httpService.get).toHaveBeenCalled();
    expect(result).not.toBeNull();
  });

  it('should invoke http method post when addProduct method is called', () => {
    spyOn(httpService, 'post').and.callThrough();

    let result = service.addProduct(product);

    expect(httpService.post).toHaveBeenCalled();
  });

  it('should invoke http method put when updateProduct method is called', () => {
    spyOn(httpService, 'put').and.callThrough();

    let result = service.updateProduct(product);

    expect(httpService.put).toHaveBeenCalled();
  });

  it('should invoke http method delete when deleteProduct method is called', () => {
    let id:number = 1;

    spyOn(httpService, 'delete').and.callThrough();

    let result = service.deleteProduct(id);

    expect(httpService.delete).toHaveBeenCalled();
  });

  it('should call the server to get all the products', () => {
    let spy = spyOn(httpService, 'get').and.callFake((_: any) => {
			return of();
		});

		service.refreshList();

		expect(spy).toHaveBeenCalled();
	});

  it('should add the products returned from the server to product list', () => {
		let spy = spyOn(httpService, 'get').and.returnValue(of(
      [
        { dummyKey: "DummyValue1" },
        { dummyKey: "DummyValue2" }
      ]
    ));

		service.refreshList();

		expect(service.ProductList.length).toBeGreaterThanOrEqual(2);
	});
});
