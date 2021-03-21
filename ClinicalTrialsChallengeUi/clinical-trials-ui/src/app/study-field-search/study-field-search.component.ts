import { Component, OnInit } from '@angular/core';
import { IFullStudyClient, FullStudyClient, Status, StatusClient } from '../client-lib/client';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'ct-study-field-search',
  templateUrl: './study-field-search.component.html',
  styleUrls: ['./study-field-search.component.scss']
})
export class StudyFieldSearchComponent implements OnInit {

  searchRequest: FormGroup = this.fb.group({
    keywords: this.fb.array([
      this.fb.control('', Validators.required)
    ]),
    location: [''],
    status: [null],
    gender: [''],
    dateOfBirth: ['']
  });

  statuses!: Observable<Status[]>;  
  constructor(private client: FullStudyClient, private statusClient: StatusClient, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.statuses = this.statusClient.getStatuses();
  }

  addKeyword() {
    if (this.keywords.invalid) {
      this.keywords.markAllAsTouched();
      return;
    }
    this.keywords.push(this.fb.control('', Validators.required));
    console.log(this.searchRequest);
  }

  removeKeyword(index: number){
    if (this.keywords.length == 1) {
      this.keywords.markAllAsTouched();
      return;
    }
    this.keywords.removeAt(index);
  }

  get keywords() {
    return this.searchRequest.get('keywords') as FormArray;
  }
}
