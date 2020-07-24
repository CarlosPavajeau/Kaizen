import { FilterEmployeesPipe } from './filter-employees.pipe';

describe('FilterEmployeesPipe', () => {
  it('create an instance', () => {
    const pipe = new FilterEmployeesPipe();
    expect(pipe).toBeTruthy();
  });
});
