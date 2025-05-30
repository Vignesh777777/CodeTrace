import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProfileService } from '../_services/profile.service';
import { HttpClient } from '@angular/common/http';

interface PlatformData {
  id: string;
  name: string;
  logo: string;
  username: string;
  stats: any; // Or a more specific interface for stats later
  isLoading: boolean;
  error: string | null;
  showStatsDetails: boolean;
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class DashboardComponent implements OnInit {

  platforms = [
    { id: 'leetcode', name: 'LeetCode', logo: 'assets/images/leetcode-logo.png' },
    { id: 'geeksforgeeks', name: 'GeeksforGeeks', logo: 'assets/images/gfg-logo.png' },
    { id: 'github', name: 'GitHub', logo: 'assets/images/github-logo.png' },
    { id: 'codeforces', name: 'CodeForces', logo: 'assets/images/codeforces-logo.png' },
    { id: 'hackerrank', name: 'HackerRank', logo: 'assets/images/hackerrank-logo.png' },
    { id: 'codechef', name: 'CodeChef', logo: 'assets/images/codechef-logo.png' }
  ];

  platformData: { [key: string]: PlatformData } = {};

  constructor(
    private profileService: ProfileService,
    private http: HttpClient // Assuming HttpClient is needed for profileService or stats fetching later
  ) { }

  ngOnInit(): void {
    this.initializePlatformData();
    this.fetchProfileData(); // Fetch profile data on component initialization
  }

  initializePlatformData() {
    this.platforms.forEach(platform => {
      this.platformData[platform.id] = {
        ...platform, // Copy platform properties (id, name, logo)
        username: '', // Initialize username as empty
        stats: null,
        isLoading: false,
        error: null,
        showStatsDetails: false
      };
      // Optionally, read from local storage for initial display before fetching from backend
      const usernameFromLocalStorage = localStorage.getItem(`${platform.id}Username`);
      if (usernameFromLocalStorage) {
          this.platformData[platform.id].username = usernameFromLocalStorage;
      }
    });
  }

  fetchProfileData() {
    this.profileService.getProfile().subscribe({
      next: (response: any) => {
        console.log('Profile data fetched:', response);
        if (response) { // Assuming profile data is directly in the response
          this.platforms.forEach(platform => {
            const usernamePropertyName = `${platform.id}Username`;
            const username = response[usernamePropertyName] || '';
            // Update username in platformData if fetched data is not empty
            if (username) {
                this.platformData[platform.id].username = username;
                // Also update local storage here to keep it consistent with backend
                localStorage.setItem(`${platform.id}Username`, username);
            }
          });
        }
      },
      error: (error) => {
        console.error('Error fetching profile data:', error);
        // Handle error - maybe set error state in platformData
      }
    });
  }

  // Method to toggle stats display (will be called from HTML)
  toggleStatsDisplay(platformId: string) {
    const platform = this.platformData[platformId];
    if (platform) {
      platform.showStatsDetails = !platform.showStatsDetails;
      // In a real app, you'd fetch stats here if not already loaded
      if (platform.showStatsDetails && !platform.stats && !platform.isLoading) {
          // Example: Call a method to fetch stats for this platform
          // this.fetchStats(platformId);
      }
    }
  }

  // Placeholder for fetching stats (will implement later)
  fetchStats(platformId: string) {
      // Implementation for fetching stats from backend
      console.log(`Fetching stats for ${platformId}...`);
      const platform = this.platformData[platformId];
       if (platform && platform.username && !platform.isLoading) {
            platform.isLoading = true;
            platform.error = null;
           // Example API call - replace with actual endpoint
           // this.http.get(`/api/CodingStats/${platform.id}/${platform.username}`).subscribe({
           //     next: (statsResponse) => {
           //         platform.stats = statsResponse; // Store fetched stats
           //         platform.isLoading = false;
           //     },
           //     error: (statsError) => {
           //         platform.error = 'Failed to fetch stats';
           //         platform.isLoading = false;
           //         console.error(`Error fetching stats for ${platform.id}:`, statsError);
           //     }
           // });
           // Simulate a delay and set dummy stats for now
           setTimeout(() => {
               platform.stats = { score: 100, problemsSolved: 50, ranking: 100000, lastUpdated: new Date() };
               platform.isLoading = false;
               console.log(`Dummy stats loaded for ${platform.id}`);
           }, 1000);
       } else if (platform && !platform.username) {
           platform.error = 'Please set your username first.';
           console.log(`Cannot fetch stats for ${platformId}: username not set.`);
       }
  }

  // Method to get the button text dynamically
  getToggleButtonText(platformId: string): string {
      const platform = this.platformData[platformId];
      if (platform) {
          return platform.showStatsDetails ? 'Hide Stats' : 'Show Stats';
      }
      return 'Show Stats';
  }

  // Placeholder for updating username
  updateUsername(platformId: string) {
      const platform = this.platformData[platformId];
      if (platform && platform.username) {
           console.log(`Updating username for ${platformId} to: ${platform.username}`);
           // Example API call - replace with actual endpoint and data structure
           // const userEmail = localStorage.getItem('userEmail');
           // const updateData = { Email: userEmail, Profile: { [`${platform.id}Username`]: platform.username } };
           // this.profileService.updateProfile(updateData).subscribe({
           //     next: () => {
           //         console.log(`Username updated successfully for ${platform.id}`);
           //         // Update local storage after successful backend update
           //         localStorage.setItem(`${platform.id}Username`, platform.username);
           //     },
           //     error: (error) => {
           //         console.error(`Error updating username for ${platform.id}:`, error);
           //         // Handle error
           //     }
           // });
           // Simulate a successful update and log
            localStorage.setItem(`${platform.id}Username`, platform.username);
           console.log(`Dummy update successful for ${platformId}. Local storage updated.`);
      } else if (platform && !platform.username) {
          console.log(`Cannot update username for ${platformId}: username is empty.`);
      }
  }

}
