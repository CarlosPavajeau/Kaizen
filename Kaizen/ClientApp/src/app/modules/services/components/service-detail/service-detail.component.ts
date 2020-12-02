import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Service } from '@modules/services/models/service';
import { ServiceService } from '@modules/services/services/service.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-service-detail',
  templateUrl: './service-detail.component.html',
  styleUrls: [ './service-detail.component.scss' ]
})
export class ServiceDetailComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  service$: Observable<Service>;

  constructor(private serviceService: ServiceService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.service$ = this.serviceService.getService(code);
  }
}
