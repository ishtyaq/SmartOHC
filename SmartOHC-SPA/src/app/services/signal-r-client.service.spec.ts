/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SignalRClientService } from './signal-r-client.service';

describe('Service: SignalRClient', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SignalRClientService]
    });
  });

  it('should ...', inject([SignalRClientService], (service: SignalRClientService) => {
    expect(service).toBeTruthy();
  }));
});
