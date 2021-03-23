import { AfterViewInit, Component, ViewChild, Input } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { EmailClient, PaginatedEmails, IEmail, EmailDto, NotificationType } from 'src/app/client-lib/client';
import { Observable } from 'rxjs';
import { startWith, switchMap, map } from 'rxjs/operators';

@Component({
  selector: 'ct-email-search-table',
  templateUrl: './email-search-table.component.html',
  styleUrls: ['./email-search-table.component.scss']
})
export class EmailSearchTableComponent implements AfterViewInit {  
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  resultsLength = 0;
  isLoadingResults = false;

  pagedEmails!: Observable<EmailDto[]>;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['sent', 'subject', 'notificationType', 'attachmentCount', 'address'];

  constructor(private notificationClient: EmailClient) { }

  ngAfterViewInit(): void {
    setTimeout(() => this.refresh());
  }

  refresh(): void {
    this.pagedEmails = this.paginator?.page
      .pipe(
        startWith({}),
        switchMap(() => {
          setTimeout(() => this.isLoadingResults = true);
          return this.notificationClient.search((this.paginator.pageIndex * this.paginator.pageSize),
          (this.paginator.pageSize));
        }),
        map((data: PaginatedEmails) => {
          setTimeout(() => this.isLoadingResults = false);

          this.resultsLength = data.pagination?.totalItems ?? 0;

          return data.emails ?? [];
        })
      )
  }

  getNotificationTypeName(email: EmailDto): string{
    return NotificationType[email.notificationType].replace(/([A-Z])/g, ' $1');
  }
}
