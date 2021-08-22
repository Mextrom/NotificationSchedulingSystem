import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'notification-scheduling',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent {
  public schedule: Schedule;
  private http: HttpClient;
  private baseUrl: string;

  /**
   * Creates a component
   * @param http an object whihc handles http requests
   * @param baseUrl application/API base URL
   */
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    if (this.schedule == null) {
      // since it is not specified how exactly we should create a company
      // we call 'create-test-company to create one company with the data specified in the requirements
      // and after the company (and its schdedue) is created we get company's schedule from the api with a separate call
      http.get<CompanyId>(baseUrl + 'notification-scheduling/create-test-company/').subscribe(result => {
        this.loadSchedule(result.companyId);
      }, error => console.error(error));
    }
  }

  /**
   * Loads a schedule for the company with the spcified identifier from the service
   * @param companyId company identifeir
   */
  loadSchedule(companyId: string) {
    this.http.get<Schedule>(this.baseUrl + 'notification-scheduling/schedule/' + companyId).subscribe(result => {
      this.schedule = result;
    }, error => console.error(error));
  }

  /**
   * Calculates a difference between tow dates in days
   * @param next the next date
   * @param prev previous date
   */
  calculateDateDiff(next: Date, prev: Date) : string {
    let currentDate = new Date(next);
    let dateSent = new Date(prev);

    let dateDiff = Math.floor(
      (Date.UTC(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate()) -
        Date.UTC(dateSent.getFullYear(), dateSent.getMonth(), dateSent.getDate())) /
      (1000 * 60 * 60 * 24));;

    return dateDiff + ' d.';
  }
}

/**
 * A schedule object
 **/
interface Schedule {
  companyId: string,
  notifications: Date[];
}

/**
 * A simple wrapper for company id
 **/
interface CompanyId {
  companyId: string
}
