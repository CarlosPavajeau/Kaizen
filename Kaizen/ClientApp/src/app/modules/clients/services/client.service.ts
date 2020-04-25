import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Client } from '../models/client';
import { Observable } from 'rxjs';
import { Endpoints } from 'src/app/global/endpoints';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(
    private http: HttpClient
  ) { }

  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(Endpoints.ClientsUrl);
  }

  getClient(id: string): Observable<Client> {
    return this.http.get<Client>(`${Endpoints.ClientsUrl}/${id}`);
  }

  saveClient(client: Client): Observable<Client> {
    return this.http.post<Client>(Endpoints.ClientsUrl, client);
  }

  updateClient(client: Client): Observable<Client> {
    return this.http.put<Client>(`${Endpoints.ClientsUrl}/${client.id}`, client);
  }
}
