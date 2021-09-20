import { DateDialogComponent } from './components/user-profile/Dialog/dialog';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuard } from './core/auth.guard';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule} from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ChartsModule, ThemeService } from 'ng2-charts';
import { NgCircleProgressModule } from 'ng-circle-progress';

import { AngularFireModule } from '@angular/fire';
import { AngularFirestore } from '@angular/fire/firestore';

import { CoreModule } from './core/core.module';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { from } from 'rxjs';
import { FlowersListComponent } from './components/flowers-list/flowers-list.component';
import { MatOptionModule, MatSelect, MatSelectModule } from '@angular/material';
import { CreateDialogComponent } from './components/flowers-list/Dialog/dialog';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DatePipe } from '@angular/common';

const firebaseConfig = {
  apiKey: 'AIzaSyBi8e76c1Pl2V-A4KwOjQI8vRWJgfaY0W0',
  authDomain: 'flowerly-1cc1b.firebaseapp.com',
  databaseURL: 'https://flowerly-1cc1b.firebaseio.com',
  projectId: 'flowerly-1cc1b',
  storageBucket: 'flowerly-1cc1b.appspot.com',
  messagingSenderId: '810081916188',
  appId: '1:810081916188:web:c9ff4a962715a5af61d6d9'
};
// Initialize Firebas


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserProfileComponent,
    NavbarComponent,
    FlowersListComponent,
    DateDialogComponent,
    CreateDialogComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatSnackBarModule,
    MatSidenavModule,
    MatListModule,
    MatInputModule,
    MatGridListModule,
    MatDatepickerModule,
    FormsModule,
    ReactiveFormsModule,
    AngularFireModule.initializeApp(firebaseConfig),
    CoreModule,
    HttpClientModule,
    MatSelectModule,
    MatOptionModule,
    MatDialogModule,
    ChartsModule,
    NgCircleProgressModule.forRoot({
      radius: 100,
      outerStrokeWidth: 16,
      innerStrokeWidth: 8,
      outerStrokeColor: "#78C000",
      innerStrokeColor: "#C7E596",
      animationDuration: 300
    })
  ],
  providers: [AngularFirestore, ThemeService, DatePipe],
  bootstrap: [AppComponent],
  entryComponents: [DateDialogComponent, CreateDialogComponent]
})
export class AppModule { }
