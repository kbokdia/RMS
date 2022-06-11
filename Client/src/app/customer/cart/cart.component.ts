import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  panelOpenState = true;
  quantity:number = 1;
  constructor() { 
  }
  ngOnInit(): void {
   
  }

  addQuantity(){
    this.quantity++;
    console.log(this.quantity);
  }

  removeQuantity(){
    if(this.quantity > 0){
      this.quantity--;
      console.log(this.quantity);
    }
  }
}
