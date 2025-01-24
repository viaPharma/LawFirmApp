import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttorneysListComponent } from './components/attorneys-list/attorneys-list.component';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { ErrorComponent } from './components/error/error.component';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'attorneys',
    component: AttorneysListComponent,
    canActivate: [AuthGuard],
    data: { expectedRole: ['Admin'] }, // Allow both Admin role
  },
  {
    path: 'admin/manage-users',
    component: AdminComponent, // Dedicated "Manage Users" page
    canActivate: [AuthGuard],
    data: { expectedRole: 'Admin' }, // Only Admins can access
  },
  { path: 'error', component: ErrorComponent }, // Error page route
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
