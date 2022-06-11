import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { filter, firstValueFrom, map } from 'rxjs';
import { ICategory, IMenuItem, MenuItemStatusEnum, ResMenuApiService } from 'src/app/api/res-menu-api-service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  formArray: FormArray<FormGroup<CategoryFormGroup>>;

  constructor(private formBuilder: FormBuilder, private menuApi: ResMenuApiService) { }

  async ngOnInit() {
    const menu = await firstValueFrom(this.menuApi.getAllByCategories());
    this.formArray = this.formBuilder.array(menu.data
      .map(category => this.createCategoryFormGroup(category)));

    this.formArray.controls.forEach(category => category.controls
      .items.controls.forEach(item => item.valueChanges
        .pipe(
          filter(value => !!value.id),
          map(value => ({
            id: value.id,
            name: value.name,
            status: value.status ? MenuItemStatusEnum.active : MenuItemStatusEnum.inactive
          } as IMenuItem)))
        .subscribe(value => this.updateMenuItem(value)))
    );
  }

  createCategoryFormGroup(category: ICategory) {
    const formGroup: FormGroup<CategoryFormGroup> = this.formBuilder.nonNullable.group({
      category: category.category,
      items: this.formBuilder.array(category.items
        .map(item => this.createItemFormGroup(item)))
    });
    return formGroup;
  }

  createItemFormGroup(item: IMenuItem) {
    const formGroup: FormGroup<MenuItemFormModel> = this.formBuilder.nonNullable.group({
      id: item.id,
      name: item.name,
      status: item.status === MenuItemStatusEnum.active
    });
    return formGroup;
  }

  private updateMenuItem(value: IMenuItem) {
    this.menuApi.updateMenuItemStatus(value).subscribe();
  }
}

interface CategoryFormGroup {
  category: FormControl<string>;
  items: FormArray<FormGroup<MenuItemFormModel>>;
}

interface MenuItemFormModel {
  id: FormControl<number>;
  name: FormControl<string>;
  status: FormControl<boolean>;
}

