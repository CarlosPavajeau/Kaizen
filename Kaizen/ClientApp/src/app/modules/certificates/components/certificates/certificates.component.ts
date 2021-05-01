import { Component, OnInit } from '@angular/core';
import { Certificate } from '@modules/certificates/models/certificate';
import { CertificateService } from '@modules/certificates/services/certificate.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Person } from '@shared/models/person';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-certificates',
  templateUrl: './certificates.component.html'
})
export class CertificatesComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  certificates$: Observable<Certificate[]>;
  certificates: Certificate[];

  certificateData: string;

  constructor(private certificatesService: CertificateService) {
  }

  ngOnInit(): void {
    this.loadCertificates();
  }

  loadCertificates(): void {
    const current_person: Person = JSON.parse(localStorage.getItem('current_person'));

    this.certificates$ = this.certificatesService.getClientCertificates(current_person.id);
    this.certificates$.subscribe((certificates: Certificate[]) => {
      this.certificates = certificates;
    });
  }
}
