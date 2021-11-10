import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoldpriceComponent } from './goldprice.component';

describe('GoldpriceComponent', () => {
  let component: GoldpriceComponent;
  let fixture: ComponentFixture<GoldpriceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GoldpriceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GoldpriceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
