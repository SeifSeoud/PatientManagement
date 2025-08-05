import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  PatientAddRequest,
  PatientResponse,
  PatientUpdateRequest,
} from '../Models/patient.model';

@Injectable({ providedIn: 'root' })
export class PatientService {
  private apiUrl = 'https://localhost:62129/api/Patients';

  constructor(private http: HttpClient) {}

  getAllPatients(): Observable<PatientResponse[]> {
    return this.http.get<PatientResponse[]>(this.apiUrl);
  }

  addPatient(patient: PatientAddRequest): Observable<PatientResponse> {
    return this.http.post<PatientResponse>(this.apiUrl, patient);
  }

  updatePatient(
    id: string,
    patient: PatientUpdateRequest
  ): Observable<PatientResponse> {
    return this.http.put<PatientResponse>(`${this.apiUrl}/${id}`, patient);
  }

  deletePatient(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getPatientById(id: string): Observable<PatientResponse> {
    return this.http.get<PatientResponse>(`${this.apiUrl}/${id}`);
  }
}
