import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { AuthServiceService } from '../_services/auth-service.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class NavbarComponent implements OnInit {
  userEmail: string | null = null;
  userRole: string | null = null;
  dropdownOpen = false;

  constructor(
    public router: Router, 
    private http: HttpClient,
    private authService: AuthServiceService
  ) {}

  ngOnInit() {
    // Get the user email from localStorage
    this.userEmail = localStorage.getItem('userEmail');
    this.userRole = localStorage.getItem('userRole') || 'user';
  }

  toggleDropdown() {
    this.dropdownOpen = !this.dropdownOpen;
  }

  closeDropdown() {
    setTimeout(() => this.dropdownOpen = false, 150);
  }

  goToProfile() {
    this.router.navigate(['/profile']);
    this.dropdownOpen = false;
  }

  logout() {
    this.authService.logout();
    this.dropdownOpen = false;
  }

  navigate(route: string) {
    this.router.navigate([route]);
    if (this.dropdownOpen) {
      this.dropdownOpen = false;
    }
  }

  checkProfileCompletion() {
    this.authService.checkProfileCompletion()?.subscribe(
      response => {
        console.log('Profile completion check:', response);
      },
      error => {
        console.error('Error checking profile completion:', error);
      }
    );
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }
}
