import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudyFieldSearchComponent } from './study-field-search/study-field-search.component';
import { EmailSearchComponent } from './email-search/email-search.component';

const routes: Routes = [
  { path: '', component: StudyFieldSearchComponent },
  { path: 'admin', component: EmailSearchComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
