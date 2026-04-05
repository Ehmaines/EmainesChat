import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserProfile, UpdateProfileRequest } from 'src/app/Interfaces/Users/user-profile';

@Injectable({ providedIn: 'root' })
export class UserService {
    private readonly apiUrl = `${environment.apiUrl}/users`;

    constructor(private http: HttpClient) {}

    getProfile(): Observable<UserProfile> {
        return this.http.get<UserProfile>(`${this.apiUrl}/me`);
    }

    updateProfile(data: UpdateProfileRequest): Observable<UserProfile> {
        return this.http.patch<UserProfile>(`${this.apiUrl}/me`, data);
    }

    uploadPicture(formData: FormData): Observable<{ profilePictureUrl: string }> {
        return this.http.post<{ profilePictureUrl: string }>(`${this.apiUrl}/me/picture`, formData);
    }

    removePicture(): Observable<{ profilePictureUrl: string | null }> {
        return this.http.delete<{ profilePictureUrl: string | null }>(`${this.apiUrl}/me/picture`);
    }

    register(data: { userName: string; name?: string; email: string; password: string }): Observable<void> {
        return this.http.post<void>(this.apiUrl, data);
    }
}
