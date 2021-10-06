import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DeviceAddComponent } from './devices/device-add/device-add.component';
import { DeviceDetailComponent } from './devices/device-detail/device-detail.component';
import { DeviceEditComponent } from './devices/device-edit/device-edit.component';
import { DeviceListComponent } from './devices/device-list/device-list.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {  
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'devices', component: DeviceListComponent},
      {path: 'devices/:id', component: DeviceDetailComponent},
      {path: 'device/edit/:id', component: DeviceEditComponent},
      {path: 'device/add', component: DeviceAddComponent},
      {path: 'lists', component: ListsComponent}
    ]
  },
  {path: 'register', component: RegisterComponent},
  {path: 'login', component: LoginComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: '**', component: HomeComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
