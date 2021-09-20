import { UserDto } from './../../core/api.service';
import { Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from './../../core/auth.service';
import { async } from 'rxjs/internal/scheduler/async';
import { ApiClient } from 'src/app/core/api.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public auth: AuthService, private router: Router, private api: ApiClient) {}

  ngOnInit() {
  }

  onClickLogin() {
    this.auth.googleSignin().then(x => {this.auth.user$.subscribe((data) => {
      this.api.user_UpsertUser({displayName: data.displayName, email: data.email, uid: data.uid}).subscribe( x => console.log(x));
      });

      this.router.navigateByUrl('/profile');
    });

  }
}
