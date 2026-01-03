import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'eShopping';
  products: any[] = [];

  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.http
      .get('http://localhost:8010/Catalog/GetProductsByBrandName/Nike')
      .subscribe({
        next: (response: any) => {
          this.products = response;
          console.log(response);
        },
        error: (error) => console.error(error),
        complete: () => {
          console.log('Request completed');
        },
      });
  }
}
