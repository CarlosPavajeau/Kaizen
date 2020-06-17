import { RequestStatePipe } from './request-state.pipe';

describe('RequestStatePipe', () => {
  it('create an instance', () => {
    const pipe = new RequestStatePipe();
    expect(pipe).toBeTruthy();
  });
});
