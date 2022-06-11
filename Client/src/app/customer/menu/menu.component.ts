import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { debounce, debounceTime, flatMap, lastValueFrom, mergeMap, map, distinctUntilChanged } from 'rxjs';
import { ICategory, IMenuItem, MenuItemStatusEnum, ResMenuApiService } from 'src/app/api/res-menu-api-service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  panelOpenState = false;
  quantity: number = 1;
  @Input() alwaysExpanded: boolean = false;
  @Input() title: string = 'MENU';

  public selectedItems: number = 0;
  public selectedItemTotalCost: number = 0;
  public formArray: FormArray<FormGroup<CategoryForms>>;
  public showPriceSnackBar: boolean = true;

  constructor(private menuSvc: ResMenuApiService, private fb: FormBuilder) {
  }

  async ngOnInit() {
    const apiResponse = await lastValueFrom(this.menuSvc.getAllByCategories());
    this.formArray = this.fb.array(apiResponse?.data?.map(x => MenuComponent.CreateFormForCategories(this.fb, x)));
    this.formArray.valueChanges
      .pipe(
        debounceTime(500),
        map(x => x.flatMap(y => y.items)),
        distinctUntilChanged(),
      )
      .subscribe(x => {
        const selectedItems = x.filter(x => (x?.quantity ?? 0) > 0);
        console.log(x)
        this.selectedItems = selectedItems.length;
        this.selectedItemTotalCost = selectedItems.reduce((p, c) => p + ((c?.price ?? 0) * (c?.quantity ?? 0)), 0);
      });
  }

  getItemGroup(fg: FormGroup) {
    console.log(fg.value)
    return fg.get('items');
  }
  patchQuantity(fg: FormGroup<MenuForms>, count: number) {
    const currentQty = fg.value.quantity ?? 0;
    const qty = Math.max(0, currentQty + count);
    fg.patchValue({ quantity: qty })
  }

  static CreateFormForCategories(fb: FormBuilder, categoryData: ICategory) {
    const formArray: FormArray<FormGroup<MenuForms>> = fb.nonNullable.array(categoryData?.items?.map(x => MenuComponent.CreateFormForMenuItem(fb, x)));
    console.log("array", formArray)
    const formGroup: FormGroup<CategoryForms> = fb.nonNullable.group({
      category: categoryData.category,
      items: formArray,
    });
    return formGroup;
  }
  static CreateFormForMenuItem(fb: FormBuilder, menuItemData: IMenuItem): FormGroup<MenuForms> {
    const formGroup: FormGroup<MenuForms> = fb.nonNullable.group({
      id: menuItemData.id,
      name: menuItemData.name,
      categoryType: menuItemData.categoryType,
      price: menuItemData.price,
      description: menuItemData.description,
      imageUrl: menuItemData.imageUrl,
      isVeg: menuItemData.isVeg,
      status: menuItemData.status,
      quantity: 0,
      // tags: menuItemData.tags,
    })
    return formGroup;
  }

}


export interface CategoryForms {
  category: FormControl<string>;
  items: FormArray<FormGroup<MenuForms>>;
}
export interface MenuForms {
  id: FormControl<number>;
  name: FormControl<string>;
  categoryType: FormControl<string>;
  price: FormControl<number>;
  description: FormControl<string>;
  imageUrl: FormControl<string>;
  isVeg: FormControl<boolean>;
  status: FormControl<MenuItemStatusEnum>;
  quantity: FormControl<number>;
  // tags: FormControl<string[]>;
}