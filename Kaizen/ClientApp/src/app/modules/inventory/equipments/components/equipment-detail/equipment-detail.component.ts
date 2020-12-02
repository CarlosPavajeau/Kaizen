import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { EquipmentService } from '@modules/inventory/equipments/services/equipment.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-equipment-detail',
  templateUrl: './equipment-detail.component.html',
  styleUrls: [ './equipment-detail.component.scss' ]
})
export class EquipmentDetailComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  equipment$: Observable<Equipment>;

  constructor(private equipmentService: EquipmentService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.equipment$ = this.equipmentService.getEquipment(code);
  }
}
