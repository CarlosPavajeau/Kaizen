import { InvoiceStatePipe } from './invoice-state.pipe';

describe('InvoiceStatePipe', () => {
  it('create an instance', () => {
    const pipe = new InvoiceStatePipe();
    expect(pipe).toBeTruthy();
  });
});
