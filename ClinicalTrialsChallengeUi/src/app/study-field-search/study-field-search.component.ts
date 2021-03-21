import { Component, OnInit, ViewChild } from '@angular/core';
import { FullStudyClient, Status, StatusClient } from '../client-lib/client';
import { of, Observable } from 'rxjs';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { SearchRequest } from './search-request';
import { FullStudyTableComponent } from './full-study-table/full-study-table.component';

@Component({
  selector: 'ct-study-field-search',
  templateUrl: './study-field-search.component.html',
  styleUrls: ['./study-field-search.component.scss']
})
export class StudyFieldSearchComponent implements OnInit {
  
  @ViewChild(FullStudyTableComponent) table !: FullStudyTableComponent; 
  searchRequestForm: FormGroup = this.fb.group({
    keywords: this.fb.array([
      this.fb.control('', Validators.required)
    ]),
    location: [''],
    statuses: [null],
    gender: ['Any']
  });

  studyStatuses!: Observable<Status[]>;  
  constructor(private client: FullStudyClient, private statusClient: StatusClient, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.studyStatuses = this.statusClient.getStatuses();
  }

  addKeyword() {
    if (this.keywords.invalid) {
      this.keywords.markAllAsTouched();
      return;
    }
    this.keywords.push(this.fb.control('', Validators.required));
  }

  removeKeyword(index: number){
    if (this.keywords.length == 1) {
      this.keywords.markAllAsTouched();
      return;
    }
    this.keywords.removeAt(index);
  }

  refreshTable(){
    if (!this.searchRequestForm.valid){
      this.searchRequestForm.markAllAsTouched();
      return;
    }
    this.table.refresh();
  }

  clearForm() {
    this.searchRequestForm = this.fb.group({
      keywords: this.fb.array([
        this.fb.control('', Validators.required)
      ]),
      location: [''],
      statuses: [null],
      gender: ['Any']
    });
  }

  get keywords() {
    return this.searchRequestForm.get('keywords') as FormArray;
  }

  get searchRequest(): SearchRequest {
    let allSearchTerms : string[] = [];

    if (this.searchRequestForm.get('location')?.value.length > 0)
      allSearchTerms.push(this.searchRequestForm.get('location')?.value);

    return <SearchRequest>{
      keywords: [...this.keywords.value, ...allSearchTerms],
      location: this.searchRequestForm.get('location')?.value,
      statuses: this.searchRequestForm.get('statuses')?.value,
      gender: this.searchRequestForm.get('gender')?.value ?? 'All'
    }
  }
}
