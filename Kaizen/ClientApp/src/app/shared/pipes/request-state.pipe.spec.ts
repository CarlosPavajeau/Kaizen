import { ServiceRequestStatePipe } from './service-request-state.pipe';

describe('RequestStatePipe', () => {
	it('create an instance', () => {
		const pipe = new ServiceRequestStatePipe();
		expect(pipe).toBeTruthy();
	});
});
