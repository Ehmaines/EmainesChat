import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/Services/UserServices/user.service';

function passwordMatchValidator(group: AbstractControl): ValidationErrors | null {
    const password = group.get('password')?.value;
    const confirm = group.get('confirmPassword')?.value;
    return password === confirm ? null : { passwordMismatch: true };
}

@Component({
    selector: 'ehm-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
    errorMessage = '';
    successMessage = '';

    form = new FormGroup(
        {
            userName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
            name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(100), Validators.pattern(/^[\p{L} ]+$/u)]),
            email: new FormControl('', [Validators.required, Validators.email]),
            password: new FormControl('', [
                Validators.required,
                Validators.minLength(8),
                Validators.pattern(/^(?=.*[a-zA-Z])(?=.*[0-9]).+$/),
            ]),
            confirmPassword: new FormControl('', [Validators.required]),
        },
        { validators: passwordMatchValidator }
    );

    constructor(private userService: UserService, private router: Router) {}

    register(event: Event): void {
        event.preventDefault();
        if (this.form.invalid) {
            this.form.markAllAsTouched();
            return;
        }

        const { userName, name, email, password } = this.form.value;
        this.userService.register({ userName: userName!, name: name ?? undefined, email: email!, password: password! }).subscribe({
            next: () => {
                this.successMessage = 'Conta criada com sucesso! Redirecionando...';
                this.errorMessage = '';
                setTimeout(() => this.router.navigate(['/login']), 1500);
            },
            error: (err) => {
                this.errorMessage = err?.error?.message ?? 'Erro ao criar conta. Tente novamente.';
                this.successMessage = '';
            },
        });
    }

    get userName() { return this.form.get('userName')!; }
    get name() { return this.form.get('name')!; }
    get email() { return this.form.get('email')!; }
    get password() { return this.form.get('password')!; }
    get confirmPassword() { return this.form.get('confirmPassword')!; }
}
