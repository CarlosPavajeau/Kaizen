import { ObservableWithStatusPipe } from './observable-with-status.pipe';

xdescribe('ObservableWithStatusPipe', () => {
  it('create an instance', () => {
    const pipe = new ObservableWithStatusPipe();
    expect(pipe).toBeTruthy();
  });
});
