import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TourComponent } from './tours/shared/tour/tour.component';
import { TourService } from './tours/shared/tour.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TabMenuModule } from 'primeng/tabmenu';
import { MenuModule } from 'primeng/menu';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import {DialogModule} from 'primeng/dialog';
import {ButtonModule} from 'primeng/button';
import { TourDetailComponent } from './tours/shared/tour-detail/tour-detail.component';
import { CardModule } from 'primeng/card';
import { CommonModule } from '@angular/common';
import { ShowsComponent } from './tours/shared/shows/shows.component';
import { OrderComponent } from './orders/shared/order/order.component';
import { OrderService } from './orders/shared/order.service';
import { OrderDetailComponent } from './orders/shared/order-detail/order-detail.component';
import { ItemsComponent } from './orders/shared/items/items.component';
import { TourAddComponent } from './tours/shared/tour-add/tour-add.component';
import { TourMasterDataService } from './shared/tour-master-data.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {InputTextModule} from 'primeng/inputtext';
import {DropdownModule} from 'primeng/dropdown';
import {InputTextareaModule} from 'primeng/inputtextarea';
import {CalendarModule} from 'primeng/calendar';
import { openStdin } from 'process';
import 'automapper-ts';
import { EnsureAcceptHeaderInterceptor } from './shared/ensure-accept-header-interceptor';
import { ShowSingleComponent } from './tours/shared/shows/show-single/show-single.component';
import { ShowAddComponent } from './tours/shared/shows/show-add/show-add.component';
import { OrderAddComponent } from './orders/shared/order/order-add/order-add.component';
import { MenuItemsComponent } from './orders/shared/menu-items/menu-items.component';
import { ItemAddComponent } from './orders/shared/item-add/item-add.component';
import { ItemSingleComponent } from './orders/shared/item-single/item-single.component';
import { ItemService } from './items/shared/item.service';
import { IngredientsComponent } from './orders/shared/ingredients/ingredients.component';
import { IngredientComponent } from './ingredient/ingredient.component';
import { IngredientCategoryComponent } from './ingredient-category/ingredient-category.component';
import { IngredientService } from './ingredient/ingredient.service';
import {MultiSelectModule} from 'primeng/multiselect';
import { PaymentDetailsComponent } from './payment-details/payment-details.component';
import { PaymentDetailComponent } from './payment-details/payment-detail/payment-detail.component';
import { PaymentDetailListComponent } from './payment-details/payment-detail-list/payment-detail-list.component';
import { PaymentDetailService } from './payment-details/shared/payment-detail.service';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  declarations: [
    AppComponent,
    TourComponent,
    TourDetailComponent,
    ShowsComponent,
    OrderComponent,
    OrderDetailComponent,
    ItemsComponent,
    TourAddComponent,
    ShowSingleComponent,
    ShowAddComponent,
    OrderAddComponent,
    MenuItemsComponent,
    ItemAddComponent,
    ItemSingleComponent,
    IngredientsComponent,
    IngredientComponent,
    IngredientCategoryComponent,
    PaymentDetailsComponent,
    PaymentDetailComponent,
    PaymentDetailListComponent,
    
  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    ReactiveFormsModule,//Add if needed 
    FormsModule,     //Add if needed
    HttpClientModule,
    BrowserAnimationsModule,
    TabMenuModule,
    MenuModule,
    PanelModule,
    TableModule,
    DialogModule,
    ButtonModule,
    CardModule,
    InputTextModule,
    DropdownModule,
    InputTextareaModule,
    CalendarModule,
    InputTextModule,
    MultiSelectModule,
    ToastrModule.forRoot()
    
  ],
  providers: [TourService,OrderService,TourMasterDataService, ItemService,IngredientService,PaymentDetailService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: EnsureAcceptHeaderInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  /**
   *
   */

  constructor() {
    automapper.createMap('TourFormModel', 'TourForCreation')
      .forSourceMember('band', (opts: AutoMapperJs.ISourceMemberConfigurationOptions) =>
      {opts.ignore();})//because I don't need these two to create tour
      .forSourceMember('manager', (opts: AutoMapperJs.ISourceMemberConfigurationOptions) =>
      {opts.ignore();})
      .forMember('bandid', function (opts) { opts.mapFrom('band'); });

      automapper.createMap('TourFormModel', 'TourWithShowsForCreation')
      .forSourceMember('band', (opts: AutoMapperJs.ISourceMemberConfigurationOptions) =>
      {opts.ignore();})//because I don't need these two to create tour
      .forSourceMember('manager', (opts: AutoMapperJs.ISourceMemberConfigurationOptions) =>
      {opts.ignore();})
      .forMember('bandid', function (opts) { opts.mapFrom('band'); });


      // 'application/vnd.jose.tourwithmanagerandshowsforcreation+json'
      automapper.createMap('TourFormModel', 'TourWithManagerForCreation')
      .forSourceMember('band', (opts: AutoMapperJs.ISourceMemberConfigurationOptions) => { opts.ignore(); })
      .forSourceMember('manager', (opts: AutoMapperJs.ISourceMemberConfigurationOptions) => { opts.ignore(); })
      .forMember('bandid', function (opts) { opts.mapFrom('band'); })
      .forMember('managerid', function (opts) { opts.mapFrom('manager'); })

      automapper.createMap('TourFormModel', 'TourWithManagerAndShowsForCreation')
      .forSourceMember('band', (opts: AutoMapperJs.ISourceMemberConfigurationOptions) => { opts.ignore(); })
      .forSourceMember('manager', (opts: AutoMapperJs.ISourceMemberConfigurationOptions) => { opts.ignore(); })
      .forMember('bandid', function (opts) { opts.mapFrom('band'); })
      .forMember('managerid', function (opts) { opts.mapFrom('manager'); })

      automapper.createMap('ShowCollectionFormModelShowsArray',
        'ShowCollectionForCreation');

      automapper.createMap('ItemCollectionFormModelItemsArray',
        'ItemCollectionForCreation');
    
  }
 }
