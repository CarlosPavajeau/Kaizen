import { ActivityState } from '@app/modules/activity-schedule/models/activity-state';
import { ActivityStatePipe } from './activity-state.pipe';

describe('ActivityStatePipe', () => {
  let pipe: ActivityStatePipe;

  beforeEach(() => {
    pipe = new ActivityStatePipe();
  });

  it('create an instance', () => {
    expect(pipe).toBeTruthy();
  });

  it('test with ActivityState.Pending', () => {
    const result = pipe.transform(ActivityState.Pending);
    expect(result).toEqual('Pendiente');
  });

  it('test with ActivityState.PendingSuggestedDate', () => {
    const result = pipe.transform(ActivityState.PendingSuggestedDate);
    expect(result).toEqual('Pendiente a confirmación de fecha');
  });

  it('test with ActivityState.Accepted', () => {
    const result = pipe.transform(ActivityState.Accepted);
    expect(result).toEqual('Aceptada');
  });

  it('test with ActivityState.Rejected', () => {
    const result = pipe.transform(ActivityState.Rejected);
    expect(result).toEqual('Cancelada/Rechazada');
  });

  it('test with ActivityState.Applied', () => {
    const result = pipe.transform(ActivityState.Applied);
    expect(result).toEqual('Aplicada');
  });

  it('test with error', () => {
    const result = pipe.transform(5);
    expect(result).toEqual('Error, estado de actividad invalido');
  });
});
