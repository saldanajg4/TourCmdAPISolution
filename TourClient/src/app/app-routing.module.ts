import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { IngredientComponent } from './ingredient/ingredient.component';
import { ItemAddComponent } from './orders/shared/item-add/item-add.component';
import { MenuItemsComponent } from './orders/shared/menu-items/menu-items.component';
import { OrderDetailComponent } from './orders/shared/order-detail/order-detail.component';
import { OrderAddComponent } from './orders/shared/order/order-add/order-add.component';
import { OrderComponent } from './orders/shared/order/order.component';
import { PaymentDetailsComponent } from './payment-details/payment-details.component';
import { LoginComponent } from './security/login.component';
import { ShowAddComponent } from './tours/shared/shows/show-add/show-add.component';
import { TourAddComponent } from './tours/shared/tour-add/tour-add.component';
import { TourDetailComponent } from './tours/shared/tour-detail/tour-detail.component';
import { TourComponent } from './tours/shared/tour/tour.component';


const routes: Routes = [
  // {path: '', component: AppComponent},
  {path: 'tour', component: TourComponent},
  {path: 'tour/:tourId', component: TourDetailComponent},
  {path: 'tour-add', component: TourAddComponent},
  {path: 'tour/:tourId/show-add', component: ShowAddComponent},
  {path: 'order', component: OrderComponent},
  {path: 'order/:orderId', component: OrderDetailComponent},
  {path: 'order-add', component: OrderAddComponent},
  {path: 'menu-items', component: MenuItemsComponent},
  {path: 'item-add', component: ItemAddComponent},
  {path: 'ingredients', component: IngredientComponent},
  {path: 'pay-details', component: PaymentDetailsComponent},
  {path: 'login', component: LoginComponent},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
