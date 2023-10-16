import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AppIconsService } from './app-icons.service';

fdescribe('AppIconsService', () => {
  let service: AppIconsService;
  let injector: TestBed;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      providers: [ AppIconsService ]
    });
    injector = getTestBed();
    service = injector.inject(AppIconsService);
    httpMock = injector.inject(HttpTestingController);
  });

  afterEach (() => {
    httpMock.verify();
  })

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
