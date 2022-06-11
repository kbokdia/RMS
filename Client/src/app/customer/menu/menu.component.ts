import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  panelOpenState = false;
  quantity:number = 1;
  @Input() alwaysExpanded:boolean = false;
  @Input() title:string = 'MENU';
  constructor() { 
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


  ngOnInit(): void {
  }

}
