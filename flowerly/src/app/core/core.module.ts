import { ApiClient } from './api.service';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AngularFireAuthModule } from '@angular/fire/auth';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AngularFireAuthModule
  ],
  providers: [AuthService, AuthGuard, ApiClient]
})
export class CoreModule { }
