import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCarsByEngine(engine: string): Observable<any[]> {
    const params = { engine };
    return this.http.get<any[]>(`${this.baseUrl}/AvailableCar/Get_Car_By_Engine`, { params });
  }
}
