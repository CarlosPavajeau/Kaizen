import { ClientStatePipe } from './client-state.pipe';

describe('ClientStatePipe', () => {
  it('create an instance', () => {
    const pipe = new ClientStatePipe();
    expect(pipe).toBeTruthy();
  });
});
