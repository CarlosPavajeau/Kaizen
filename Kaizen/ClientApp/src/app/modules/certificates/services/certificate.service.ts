import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CERTIFICATES_API_URL } from '@global/endpoints';
import { Certificate } from '@modules/certificates/models/certificate';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CertificateService {

  constructor(private http: HttpClient) {
  }

  getClientCertificates(clientId: string): Observable<Certificate[]> {
    return this.http.get<Certificate[]>(`${ CERTIFICATES_API_URL }/Client/${ clientId }`);
  }

  getCertificate(id: number): Observable<Certificate> {
    return this.http.get<Certificate>(`${ CERTIFICATES_API_URL }/${ id }`);
  }
}
