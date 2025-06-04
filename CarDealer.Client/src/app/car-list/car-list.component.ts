import { Component, OnInit } from '@angular/core';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent implements OnInit {
  engineSearch = '';
  cars: any[] = [];
  loading = false;
  error = '';

  constructor(private carService: CarService) {}

  ngOnInit(): void {
    this.search();
  }

  search(): void {
    this.loading = true;
    this.error = '';
    this.carService.getCarsByEngine(this.engineSearch).subscribe({
      next: data => {
        this.cars = data;
        this.loading = false;
      },
      error: err => {
        this.error = 'Error fetching cars';
        this.loading = false;
      }
    });
  }
}
