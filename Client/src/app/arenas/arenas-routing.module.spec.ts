import { ArenasRoutingModule } from './arenas-routing.module';

describe('ArenasRoutingModule', () => {
  let arenasRoutingModule: ArenasRoutingModule;

  beforeEach(() => {
    arenasRoutingModule = new ArenasRoutingModule();
  });

  it('should create an instance', () => {
    expect(arenasRoutingModule).toBeTruthy();
  });
});
