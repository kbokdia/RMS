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

  orders$ = this.ordersApi.getAll();

  constructor(private formBuilder: FormBuilder, private ordersApi: ResOrderApiService) { }

  ngOnInit() {

  }

  onComplete(id: number) {
    this.ordersApi.updateStatus(id, OrderEnum.Completed).subscribe();
  }

}
