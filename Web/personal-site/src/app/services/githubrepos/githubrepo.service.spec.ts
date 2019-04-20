import { TestBed } from '@angular/core/testing';

import { GithubrepoService } from './githubrepo.service';

describe('GithubrepoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GithubrepoService = TestBed.get(GithubrepoService);
    expect(service).toBeTruthy();
  });
});
