import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArenasComponent } from './arenas.component';

describe('ArenasComponent', () => {
  let component: ArenasComponent;
  let fixture: ComponentFixture<ArenasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArenasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArenasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
