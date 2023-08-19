import { NgModule } from '@angular/core';
import { ExtraOptions, RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';

const routes: Routes = [
  { path: '', redirectTo: '/room/4', pathMatch: 'full' },
  { path: 'room/:id', component: HomeComponent }
];

const routerOptions: ExtraOptions = {
  onSameUrlNavigation: 'reload' // Isso fará com que o componente seja recarregado ao mesmo URL
};

@NgModule({
  imports: [RouterModule.forRoot(routes, routerOptions)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
