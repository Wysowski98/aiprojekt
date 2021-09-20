import { TestBed } from '@angular/core/testing';

import { TrefleService } from './trefle.service';

describe('TrefleService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TrefleService = TestBed.get(TrefleService);
    expect(service).toBeTruthy();
  });
});
