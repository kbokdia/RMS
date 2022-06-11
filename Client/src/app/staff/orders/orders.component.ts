import { Component, OnInit } from '@angular/core';
import { OrdersApiService } from 'src/app/api/orders-api.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orders$ = this.ordersApi.getAll();

  constructor(private ordersApi: OrdersApiService) { }

  ngOnInit(): void {
  }

}
