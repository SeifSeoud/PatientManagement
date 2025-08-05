export interface PatientResponse {
  patientID: string;
  fullName: string | null;
  age: number;
  address: string;
  dateOfBirth: string | Date;
  gender: string | null;
  contactInfo: string | null;
  isActive: boolean | null;
}

export interface PatientAddRequest {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  address: string;
  phoneNumber: string;
  email: string;
}

export interface PatientUpdateRequest extends PatientAddRequest {}
