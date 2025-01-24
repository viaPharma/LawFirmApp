import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttorneysListComponent } from './components/attorneys-list/attorneys-list.component';


const routes: Routes = [
  { path: 'attorneys', component: AttorneysListComponent },
  { path: 'add-attorney', component: AttorneysListComponent },
  { path: '', redirectTo: '/attorneys', pathMatch: 'full' }, // Default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
