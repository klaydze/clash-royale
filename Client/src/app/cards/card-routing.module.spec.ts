import { CardRoutingModule } from './card-routing.module';

describe('CardRoutingModule', () => {
  let cardRoutingModule: CardRoutingModule;

  beforeEach(() => {
    cardRoutingModule = new CardRoutingModule();
  });

  it('should create an instance', () => {
    expect(cardRoutingModule).toBeTruthy();
  });
});
