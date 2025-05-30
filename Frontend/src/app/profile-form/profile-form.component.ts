import { Component, Output, EventEmitter } from '@angular/core';
import { ProfileService } from '../_services/profile.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-profile-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile-form.component.html',
  styleUrls: ['./profile-form.component.css']
})
export class ProfileFormComponent {
  @Output() completed = new EventEmitter<void>();
  errorMessage = '';
  profile = {
    name: '',
    graduationYear: '',
    rollNo: '',
    phoneNo: '',
    department: '',
    section: '',
    codechef: '',
    hackerrank: '',
    leetcode: '',
    github: '',
    codeforces: '',
    geeksforgeeks: '',
    skills: '',
    description: ''
  };

  step = 1;
  maxStep = 4;

  constructor(private profileService: ProfileService) {}

  isCurrentStepValid(): boolean {
    if (this.step === 1) {
      return !!(
        this.profile.name &&
        this.profile.graduationYear &&
        this.profile.rollNo &&
        this.profile.phoneNo &&
        this.profile.department &&
        this.profile.section
      );
    }
    if (this.step === 2) {
      return !!(
        this.profile.codechef &&
        this.profile.hackerrank &&
        this.profile.leetcode &&
        this.profile.github &&
        this.profile.codeforces &&
        this.profile.geeksforgeeks
      );
    }
    // Steps 3 and 4 can be empty for navigation
    return true;
  }

  nextStep() {
    if (this.step < this.maxStep && this.isCurrentStepValid()) {
      this.step++;
    }
  }

  prevStep() {
    if (this.step > 1) {
      this.step--;
    }
  }

  completeSetup() {
    if (this.step < this.maxStep) {
      this.nextStep();
    } else {
      const userEmail = localStorage.getItem('userEmail') || '';
      const userPayload = {
        Email: userEmail,
        Profile: {
          email: userEmail,
          name: this.profile.name,
          graduationYear: this.profile.graduationYear,
          department: this.profile.department,
          phoneNumber: this.profile.phoneNo,
          section: this.profile.section,
          rollNumber: this.profile.rollNo,
          leetcodeUsername: this.profile.leetcode,
          githubUsername: this.profile.github,
          codechefUsername: this.profile.codechef,
          hackerrankUsername: this.profile.hackerrank,
          codeforcesUsername: this.profile.codeforces,
          gfgUsername: this.profile.geeksforgeeks,
          skills: this.profile.skills ? this.profile.skills.split(',').map((s: string) => s.trim()).filter((s: string) => s) : [],
          interests: [],
          description: this.profile.description,
          User: { Email: userEmail }
        }
      };
      console.log('Sending payload:', JSON.stringify(userPayload, null, 2));
      
      // First update the profile
      this.profileService.updateProfile(userPayload).subscribe({
        next: () => {
          // After profile is updated, update coding stats
          const statsUpdates = [];
          
          if (this.profile.leetcode) {
            statsUpdates.push(this.profileService.updateLeetcodeStats(this.profile.leetcode));
          }
          if (this.profile.codeforces) {
            statsUpdates.push(this.profileService.updateCodeforcesStats(this.profile.codeforces));
          }
          if (this.profile.geeksforgeeks) {
            statsUpdates.push(this.profileService.updateGfgStats(this.profile.geeksforgeeks));
          }
          if (this.profile.hackerrank) {
            statsUpdates.push(this.profileService.updateHackerrankStats(this.profile.hackerrank));
          }

          // Wait for all stats updates to complete
          if (statsUpdates.length > 0) {
            forkJoin(statsUpdates).subscribe({
              next: () => {
                this.errorMessage = '';
                this.completed.emit();
              },
              error: (err) => {
                console.error('Stats update error:', err);
                // Even if stats update fails, we still want to complete the profile setup
                this.errorMessage = 'Profile updated but some coding stats could not be fetched. You can update them later.';
                this.completed.emit();
              }
            });
          } else {
            this.errorMessage = '';
            this.completed.emit();
          }
        },
        error: (err) => {
          console.error('Profile update error:', err);
          this.errorMessage = JSON.stringify(err?.error) || 'Profile update failed. Please check your details.';
        }
      });
    }
  }
}
