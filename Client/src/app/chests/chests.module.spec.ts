import { ChestsModule } from './chests.module';

describe('ChestsModule', () => {
  let chestsModule: ChestsModule;

  beforeEach(() => {
    chestsModule = new ChestsModule();
  });

  it('should create an instance', () => {
    expect(chestsModule).toBeTruthy();
  });
});
