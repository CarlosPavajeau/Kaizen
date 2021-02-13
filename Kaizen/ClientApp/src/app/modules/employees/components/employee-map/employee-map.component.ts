import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MapInfoWindow, MapMarker } from '@angular/google-maps';
import { environment } from '@base/environments/environment';
import { EmployeeLocation } from '@modules/employees/models/employee-location';
import { EmployeeSignalrService } from '@modules/employees/services/employee-signalr.service';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'app-employee-map',
  templateUrl: './employee-map.component.html',
  styleUrls: [ './employee-map.component.scss' ]
})
export class EmployeeMapComponent implements OnInit, OnDestroy {
  apiLoaded: Observable<boolean>;

  readonly mapOptions: google.maps.MapOptions = {
    center: { lat: 10.4453036, lng: -73.2646046 },
    zoom: 15
  };

  size: google.maps.Size;
  markerOptions: google.maps.MarkerOptions;

  employeeLocations: EmployeeLocation[] = [];

  constructor(private http: HttpClient, private employeeSignalR: EmployeeSignalrService) {
  }

  ngOnInit(): void {
    this.loadGoogleMapsApi();

    this.employeeSignalR.startConnection();
    this.employeeSignalR.addOnUpdateEmployeeLocation();

    this.employeeSignalR.onUpdateEmployeeLocation.subscribe((employeeLocation: EmployeeLocation) => {
      const index = this.employeeLocations.findIndex((e) => e.id == employeeLocation.id);
      if (index != -1) {
        this.employeeLocations[index] = employeeLocation;
      } else {
        this.employeeLocations.push(employeeLocation);
      }
    });
  }

  ngOnDestroy(): void {
    this.employeeSignalR.onUpdateEmployeeLocation.unsubscribe();
  }

  private loadGoogleMapsApi(): void {
    this.apiLoaded = this.http.jsonp(`https://maps.googleapis.com/maps/api/js?key=${ environment.google_maps_api_key }`, 'callback')
      .pipe(
        map(() => true),
        catchError(err => {
          console.log(err);
          return of(false);
        })
      );

    this.apiLoaded.subscribe(result => {
      if (result) {
        this.size = new google.maps.Size(20, 20);
        this.markerOptions = {
          draggable: false,
          icon: {
            url: '../../../assets/images/engineering.svg',
            size: this.size,
            scaledSize: this.size
          },
        };
      }
    });
  }

  openInfoWindow(marker: MapMarker, infoWindow: MapInfoWindow): void {
    infoWindow.open(marker);
  }
}
