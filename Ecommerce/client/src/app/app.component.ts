import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { IProduct } from './shared/models/product';
import { IPagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'eShopping';
  products: IProduct[] = [];

  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.http
      .get<IPagination<IProduct>>('http://localhost:8010/Catalog/GetAllProducts')
      .subscribe({
        next: response => {
          this.products = response.data;
          console.log(response);
        },
        error: (error) => console.error(error),
        complete: () => {
          console.log('Request completed');
        },
      });
  }
}
