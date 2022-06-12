import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { Router } from '@angular/router';
import { debounce, debounceTime, distinctUntilChanged, lastValueFrom } from 'rxjs';
import { IMenuItem } from 'src/app/api/res-menu-api-service';
import { IMenuItemRef, OrderEnum, IOrder, ResOrderApiService } from 'src/app/api/res-order-api.service';
import { AuthService } from 'src/app/auth/auth.service';
import { LocalStorageService } from 'src/app/tools/local-storage.service';
import { MenuComponent, MenuForms } from '../menu/menu.component';
import { PlaceOrderComponent } from './place-order/place-order.component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  panelOpenState = true;
  quantity: number = 1;
  selectedItems: IMenuItem[];

  @ViewChild("inputTextArea") inputTextArea: ElementRef;
  public totalCost: number = 0;
  public phoneNumber: string = '';

  // public formGroup: FormGroup<IOrder>
  public formArray: FormArray<FormGroup<MenuForms>>
  constructor(
    private fb: FormBuilder,
    private storageSvc: LocalStorageService,
    private orderApiSvc: ResOrderApiService,
    private router: Router,
    private _bottomSheet: MatBottomSheet,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.selectedItems = this.storageSvc.getValue("SelectedItems");
    if (!this.selectedItems) {
      this.router.navigate(['customer/menu']);
      return;
    }
    this.totalCost = this.selectedItems.reduce((p, c) => p + (c?.cost ?? 0), 0)
    this.formArray = this.fb.array(this.selectedItems.map(item => MenuComponent.CreateFormForMenuItem(this.fb, item)));
    this.formArray.valueChanges
      .pipe(
        debounceTime(200),
        distinctUntilChanged()
      )
      .subscribe(x => {
        this.totalCost = x.filter(x => (x.quantity ?? 0) > 0).reduce((p, c) => p + (c?.cost ?? 0), 0)
      });
  }

  patchQuantity(fg: FormGroup<MenuForms>, count: number) {
    const currentQty = fg.value.quantity ?? 0;
    const qty = Math.max(0, currentQty + count);
    const cost = qty * (fg.value?.price ?? 0);
    fg.patchValue({ quantity: qty, cost: cost })
  }
  async placeOrder() {
    const userInput = this.inputTextArea?.nativeElement?.value ?? '';
    const menuItemRefs = this.formArray.value
      .filter(x => (x?.quantity ?? 0) > 0)
      .map(x => ({ menuId: x.id, quantity: x.quantity } as IMenuItemRef))

    const tableId = this.storageSvc.getValue('tableId')
    let order: IOrder = {
      instructions: userInput,
      mobile: this.authService.getUsername(),
      status: OrderEnum.Pending,
      tableId: tableId,
      userId: this.authService.getUserId(),
      items: menuItemRefs,
    };
    if (!order.mobile) {
      const bottomSheet = this._bottomSheet.open(PlaceOrderComponent, { panelClass: 'bg-color' });
      const contactNo = await lastValueFrom(bottomSheet.afterDismissed());
      order.mobile = contactNo;
    }
    try {
      const orderResponse = await lastValueFrom(this.orderApiSvc.save(order));
      const orderId = orderResponse.data?.orderId;
      this.storageSvc.setValue("SelectedItems", '');
      this.router.navigate([`customer/order`], { queryParams: { orderId: orderId } })
    }
    catch (e) {
      console.error(e)
    }
  }

}

export interface OrderForms {
  mobile: FormControl<string>;
  userId: FormControl<number>;
  tableId: FormControl<number>;
  instructions: FormControl<string>;
  status: FormControl<OrderEnum>;
  items: FormArray<FormGroup<MenuRefForms>>;
}
export interface MenuRefForms {
  menuId: FormControl<number>;
  quantity: FormControl<number>;
}