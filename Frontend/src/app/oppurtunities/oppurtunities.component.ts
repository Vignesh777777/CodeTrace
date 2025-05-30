import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthServiceService } from '../_services/auth-service.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { OpportunityService } from '../_services/opportunity.service';
import { Opportunity } from '../_models/Opportunity';
import { Router } from '@angular/router';

@Component({
  selector: 'app-oppurtunities',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './oppurtunities.component.html',
  styleUrl: './oppurtunities.component.css',
  animations: [
    trigger('formAnimation', [
      state('closed', style({
        opacity: 0,
        transform: 'translateY(50px) scale(0.9)'
      })),
      state('open', style({
        opacity: 1,
        transform: 'translateY(0) scale(1)'
      })),
      transition('closed => open', [
        animate('0.4s cubic-bezier(0.3, 1.05, 0.4, 1.05)')
      ]),
      transition('open => closed', [
        animate('0.3s ease-out')
      ])
    ])
  ]
})
export class OppurtunitiesComponent implements OnInit {
  opportunities: Opportunity[] = [];
  showAddForm = false;
  showUpdateForm = false;
  successMessage: string = '';
  errorMessage: string = '';
  newOpportunity: Opportunity = {
    id: 0,
    name: '',
    company: '',
    description: '',
    link: '',
    imageUrl: '',
    createdAt: new Date().toISOString(),
    deadline: new Date().toISOString()
  };
  
  selectedOpportunity: Opportunity | null = null;
  confirmDeleteId: number | null = null;

  constructor(
    public authService: AuthServiceService, 
    public opportunityService: OpportunityService,
    private router: Router
  ) {}
  
  ngOnInit(): void {
    // Check if user is logged in
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }
    
    this.loadOpportunities();
  }
  
  private handleError(error: any): void {
    console.error('Error:', error);
    
    if (error.message && error.message.includes('Unauthorized')) {
      this.errorMessage = 'Your session has expired. Please log in again.';
      setTimeout(() => {
        this.authService.logout();
        this.router.navigate(['/login']);
      }, 3000);
    } else {
      this.errorMessage = error.message || 'An error occurred. Please try again.';
      setTimeout(() => {
        this.errorMessage = '';
      }, 5000);
    }
  }
  
  loadOpportunities(): void {
    this.opportunityService.getOpportunities().subscribe({
      next: (opportunities: Opportunity[]) => {
        this.opportunities = opportunities;
      },
      error: (error) => {
        this.handleError(error);
      }
    });
  }
  
  toggleAddForm(): void {
    // If we're closing the form
    if (this.showAddForm) {
      this.showAddForm = false;
      this.showUpdateForm = false;
      this.resetOpportunityForm();
    } else {
      // We're opening the form for adding
      this.showAddForm = true;
      this.showUpdateForm = false;
      this.resetOpportunityForm();
    }
  }
  
  toggleUpdateForm(opportunity: Opportunity): void {
    this.selectedOpportunity = { ...opportunity };
    this.newOpportunity = { ...opportunity };
    this.showUpdateForm = true;
    this.showAddForm = true;
  }

  resetOpportunityForm(): void {
    this.newOpportunity = {
      id: 0,
      name: '',
      company: '',
      description: '',
      link: '',
      imageUrl: '',
      createdAt: new Date().toISOString(),
      deadline: new Date().toISOString()
    };
    this.selectedOpportunity = null;
  }
  
  addOpportunity(): void {
    // Check if user is logged in and has admin privileges
    if (!this.authService.isLoggedIn()) {
      this.errorMessage = 'You must be logged in to add opportunities.';
      setTimeout(() => {
        this.router.navigate(['/login']);
      }, 2000);
      return;
    }
    
    if (!this.authService.isAdmin()) {
      this.errorMessage = 'You do not have permission to add opportunities.';
      setTimeout(() => {
        this.errorMessage = '';
      }, 5000);
      return;
    }
    
    // Make sure id is 0 for new opportunities
    const opportunityToAdd = {
      ...this.newOpportunity,
      id: 0
    };
    
    console.log('Adding opportunity:', opportunityToAdd);
    
    this.opportunityService.addOpportunity(opportunityToAdd).subscribe({
      next: () => {
        console.log('Opportunity added successfully');
        // Refresh opportunity list
        this.loadOpportunities();
        
        // Close the form
        this.showAddForm = false;
        
        // Show success message
        this.successMessage = `Opportunity "${opportunityToAdd.name}" has been added successfully!`;
        
        // Clear success message after 5 seconds
        setTimeout(() => {
          this.successMessage = '';
        }, 5000);
      },
      error: (error) => {
        this.handleError(error);
      }
    });
  }

  updateOpportunity(): void {
    // Check if user is logged in and has admin privileges
    if (!this.authService.isLoggedIn()) {
      this.errorMessage = 'You must be logged in to update opportunities.';
      setTimeout(() => {
        this.router.navigate(['/login']);
      }, 2000);
      return;
    }
    
    if (!this.authService.isAdmin()) {
      this.errorMessage = 'You do not have permission to update opportunities.';
      setTimeout(() => {
        this.errorMessage = '';
      }, 5000);
      return;
    }
    
    if (!this.selectedOpportunity || !this.selectedOpportunity.id) return;
    
    // Ensure id is explicitly defined for the API call
    const updatedOpportunity = { 
      ...this.newOpportunity, 
      id: this.selectedOpportunity.id 
    };
    
    console.log('Updating opportunity:', updatedOpportunity);
    
    this.opportunityService.updateOpportunity(updatedOpportunity).subscribe({
      next: () => {
        console.log('Opportunity updated successfully');
        // Refresh opportunity list
        this.loadOpportunities();
        
        // Close the form
        this.showAddForm = false;
        this.showUpdateForm = false;
        
        // Show success message
        this.successMessage = `Opportunity "${updatedOpportunity.name}" has been updated successfully!`;
        
        // Clear success message after 5 seconds
        setTimeout(() => {
          this.successMessage = '';
        }, 5000);
      },
      error: (error) => {
        this.handleError(error);
      }
    });
  }

  confirmDelete(opportunityId: number): void {
    if (!opportunityId) return;
    this.confirmDeleteId = opportunityId;
  }

  cancelDelete(): void {
    this.confirmDeleteId = null;
  }

  deleteOpportunity(opportunityId: number): void {
    // Check if user is logged in and has admin privileges
    if (!this.authService.isLoggedIn()) {
      this.errorMessage = 'You must be logged in to delete opportunities.';
      setTimeout(() => {
        this.router.navigate(['/login']);
      }, 2000);
      return;
    }
    
    if (!this.authService.isAdmin()) {
      this.errorMessage = 'You do not have permission to delete opportunities.';
      setTimeout(() => {
        this.errorMessage = '';
      }, 5000);
      return;
    }
    
    if (!opportunityId) return;
    
    console.log('Deleting opportunity:', opportunityId);
    
    this.opportunityService.deleteOpportunity(opportunityId).subscribe({
      next: () => {
        console.log('Opportunity deleted successfully');
        // Refresh opportunity list
        this.loadOpportunities();
        
        // Clear confirm delete
        this.confirmDeleteId = null;
        
        // Show success message
        this.successMessage = 'Opportunity has been deleted successfully!';
        
        // Clear success message after 5 seconds
        setTimeout(() => {
          this.successMessage = '';
        }, 5000);
      },
      error: (error) => {
        this.handleError(error);
      }
    });
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toLocaleDateString();
  }

  isDeadlinePassed(deadline: string): boolean {
    const deadlineDate = new Date(deadline);
    const today = new Date();
    return deadlineDate < today;
  }
}
