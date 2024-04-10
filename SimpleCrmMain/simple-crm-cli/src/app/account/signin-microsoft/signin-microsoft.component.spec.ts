import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SigninMicrosoftComponent } from './signin-microsoft.component';

describe('SigninMicrosoftComponent', () => {
  let component: SigninMicrosoftComponent;
  let fixture: ComponentFixture<SigninMicrosoftComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SigninMicrosoftComponent]
    });
    fixture = TestBed.createComponent(SigninMicrosoftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
