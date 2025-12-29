import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'eShopping';

  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.http.get('http://localhost:8000/api/v1/Catalog/GetProductByName/Adidas%20Terrex%20Swift').subscribe({
       next: response => console.log(response),
       error: error => console.error(error),
       complete: () => {
        console.log('Request completed')
       }
    });
  }
}
