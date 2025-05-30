import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { Router, RouterOutlet, NavigationEnd } from '@angular/router';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { AuthServiceService } from './_services/auth-service.service';
import { NavbarComponent } from './navbar/navbar.component';
import { ProfileFormComponent } from './profile-form/profile-form.component';
import { filter } from 'rxjs/operators';
import { ProfileService } from './_services/profile.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [RouterOutlet, CommonModule, NavbarComponent, ProfileFormComponent]
})
export class AppComponent implements OnInit {
  title = 'CodeTrace';
  currentRoute: string = '';
  showProfileForm: boolean = false;

  constructor(
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object,
    private authService: AuthServiceService,
    private profileService: ProfileService
  ) {
    // Subscribe to router events to track current route
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      this.currentRoute = event.url;
    });
  }

  get isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  get shouldShowNavbar(): boolean {
    // Hide navbar on getting started, login, and register pages
    // Show navbar only when user is logged in
    const publicRoutes = ['/', '/login', '/register'];
    return this.isLoggedIn && !publicRoutes.includes(this.currentRoute);
  }

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      const urlParams = new URLSearchParams(window.location.search);
      const token = urlParams.get('token');
      if (token) {
        localStorage.setItem('jwt', token);
        try {
          // Decode JWT to extract email and role
          const payload = JSON.parse(atob(token.split('.')[1]));
          if (payload && payload.email) {
            localStorage.setItem('userEmail', payload.email);
          }
          // Store role if present
          if (payload && payload.role) {
            localStorage.setItem('userRole', payload.role);
          } else {
            // Default role is 'user' if not specified
            localStorage.setItem('userRole', 'user');
          }
          
          // Initialize user in the auth service
          this.authService.loadUserFromToken();
          
          // Fetch and store profile data after successful login
          console.log('Fetching and storing profile data after login...');
          this.fetchAndStoreProfileData();
          
          // Check profile completion status
          this.checkProfileCompletion();
          
          this.router.navigate(['/dashboard']);
        } catch (error) {
          console.error('Error parsing token:', error);
        }
      } else if (this.isLoggedIn) {
        // If already logged in, check profile completion and fetch profile data
        console.log('User already logged in. Fetching and storing profile data...');
        this.checkProfileCompletion();
        this.fetchAndStoreProfileData();
      }
    }
  }

  checkProfileCompletion() {
    this.authService.checkProfileCompletion()?.subscribe({
      next: (isComplete) => {
        this.showProfileForm = !isComplete;
      },
      error: (error) => {
        console.error('Error checking profile completion:', error);
        // Optionally, hide the profile form on error
        this.showProfileForm = false;
      }
    });
  }

  onProfileCompleted() {
    this.showProfileForm = false;
    // Optionally refresh user data or redirect after profile completion
  }

  // New method to fetch and store profile data
  fetchAndStoreProfileData() {
    console.log('Executing fetchAndStoreProfileData');
    this.profileService.getProfile().subscribe({
      next: (response: any) => {
        console.log('Profile data fetched successfully:', response);
        if (response?.Profile) {
          const profile = response.Profile;
          console.log('Profile object:', profile);
          // Store platform usernames in local storage
          localStorage.setItem('leetcodeUsername', profile.leetcodeUsername || '');
          localStorage.setItem('githubUsername', profile.githubUsername || '');
          localStorage.setItem('codechefUsername', profile.codechefUsername || '');
          localStorage.setItem('hackerrankUsername', profile.hackerrankUsername || '');
          localStorage.setItem('codeforcesUsername', profile.codeforcesUsername || '');
          localStorage.setItem('gfgUsername', profile.gfgUsername || '');
          console.log('Platform usernames stored in local storage.');
          console.log('LeetCode Username in Local Storage:', localStorage.getItem('leetcodeUsername'));
          console.log('GitHub Username in Local Storage:', localStorage.getItem('githubUsername'));
          // Add logs for other platforms as needed
        } else {
          console.log('Profile data not found in response or response is null.');
        }
      },
      error: (error) => {
        console.error('Error fetching profile data:', error);
      }
    });
  }
}
