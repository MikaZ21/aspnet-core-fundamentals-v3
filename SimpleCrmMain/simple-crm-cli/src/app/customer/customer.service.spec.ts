import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CustomerService } from './customer.service';

fdescribe('CustomerService', () => {
  let service: CustomerService;
  let injector: TestBed;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      providers: [ CustomerService ]
    });
    injector = getTestBed();
    service = injector.inject(CustomerService);
    httpMock = injector.inject(HttpTestingController);
  });

  afterEach (() => {
    httpMock.verify();
  })

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
