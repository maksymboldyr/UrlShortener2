import { Component } from "@angular/core";
import { UrlService } from "./services/url.service";
import { UrlTableEntry } from "./interfaces/url-table-entry";
import { FormControl } from '@angular/forms';
import { FormGroup } from "@angular/forms";
import { AuthService } from "./services/auth.service";
import { environment } from "../environments/environment.development";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    standalone: false
})
export class AppComponent {
  rootUrl = environment.root;
  title = 'AngularApp';
  displayedColumns: string[] = ['originalUrlString', 'shortUrlString', 'shortUrlInfo'];
  urlTextToDisplay: string = '';
  urlForm: FormGroup = new FormGroup({
    originalUrl: new FormControl(''),
  });

  tableData : UrlTableEntry[] = [];

  constructor(private urlService: UrlService, private authService: AuthService) {
  }

  get isAdmin() {
    return this.authService.hasRole('ADMIN');
  }

  get isAuthorized() {
    return this.authService.hasRole('USER');
  }

  get currentUserId() {
    return this.authService.userId;
  }

  ngOnInit() {
    this.updateTable();
  }

  createRedirectUrl(shortUrl: string) {
    return `${this.rootUrl}/${shortUrl}`;
  }

  redirectToInfo(url: string) {
    const redirectTo = this.rootUrl + `/${url}/Info`;
    
    return redirectTo;
  }

  deleteUrl(id: string) {
    this.urlService.delete(id).subscribe(() => {
      this.updateTable();
    });
  }

  deleteAllUrls() {
    this.urlService.deleteAll().subscribe(() => {
      this.updateTable();
    });
  }

  updateTable() {
    this.urlService.getAll().subscribe((data) => {
      this.tableData = data;
    });
  }
  
  linkSubmit() {

    const url = this.urlForm.get('originalUrl')?.value;

    this.urlTextToDisplay = url;

    console.log(url);
    
    
    this.urlService.create(url).subscribe((data) => {
      this.tableData.push(data);
      this.updateTable();
    });

    this.urlForm.reset();
  }
}
