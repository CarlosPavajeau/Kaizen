import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Client } from '@modules/clients/models/client';
import { Observable } from 'rxjs';
import { CLIENTS_API_URL } from '@global/endpoints';

@Injectable({
	providedIn: 'root'
})
export class ClientService {
	constructor(private http: HttpClient) {}

	getClients(): Observable<Client[]> {
		return this.http.get<Client[]>(CLIENTS_API_URL);
	}

	getClient(id: string): Observable<Client> {
		return this.http.get<Client>(`${CLIENTS_API_URL}/${id}`);
	}

	getClientId(userId: string): Observable<string> {
		return this.http.get<string>(`${CLIENTS_API_URL}/ClientId/${userId}`);
	}

	getClientRequests(): Observable<Client[]> {
		return this.http.get<Client[]>(`${CLIENTS_API_URL}/Requests`);
	}

	saveClient(client: Client): Observable<Client> {
		return this.http.post<Client>(CLIENTS_API_URL, client);
	}

	updateClient(client: Client): Observable<Client> {
		return this.http.put<Client>(`${CLIENTS_API_URL}/${client.id}`, client);
	}
}
