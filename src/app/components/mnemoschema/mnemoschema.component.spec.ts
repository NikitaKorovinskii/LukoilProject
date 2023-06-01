import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MnemoschemaComponent } from './mnemoschema.component';

describe('MnemoschemaComponent', () => {
  let component: MnemoschemaComponent;
  let fixture: ComponentFixture<MnemoschemaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MnemoschemaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MnemoschemaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
