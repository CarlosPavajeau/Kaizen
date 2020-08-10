import { Component, OnInit } from '@angular/core';
import { ServiceService } from '@modules/services/services/service.service';
import { Service } from '@modules/services/models/service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-service-detail',
  templateUrl: './service-detail.component.html',
  styleUrls: [ './service-detail.component.css' ]
})
export class ServiceDetailComponent implements OnInit {
  service: Service;

  constructor(private serviceService: ServiceService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.serviceService.getService(code).subscribe((service) => {
      this.service = service;
      console.log(service);
    });
  }
}
