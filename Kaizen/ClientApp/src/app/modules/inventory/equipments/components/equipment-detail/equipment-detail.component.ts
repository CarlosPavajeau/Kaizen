import { Component, OnInit } from '@angular/core';
import { EquipmentService } from '../../services/equipment.service';
import { ActivatedRoute } from '@angular/router';
import { Equipment } from '../../models/equipment';

@Component({
  selector: 'app-equipment-detail',
  templateUrl: './equipment-detail.component.html',
  styleUrls: [ './equipment-detail.component.css' ]
})
export class EquipmentDetailComponent implements OnInit {
  equipment: Equipment;

  constructor(private equipmentService: EquipmentService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.equipmentService.getEquipment(code).subscribe((equipment) => {
      this.equipment = equipment;
    });
  }
}
