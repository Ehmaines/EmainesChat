import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomsSideBarComponent } from './rooms-side-bar.component';

describe('RoomsSideBarComponent', () => {
  let component: RoomsSideBarComponent;
  let fixture: ComponentFixture<RoomsSideBarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RoomsSideBarComponent]
    });
    fixture = TestBed.createComponent(RoomsSideBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
