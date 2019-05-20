import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserProjectColumnComponent } from './user-project-column.component';

describe('UserProjectColumnComponent', () => {
  let component: UserProjectColumnComponent;
  let fixture: ComponentFixture<UserProjectColumnComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserProjectColumnComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserProjectColumnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
