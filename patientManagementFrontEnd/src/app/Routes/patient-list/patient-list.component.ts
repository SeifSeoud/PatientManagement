import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {
  PatientAddRequest,
  PatientResponse,
  PatientUpdateRequest,
} from '../../Models/patient.model';
import { PatientService } from '../../Services/patient.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterLink,
    DatePipe,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.scss',
})
export class PatientListComponent implements OnInit {
  patients: PatientResponse[] = [];
  editFormDate: string = '';
  newPatient: PatientAddRequest = this.getEmptyPatient();
  editingPatientId: string | null = null;
  updatePatientData: PatientUpdateRequest = this.getEmptyPatient();

  constructor(private patientService: PatientService) {}

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients() {
    this.patientService
      .getAllPatients()
      .subscribe((data) => (this.patients = data));
  }

  addPatient() {
    this.patientService.addPatient(this.newPatient).subscribe(() => {
      this.loadPatients();
      this.newPatient = this.getEmptyPatient();
    });
  }

  deletePatient(id: string) {
    this.patientService.deletePatient(id).subscribe(() => {
      this.patients = this.patients.filter((p) => p.patientID !== id);
    });
  }

  startEdit(patient: PatientResponse) {
    this.editingPatientId = patient.patientID;
    const [firstName, ...rest] = (patient.fullName || '').split(' ');

    // Parse contact info
    let phone = '';
    let email = '';
    if (patient.contactInfo) {
      const parts = patient.contactInfo.split('|').map((p) => p.trim());
      phone = parts[0] || '';
      email = parts.length > 1 ? parts[1] : '';
    }

    // Robust date handling
    let formattedDate = '';
    if (patient.dateOfBirth) {
      // First ensure we have a Date object
      let dob: Date;

      if (patient.dateOfBirth instanceof Date) {
        dob = patient.dateOfBirth;
      } else if (typeof patient.dateOfBirth === 'string') {
        // Handle both ISO format (YYYY-MM-DDTHH:MM:SS) and date-only strings
        const dateString = patient.dateOfBirth.split('T')[0];
        const [year, month, day] = dateString.split('-').map(Number);
        dob = new Date(year, month - 1, day);
      } else {
        // Fallback to current date if format is unexpected
        dob = new Date();
      }

      // Format as YYYY-MM-DD for the input
      const year = dob.getFullYear();
      const month = (dob.getMonth() + 1).toString().padStart(2, '0');
      const day = dob.getDate().toString().padStart(2, '0');
      formattedDate = `${year}-${month}-${day}`;
    }

    this.updatePatientData = {
      firstName: firstName || '',
      lastName: rest.join(' ') || '',
      dateOfBirth: formattedDate,
      gender: patient.gender || '',
      address: patient.address || '',
      phoneNumber: phone,
      email: email,
    };
  }

  saveEdit() {
    if (this.editingPatientId) {
      this.patientService
        .updatePatient(this.editingPatientId, this.updatePatientData)
        .subscribe(() => {
          this.loadPatients();
          this.editingPatientId = null;
        });
    }
  }

  cancelEdit() {
    this.editingPatientId = null;
  }

  getFakeDOBFromAge(age: number): string {
    const year = new Date().getFullYear() - age;
    return new Date(year, 0, 1).toISOString();
  }

  getEmptyPatient(): any {
    return {
      firstName: '',
      lastName: '',
      dateOfBirth: '',
      gender: '',
      address: '',
      phoneNumber: '',
      email: '',
    };
  }
}
