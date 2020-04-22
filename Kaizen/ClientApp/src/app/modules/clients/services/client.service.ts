import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Client } from '../models/client';
import { Observable } from 'rxjs';
import { Endpoints } from 'src/app/global/endpoints';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(
    private http: HttpClient
  ) { }

  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(Endpoints.ClientsUrl)
      .pipe(
        tap(_ => console.log('Hola'))
      );
  }

  getClient(id: string): Observable<Client> {
    return this.http.get<Client>(`${Endpoints.ClientsUrl}/${id}`)
      .pipe(
        tap(_ => console.log('Test'))
      );
  }

  saveClient(client: Client): Observable<Client> {
    return this.http.post<Client>(Endpoints.ClientsUrl, client)
      .pipe(
        tap(_ => console.log('Hola'))
      );
  }

  updateClient(client: Client): Observable<Client> {
    return this.http.put<Client>(`${Endpoints.ClientsUrl}/${client.id}`, client)
      .pipe(
        tap(_ => console.log('Hola'))
      );
  }
}
