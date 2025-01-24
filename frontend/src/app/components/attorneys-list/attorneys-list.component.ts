import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { AttorneyService, Attorney } from '../../services/attorney.service';
import { AttorneyModalComponent } from '../attorney-form/attorney-modal/attorney-modal.component';
import { DeleteConfirmationDialogComponent } from '../attorney-form/attorney-modal/confirmation-dialog.component';

@Component({
  selector: 'app-attorneys-list',
  templateUrl: './attorneys-list.component.html',
  styleUrls: ['./attorneys-list.component.less'],
})
export class AttorneysListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'email', 'phoneNumber', 'actions'];
  dataSource = new MatTableDataSource<Attorney>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private attorneyService: AttorneyService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.attorneyService.getAttorneys().subscribe((data: Attorney[]) => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  // Load attorneys from API
  loadAttorneys(): void {
    this.attorneyService.getAttorneys().subscribe(data => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  // Open the modal for adding/editing an attorney
  openModal(attorney: Attorney | null = null): void {
    const dialogRef = this.dialog.open(AttorneyModalComponent, {
      width: '400px',
      data: { attorney },
    });

    dialogRef.afterClosed().subscribe((result: Attorney | undefined) => {
      if (result) {
        if (attorney) {
          // Update existing attorney
          this.attorneyService.updateAttorney(result).subscribe(() => {
            this.loadAttorneys();
          });
        } else {
          result.id = 0;
          // Add new attorney
          this.attorneyService.addAttorney(result).subscribe({
            next: (response) => {
              console.log('Attorney added successfully', response);
              this.loadAttorneys();
            },
            error: (error) => {
              console.error('Error adding attorney:', error.error.errors);
              alert(`Validation Errors: ${JSON.stringify(error.error.errors)}`);
            },
          });
        }
      }
    });
  }

  // Confirm and delete an attorney
  deleteAttorney(attorney: Attorney): void {
    const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, {
      width: '400px',
      data: { name: attorney.name },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.attorneyService.deleteAttorney(attorney.id).subscribe(() => {
          this.loadAttorneys();
        });
      }
    });
  }
}
