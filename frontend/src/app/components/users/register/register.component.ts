import {AfterViewInit, Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ToasterService, UsersService} from "../../../services";
import {Router} from "@angular/router";



@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements AfterViewInit {
  registerForm: FormGroup;
  submitted = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private userService: UsersService,
    private router: Router,
    private toast: ToasterService
  ) {
    this.registerForm = this.fb.nonNullable.group({
      name: ['', Validators.required],
      userName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', [Validators.required]],
      address: ['', [Validators.required]],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]],
      zipCode: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      this.toast.showErrorToast('The form is invalid', 'The content in this form is invalid and should be changed.');
      return;
    }
    const formData = this.registerForm.value;
    if(formData.password !== formData.confirmPassword) {
      this.toast.showErrorToast('Passwords do not match', 'The passwords do not match, make them match and then submit again.');
      return;
    }
    const userData = {
      name: formData.name,
      userName: formData.userName,
      email: formData.email,
      dateOfBirth: formData.dateOfBirth,
      address: formData.address,
      city: formData.city,
      state: formData.state,
      zipCode: formData.zipCode,
      password: formData.password,
      confirmPassword: formData.confirmPassword,
    };

    this.userService.register(userData).subscribe({
      next: () => {
        this.toast.showSuccessToast('User registered successfully', 'Successfully registered, sending you to the login screen.');
        this.registerForm.reset();
        this.router.navigate(['users/login-page']).then(r => this.router.navigate(['users/login-page']));
      },
      error: (err) => {
        this.errorMessage = err.message;
        this.toast.showErrorToast('Error Occurred', `${this.errorMessage}`);
      }
    });
  }

  ngAfterViewInit(): void {
    const togglePassword = document.getElementById('togglePassword');
    const toggleConfirmPassword = document.getElementById('toggleConfirmPassword');
    if (togglePassword) {
      togglePassword.addEventListener('click', e => {
        const passwordInput = document.getElementById('password') as HTMLInputElement;
        if (!passwordInput) return;
        const type = passwordInput.type === 'password' ? 'text' : 'password';
        passwordInput.type = type;
        if (type === 'password') {
          togglePassword.classList.remove("fa-eye");
          togglePassword.classList.remove("text-success");
          togglePassword.classList.add("fa-eye-slash");
          togglePassword.classList.add("text-danger");
        } else {
          togglePassword.classList.remove("fa-eye-slash");
          togglePassword.classList.remove("text-danger");
          togglePassword.classList.add("fa-eye");
          togglePassword.classList.add("text-success");
        }
      })
    }
    if (toggleConfirmPassword) {
      toggleConfirmPassword.addEventListener('click', e => {
        const passwordInputConfirm = document.getElementById('confirmPassword') as HTMLInputElement;
        if (!passwordInputConfirm) return;
        const type = passwordInputConfirm.type === 'password' ? 'text' : 'password';
        passwordInputConfirm.type = type;
        if (type === 'password') {
          toggleConfirmPassword.classList.remove("fa-eye");
          toggleConfirmPassword.classList.remove("text-success");
          toggleConfirmPassword.classList.add("fa-eye-slash");
          toggleConfirmPassword.classList.add("text-danger");
        } else {
          toggleConfirmPassword.classList.remove("fa-eye-slash");
          toggleConfirmPassword.classList.remove("text-danger");
          toggleConfirmPassword.classList.add("fa-eye");
          toggleConfirmPassword.classList.add("text-success");
        }
      })
    }
  }

}
