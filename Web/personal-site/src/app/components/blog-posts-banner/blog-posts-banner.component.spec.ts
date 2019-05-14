import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogPostsBannerComponent } from './blog-posts-banner.component';

describe('BlogPostsBannerComponent', () => {
  let component: BlogPostsBannerComponent;
  let fixture: ComponentFixture<BlogPostsBannerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlogPostsBannerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlogPostsBannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
