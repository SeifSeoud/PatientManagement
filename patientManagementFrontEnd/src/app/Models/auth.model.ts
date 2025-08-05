export interface RegisterRequest {
  email: string;
  password: string;
  personName: string;
  gender: 'Male' | 'Female';
}
export interface LoginRequest {
  email?: string;
  password?: string;
}
export interface AuthenticationResponse {
  userID: string;
  email?: string;
  personName?: string;
  gender?: string;
  token?: string;
  success: boolean;
}
