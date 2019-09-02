import { TestBed, inject } from '@angular/core/testing';

import { ArenaService } from './arena.service';

describe('ArenaService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ArenaService]
    });
  });

  it('should be created', inject([ArenaService], (service: ArenaService) => {
    expect(service).toBeTruthy();
  }));
});
