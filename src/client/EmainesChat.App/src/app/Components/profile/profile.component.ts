import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserProfile, UpdateProfileRequest } from 'src/app/Interfaces/Users/user-profile';
import { UserService } from 'src/app/Services/UserServices/user.service';
import { AuthService } from 'src/app/core/authentication/auth.service';

@Component({
    selector: 'ehm-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
    profile: UserProfile | null = null;
    successMessage = '';
    errorMessage = '';
    pictureError = '';

    form = new FormGroup({
        name: new FormControl(''),
        email: new FormControl(''),
        currentPassword: new FormControl(''),
        newPassword: new FormControl(''),
    });

    constructor(
        private userService: UserService,
        private authService: AuthService,
        private router: Router,
        private location: Location
    ) { }

    ngOnInit(): void {
        this.userService.getProfile().subscribe({
            next: (profile) => {
                this.profile = profile;
                this.form.patchValue({
                    name: profile.name ?? '',
                    email: profile.email,
                });
                this.form.markAsPristine();
            },
            error: () => {
                this.errorMessage = 'Não foi possível carregar o perfil.';
            },
        });
    }

    save(): void {
        if (!this.form.dirty) return;

        const value = this.form.value;
        const request: UpdateProfileRequest = {};

        if (value.name !== (this.profile?.name ?? '')) {
            request.name = value.name ?? undefined;
        }
        if (value.email !== this.profile?.email) {
            request.email = value.email ?? undefined;
        }
        if (value.newPassword) {
            request.currentPassword = value.currentPassword ?? undefined;
            request.newPassword = value.newPassword;
        }

        this.userService.updateProfile(request).subscribe({
            next: (updated) => {
                this.profile = updated;
                this.successMessage = 'Perfil atualizado com sucesso.';
                this.errorMessage = '';
                this.form.patchValue({ currentPassword: '', newPassword: '' });
                this.form.markAsPristine();
            },
            error: (err) => {
                this.errorMessage = err?.error?.message ?? 'Erro ao atualizar perfil.';
                this.successMessage = '';
            },
        });
    }

    onFileSelected(event: Event): void {
        const file = (event.target as HTMLInputElement).files?.[0];
        if (!file) return;
        const formData = new FormData();
        formData.append('file', file);
        this.userService.uploadPicture(formData).subscribe({
            next: (res) => {
                if (this.profile) this.profile.profilePictureUrl = res.profilePictureUrl;
                this.pictureError = '';
            },
            error: (err) => {
                this.pictureError = err?.error?.message ?? 'Erro ao enviar foto.';
            },
        });
    }

    removePicture(): void {
        this.userService.removePicture().subscribe({
            next: () => {
                if (this.profile) this.profile.profilePictureUrl = null;
                this.pictureError = '';
            },
            error: (err) => {
                this.pictureError = err?.error?.message ?? 'Erro ao remover foto.';
            },
        });
    }

    goBack(): void {
        this.location.back();
    }
}
