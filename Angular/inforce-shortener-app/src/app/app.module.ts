import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { JwtModule } from '@auth0/angular-jwt';
import { TOKEN_KEY } from './shared/services/auth.service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon'
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthorizationComponent } from './authorization/authorization.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AboutComponent } from './about/about.component';
import { HomeComponent } from './home/home.component';
import { AuthorizationInterceptor } from './shared/Interceptors/authorization.interceptor';

export function tokenGetter() {
  return localStorage.getItem(TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    AuthorizationComponent,
    AboutComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatFormFieldModule,
    MatIconModule,
    MatProgressSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost']
      }
    }),
    BrowserAnimationsModule
  ],
  providers: [
    {
     provide: HTTP_INTERCEPTORS,
     useClass: AuthorizationInterceptor,
     multi: true
    }
   ],
  bootstrap: [AppComponent]
})
export class AppModule { }
