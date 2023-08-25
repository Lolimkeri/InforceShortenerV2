import { TestBed } from '@angular/core/testing';

import { UrlRecordService } from './url-record.service';

describe('UrlRecordService', () => {
  let service: UrlRecordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UrlRecordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
