import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { EquipmentService } from '@modules/inventory/equipments/services/equipment.service';

@Component({
	selector: 'app-equipments',
	templateUrl: './equipments.component.html',
	styleUrls: [ './equipments.component.css' ]
})
export class EquipmentsComponent implements OnInit, AfterViewInit {
	equipments: Equipment[];
	dataSource: MatTableDataSource<Equipment> = new MatTableDataSource<Equipment>(this.equipments);
	displayedColumns: string[] = [ 'code', 'name', 'maintenanceDate', 'amount', 'price', 'options' ];
	@ViewChild(MatPaginator, { static: true })
	paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;

	constructor(private equipmentService: EquipmentService) {}

	ngOnInit(): void {
		this.loadEquipments();
	}

	private loadEquipments(): void {
		this.equipmentService.getEquipments().subscribe((equipments) => {
			this.equipments = equipments;
			this.dataSource.data = this.equipments;
		});
	}

	ngAfterViewInit(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}
}
