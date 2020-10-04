import { HttpClient } from '@angular/common/http';
import { Injectable, Optional } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckEntityExistsService {
  private readonly CHECK_API_URL: string;
  constructor(private http: HttpClient, @Optional() api_url: string) {
    this.CHECK_API_URL = `${api_url}/CheckExists`;
  }

  checkEntityExists(id: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.CHECK_API_URL}/${id}`);
  }
}
