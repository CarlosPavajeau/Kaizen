import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { UploadDownloadService } from './upload-download.service';

describe('UploadDownloadService', () => {
  let service: UploadDownloadService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ UploadDownloadService ],
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(UploadDownloadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
