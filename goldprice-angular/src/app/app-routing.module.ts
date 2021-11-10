import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GoldpriceComponent } from './goldprice/goldprice.component';


const routes: Routes = [
  { path: 'goldprice', component: GoldpriceComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
