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
    ShowSingleComponent
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
    
  ],
  providers: [TourService,OrderService,TourMasterDataService,
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
    
  }
 }
