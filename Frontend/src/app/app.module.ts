import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { ProfileFormComponent } from './profile-form/profile-form.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AppComponent } from './app.component';
import { GettingstartedComponent } from './gettingstarted/gettingstarted.component';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';
import { CoursesComponent } from './courses/courses.component';
import { ProfileComponent } from './profile/profile.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { OppurtunitiesComponent } from './oppurtunities/oppurtunities.component';
import { SigninComponent } from './signin/signin.component';
import { RegisterComponent } from './register/register.component';
import { routes } from './app.routes';

@NgModule({
  declarations: [
    // Removed all standalone components from here
  ],
  imports: [
    BrowserModule,
    CommonModule,
    HttpClientModule,
    RouterModule.forRoot(routes, { 
      useHash: false,
      enableTracing: false // For debugging
    }),
    // Add all standalone components here
    AppComponent,
    ProfileFormComponent,
    NavbarComponent,
    GettingstartedComponent,
    SigninComponent,
    RegisterComponent,
    DashboardComponent,
    LeaderboardComponent,
    ProfileComponent,
    CoursesComponent,
    OppurtunitiesComponent
  ],
  providers: [
    provideHttpClient(withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { } 