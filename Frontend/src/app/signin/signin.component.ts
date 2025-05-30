import { Component } from '@angular/core';
import { AuthServiceService } from '../_services/auth-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent {
  constructor(
    private authService: AuthServiceService,
    private router: Router
  ) { }
  
  signInWithGoogle() {
    this.authService.signInWithGoogle();
  }

  goBack() {
    this.router.navigate(['/']);
  }
}