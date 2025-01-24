import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-attorney-modal',
  templateUrl: './attorney-modal.component.html',
  styleUrls: ['./attorney-modal.component.less']
})
export class AttorneyModalComponent {
  // Initialize attorney with default or existing data
  attorney = this.data?.attorney || { id: null, name: '', email: '', phoneNumber: '' };

  constructor(
    public dialogRef: MatDialogRef<AttorneyModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  // Close modal without saving
  onCancel(): void {
    this.dialogRef.close();
  }

  // Save and close modal
  onSave(): void {
    this.dialogRef.close(this.attorney);
  }
}
