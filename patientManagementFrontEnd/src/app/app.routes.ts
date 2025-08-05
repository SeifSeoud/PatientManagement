import { Routes } from '@angular/router';
import { PatientListComponent } from './Routes/patient-list/patient-list.component';
import { AuthGuard } from './Services/auth.guard';
import { RegisterComponent } from './Routes/register/register.component';
import { LoginComponent } from './Routes/login/login.component';

export const routes: Routes = [
  { path: '', redirectTo: 'patients', pathMatch: 'full' },
  {
    path: 'patients',
    component: PatientListComponent,
    canActivate: [AuthGuard],
    data: { title: 'Patient' },
  },
  { path: 'login', component: LoginComponent, data: { title: 'Login' } },
  {
    path: 'register',
    component: RegisterComponent,
    data: { title: 'Register' },
  },
  { path: '**', redirectTo: 'patients' },
];
