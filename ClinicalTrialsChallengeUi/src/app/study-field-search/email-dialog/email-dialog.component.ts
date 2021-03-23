import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormControl, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'ct-email-dialog',
  templateUrl: './email-dialog.component.html',
  styleUrls: ['./email-dialog.component.scss']
})
export class EmailDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<EmailDialogComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: EmailDialogInput
  ) { }
  
  form = this.fb.group({
    'email': ['', [Validators.email, Validators.required]],
    'name': ['']
  });

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  submit(): void {
    if (!this.form.valid){
      this.form.markAllAsTouched();
      return;
    }

    const result: EmailDialogResult = <EmailDialogResult>this.form.value;
    this.dialogRef.close(result);
  }
}

export interface EmailDialogResult {
  email: string;
  name: string
}

export interface EmailDialogInput {
  title: string;
  prompt: string;
}
