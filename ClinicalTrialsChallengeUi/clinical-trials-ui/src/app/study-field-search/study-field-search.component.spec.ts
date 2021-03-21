import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudyFieldSearchComponent } from './study-field-search.component';

describe('StudyFieldSearchComponent', () => {
  let component: StudyFieldSearchComponent;
  let fixture: ComponentFixture<StudyFieldSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudyFieldSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudyFieldSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
