import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-gettingstarted',
  templateUrl: './gettingstarted.component.html',
  styleUrls: ['./gettingstarted.component.css']
})
export class GettingstartedComponent {
  constructor(private router: Router) {}

  navigateTo(route: string) {
    this.router.navigate([route]);
  }
}
