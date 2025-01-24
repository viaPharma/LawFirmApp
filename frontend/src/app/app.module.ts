import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AttorneysListComponent } from './components/attorneys-list/attorneys-list.component';
import { CommonModule } from '@angular/common';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AttorneyModalComponent } from './components/attorney-form/attorney-modal/attorney-modal.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { DeleteConfirmationDialogComponent } from './components/attorney-form/attorney-modal/confirmation-dialog.component';
import { LayoutComponent } from './components/shared/layout/layout.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './auth.guard';
import { TokenInterceptor } from './token.interceptor';
import { AdminComponent } from './components/admin/admin.component';
import { ErrorComponent } from './components/error/error.component';
@NgModule({
  declarations: [
    AppComponent,
    AttorneysListComponent,
    AttorneyModalComponent,
    DeleteConfirmationDialogComponent,
    LayoutComponent,
    LoginComponent, 
    AdminComponent, ErrorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    CommonModule,
    MatGridListModule,
    MatDialogModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  providers: [
    AuthService,
    AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
