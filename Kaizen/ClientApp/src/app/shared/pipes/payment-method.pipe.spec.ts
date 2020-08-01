import { PaymentMethodPipe } from './payment-method.pipe';

describe('PaymentMethodPipe', () => {
  it('create an instance', () => {
    const pipe = new PaymentMethodPipe();
    expect(pipe).toBeTruthy();
  });
});
