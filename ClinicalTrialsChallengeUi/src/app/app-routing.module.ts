import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudyFieldSearchComponent } from './study-field-search/study-field-search.component';

const routes: Routes = [
  { path: '', component: StudyFieldSearchComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
