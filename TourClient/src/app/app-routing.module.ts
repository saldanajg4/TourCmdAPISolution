import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { TourComponent } from './tours/shared/tour/tour.component';


const routes: Routes = [
  // {path: '', component: AppComponent},
  {path: 'tour', component: TourComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
