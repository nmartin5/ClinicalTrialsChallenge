import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'ct-contact-request',
  templateUrl: './contact-request.component.html',
  styleUrls: ['./contact-request.component.scss']
})
export class ContactRequestComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<ContactRequestComponent>
  ) { }
  
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  submit(): void {
    if (!this.emailFormControl.valid){
      this.emailFormControl.markAllAsTouched();
      return;
    }
    this.dialogRef.close(this.emailFormControl.value);
  }
}
