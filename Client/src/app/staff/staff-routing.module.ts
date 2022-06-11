import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { OrdersComponent } from './orders/orders.component';
import { TablesComponent } from './tables/tables.component';


const routes: Routes = [
  { path: 'tables', component: TablesComponent },
  { path: 'orders', component: OrdersComponent },
  { path: 'menu', component: MenuComponent },
  { path: 'home', component:HomeComponent }
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StaffRoutingModule { }
