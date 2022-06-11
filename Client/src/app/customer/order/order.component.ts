import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  public orderId: string;
  constructor(private activateRoute: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.activateRoute.queryParams.subscribe((x: any) => this.orderId = x.orderId)

  }

}
