import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckEntityExistsService {
  readonly CHECK_API_URL: string;
  constructor(private http: HttpClient, api_url: string) {
    this.CHECK_API_URL = `${api_url}/CheckExists`;
  }

  checkEntityExists(id: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.CHECK_API_URL}/${id}`);
  }
}
