import { Component } from '@angular/core';
import { AuthServiceService } from '../_services/auth-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
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
