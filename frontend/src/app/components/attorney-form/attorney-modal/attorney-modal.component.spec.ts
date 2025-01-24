import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttorneyModalComponent } from './attorney-modal.component';

describe('AttorneyModalComponent', () => {
  let component: AttorneyModalComponent;
  let fixture: ComponentFixture<AttorneyModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AttorneyModalComponent]
    });
    fixture = TestBed.createComponent(AttorneyModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
