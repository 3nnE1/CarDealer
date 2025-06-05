import { Component } from '@angular/core';

interface Car {
  id: number;
  model: string;
  price: number;
}

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent {
  cars: Car[] = [
    { id: 1, model: 'Model S', price: 79999 },
    { id: 2, model: 'Model X', price: 89999 },
    { id: 3, model: 'Model 3', price: 49999 }
  ];
}
