import { ArenasModule } from './arenas.module';

describe('ArenasModule', () => {
  let arenasModule: ArenasModule;

  beforeEach(() => {
    arenasModule = new ArenasModule();
  });

  it('should create an instance', () => {
    expect(arenasModule).toBeTruthy();
  });
});
