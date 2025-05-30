import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthServiceService } from '../_services/auth-service.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { CourseService } from '../_services/course.service';
import { Course } from '../_models/Course';
import { Router } from '@angular/router';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.css',
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
export class CoursesComponent implements OnInit {
  courses: Course[] = [];
  showAddForm = false;
  showUpdateForm = false;
  successMessage: string = '';
  errorMessage: string = '';
  newCourse: Course = {
    id: 0,
    name: '',
    level: '',
    description: '',
    link: '',
    imageUrl: '',
    duration: ''
  };
  
  selectedCourse: Course | null = null;
  confirmDeleteId: number | null = null;

  constructor(
    public authService: AuthServiceService, 
    public courseService: CourseService,
    private router: Router
  ) {}
  
  ngOnInit(): void {
    // Check if user is logged in
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }
    
    this.loadCourses();
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
  
  loadCourses(): void {
    this.courseService.getCourses().subscribe({
      next: (courses: Course[]) => {
        this.courses = courses;
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
      this.resetCourseForm();
    } else {
      // We're opening the form for adding
      this.showAddForm = true;
      this.showUpdateForm = false;
      this.resetCourseForm();
    }
  }
  
  toggleUpdateForm(course: Course): void {
    this.selectedCourse = { ...course };
    this.newCourse = { ...course };
    this.showUpdateForm = true;
    this.showAddForm = true;
  }

  resetCourseForm(): void {
    this.newCourse = {
      id: 0,
      name: '',
      level: '',
      description: '',
      link: '',
      imageUrl: '',
      duration: ''
    };
    this.selectedCourse = null;
  }
  
  addCourse(): void {
    // Check if user is logged in and has admin privileges
    if (!this.authService.isLoggedIn()) {
      this.errorMessage = 'You must be logged in to add courses.';
      setTimeout(() => {
        this.router.navigate(['/login']);
      }, 2000);
      return;
    }
    
    if (!this.authService.isAdmin()) {
      this.errorMessage = 'You do not have permission to add courses.';
      setTimeout(() => {
        this.errorMessage = '';
      }, 5000);
      return;
    }
    
    // Make sure id is 0 for new courses
    const courseToAdd = {
      ...this.newCourse,
      id: 0
    };
    
    console.log('Adding course:', courseToAdd);
    
    this.courseService.addCourse(courseToAdd).subscribe({
      next: () => {
        console.log('Course added successfully');
        // Refresh course list
        this.loadCourses();
        
        // Close the form
        this.showAddForm = false;
        
        // Show success message
        this.successMessage = `Course "${courseToAdd.name}" has been added successfully!`;
        
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

  updateCourse(): void {
    // Check if user is logged in and has admin privileges
    if (!this.authService.isLoggedIn()) {
      this.errorMessage = 'You must be logged in to update courses.';
      setTimeout(() => {
        this.router.navigate(['/login']);
      }, 2000);
      return;
    }
    
    if (!this.authService.isAdmin()) {
      this.errorMessage = 'You do not have permission to update courses.';
      setTimeout(() => {
        this.errorMessage = '';
      }, 5000);
      return;
    }
    
    if (!this.selectedCourse || !this.selectedCourse.id) return;
    
    // Ensure id is explicitly defined for the API call
    const updatedCourse = { 
      ...this.newCourse, 
      id: this.selectedCourse.id 
    };
    
    console.log('Updating course:', updatedCourse);
    
    this.courseService.updateCourse(updatedCourse).subscribe({
      next: () => {
        console.log('Course updated successfully');
        // Refresh course list
        this.loadCourses();
        
        // Close the form
        this.showAddForm = false;
        this.showUpdateForm = false;
        
        // Show success message
        this.successMessage = `Course "${updatedCourse.name}" has been updated successfully!`;
        
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

  confirmDelete(courseId: number): void {
    if (!courseId) return;
    this.confirmDeleteId = courseId;
  }

  cancelDelete(): void {
    this.confirmDeleteId = null;
  }

  deleteCourse(courseId: number): void {
    // Check if user is logged in and has admin privileges
    if (!this.authService.isLoggedIn()) {
      this.errorMessage = 'You must be logged in to delete courses.';
      setTimeout(() => {
        this.router.navigate(['/login']);
      }, 2000);
      return;
    }
    
    if (!this.authService.isAdmin()) {
      this.errorMessage = 'You do not have permission to delete courses.';
      setTimeout(() => {
        this.errorMessage = '';
      }, 5000);
      return;
    }
    
    if (!courseId) return;
    
    console.log('Deleting course:', courseId);
    
    this.courseService.deleteCourse(courseId).subscribe({
      next: () => {
        console.log('Course deleted successfully');
        // Refresh course list
        this.loadCourses();
        
        // Clear confirm delete
        this.confirmDeleteId = null;
        
        // Show success message
        this.successMessage = 'Course has been deleted successfully!';
        
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
}
