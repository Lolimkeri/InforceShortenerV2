import { TestBed } from '@angular/core/testing';

import { TextProviderService } from './text-provider.service';

describe('TextProviderService', () => {
  let service: TextProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TextProviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
