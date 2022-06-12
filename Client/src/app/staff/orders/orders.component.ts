import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { IOrderData, IOrderItem, OrderEnum, ResOrderApiService } from 'src/app/api/res-order-api.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  formArray: FormArray;

  orders$ = this.ordersApi.getByStatus(OrderEnum.Pending);
  completedOrders$ = this.ordersApi.getByStatus(OrderEnum.Completed);
  constructor(private formBuilder: FormBuilder, private ordersApi: ResOrderApiService) { }
  readonly OrderEnum = OrderEnum;
  ngOnInit() {

  }

  onComplete(id: number, action: OrderEnum) {
    this.ordersApi.updateStatus(id, action)
      .subscribe(() => {
        this.orders$ = this.ordersApi.getByStatus(OrderEnum.Pending);
        this.completedOrders$ = this.ordersApi.getByStatus(OrderEnum.Completed);
      });
  }
}
