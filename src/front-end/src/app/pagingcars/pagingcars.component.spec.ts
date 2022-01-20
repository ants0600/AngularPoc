import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagingcarsComponent } from './pagingcars.component';

describe('PagingcarsComponent', () => {
  let component: PagingcarsComponent;
  let fixture: ComponentFixture<PagingcarsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagingcarsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PagingcarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
