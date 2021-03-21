import { AfterViewInit, Component, ViewChild, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { FullStudyClient, FullStudyViewDto, LocationDto, PaginatedFullStudies, Pagination, NotificationClient } from 'src/app/client-lib/client';
import { Observable, of, timer, Scheduler, interval } from 'rxjs';
import { startWith, switchMap, map, takeUntil, finalize } from 'rxjs/operators';
import { SearchRequest } from '../search-request';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ContactRequestComponent } from '../contact-request/contact-request.component';

@Component({
  selector: 'ct-full-study-table',
  templateUrl: './full-study-table.component.html',
  styleUrls: ['./full-study-table.component.scss']
})
export class FullStudyTableComponent implements AfterViewInit {
  @Input() searchRequest: SearchRequest | null = null;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  resultsLength = 0;
  isLoadingResults = false;
  isGovernmentServiceDown = false;
  retrySecondsLeft = 0;

  filteredAndPagedStudies!: Observable<FullStudyViewDto[]>;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['title', 'organizationName', 'status', 'detail'];

  constructor(private fullStudyClient: FullStudyClient, private notificationClient: NotificationClient, public dialog: MatDialog) { }

  ngAfterViewInit(): void {
    setTimeout(() => this.refresh());
  }

  refresh() {
    console.log(this.searchRequest);
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
            this.searchRequest?.gender
          );
        }),
        map((data: PaginatedFullStudies) => {
          setTimeout(() => this.isLoadingResults = false);

          if(data.pagination?.skip == 0 && data.pagination.take == 0 && data.pagination.totalItems == 0 && this.searchRequestIsValid()){
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

  private searchRequestIsValid(){
    return this.searchRequest && this.searchRequest.keywords.some(k => k.length > 0);
  }

  showContactModal(study: FullStudyViewDto){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = "30%";
    dialogConfig.minWidth = '400px'; 
    const dialogRef = this.dialog.open(ContactRequestComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.notificationClient.notify();
      console.log(`Dialog result: ${result}`);
    })
  }

  retry(){
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
}
