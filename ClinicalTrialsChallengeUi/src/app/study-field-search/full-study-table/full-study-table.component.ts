import { AfterViewInit, Component, ViewChild, Input, OnChanges, SimpleChanges, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { FullStudyClient, FullStudyViewDto, LocationDto, PaginatedFullStudies, Pagination, EmailClient, NotificationRequest, RecipientRequest, NotificationType } from 'src/app/client-lib/client';
import { Observable, of, timer, Scheduler, interval } from 'rxjs';
import { startWith, switchMap, map, takeUntil, finalize } from 'rxjs/operators';
import { SearchRequest } from '../search-request';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { EmailDialogComponent, EmailDialogInput, EmailDialogResult } from '../email-dialog/email-dialog.component';

@Component({
  selector: 'ct-full-study-table',
  templateUrl: './full-study-table.component.html',
  styleUrls: ['./full-study-table.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class FullStudyTableComponent implements AfterViewInit {
  @Input() searchRequest: SearchRequest | null = null;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  resultsLength = 0;
  isLoadingResults = false;
  isGovernmentServiceDown = false;
  retrySecondsLeft = 0;

  filteredAndPagedStudies!: Observable<FullStudyViewDto[]>;
  displayedColumns = ['title', 'organizationName', 'status', 'detail'];

  constructor(private fullStudyClient: FullStudyClient, private notificationClient: EmailClient, public dialog: MatDialog) { }

  ngAfterViewInit(): void {
    setTimeout(() => this.refresh());
  }

  refresh(): void {
    this.filteredAndPagedStudies = this.paginator?.page
      .pipe(
        startWith({}),
        switchMap(() => {
          setTimeout(() => this.isLoadingResults = true);
          if (!this.searchRequestIsValid())
            return of(new PaginatedFullStudies({
              fullStudies: [],
              pagination: new Pagination({
                skip: 0,
                take: 0,
                totalItems: 0
              })
            }));
          return this.fullStudyClient.search(
            (this.paginator.pageIndex * this.paginator.pageSize),
            (this.paginator.pageSize),
            this.searchRequest?.keywords,
            this.searchRequest?.location,
            this.searchRequest?.statuses,
            this.searchRequest?.gender,
            this.searchRequest?.centralContactRequired
          );
        }),
        map((data: PaginatedFullStudies) => {
          setTimeout(() => this.isLoadingResults = false);

          if (data.pagination?.skip == 0 && data.pagination.take == 0 && data.pagination.totalItems == 0 && this.searchRequestIsValid()) {
            this.isGovernmentServiceDown = true;
            this.retry();
            return [];
          }

          this.isGovernmentServiceDown = false;
          this.resultsLength = data.pagination?.totalItems ?? 0;

          return data.fullStudies ?? [];
        })
      );
  }

  private searchRequestIsValid() {
    return this.searchRequest && this.searchRequest.keywords.some(k => k.length > 0);
  }

  showContactModal(study: FullStudyViewDto) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = "30%";
    dialogConfig.minWidth = '400px';
    dialogConfig.data = <EmailDialogInput>{
      title: 'Contact Request',
      prompt: 'Provide your email address to receive the contact information of a person to whom questions concerning enrollment at any location of the study can be addressed.'
    }
    const dialogRef = this.dialog.open(EmailDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((result: EmailDialogResult) => {
      if (!result)
        return;
      this.sendNotification(result, study.nctId!, NotificationType.ContactRequestEmail);
    })
  }

  showDownloadModal(study: FullStudyViewDto) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = "30%";
    dialogConfig.minWidth = '400px';
    dialogConfig.data = <EmailDialogInput>{
      title: 'Study Request',
      prompt: 'Provide your email address to receive the full study data export.'
    }
    const dialogRef = this.dialog.open(EmailDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((result: EmailDialogResult) => {
      if (!result)
        return;
      this.sendNotification(result, study.nctId!, NotificationType.StudyRequestEmail)
    })
  }

  private sendNotification(result: EmailDialogResult, nctId: string, notificationType: NotificationType){
    this.notificationClient.send(new NotificationRequest({
      nctId: nctId, recipientRequest: new RecipientRequest({
        recipientAddress: result.email,
        recipientName: result.name
      }), notificationType: notificationType
    })).subscribe();
  }

  retry() {
    const retryInterval = interval(1000);
    const retryTimer = timer(5000);

    this.retrySecondsLeft = 5;

    retryInterval.pipe(
      takeUntil(retryTimer),
      finalize(() => this.refresh())
    ).subscribe(val => {
      this.retrySecondsLeft--;
    });

  }

  formatLocation(location: LocationDto): string {
    if (!location)
      return '';
    return `${location.locationCity}${location.locationState ? `, ${location.locationState}` : ''}, ${location.locationCountry}`;
  }

  hasContact(study: FullStudyViewDto): boolean {
    return (study.centralContacts !== undefined && study.centralContacts !== null && study.centralContacts.length > 0)
  }
}
