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
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private _userService: UsersService, private fb: FormBuilder, private router: Router, private toast: ToasterService) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      address: ['', [Validators.required]],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]],
      zipCode: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', [Validators.required, Validators.minLength(5)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    })
  }

  onSubmit(): void {
    if(!this.registerForm.invalid) {
      const newUser: { password: any; confirmPassword: any; id: undefined, email: any, name: any, userName: any, dateOfBirth: any,
        address: any, city: any, state: any, zipCode: any} = {
        id: undefined,
        name: this.registerForm.value.name,
        userName: this.registerForm.value.userName,
        email: this.registerForm.value.email,
        dateOfBirth: this.registerForm.value.dateOfBirth,
        address: this.registerForm.value.address,
        city: this.registerForm.value.city,
        state: this.registerForm.value.state,
        zipCode: this.registerForm.value.zipCode,
        password: this.registerForm.value.password,
        confirmPassword: this.registerForm.value.confirmPassword
      };
      this._userService.register(newUser).subscribe({
        next: (response) => {
          console.log('User registered successfully', response);
          this.toast.showSuccessToast('Successfully registered, sending you to the login screen.', 'User registered successfully');
          this.registerForm.reset();
          this.router.navigate(['users/login-page']);
        },
        error: (error) => {
          this.errorMessage = error.message;
          this.toast.showErrorToast(`${this.errorMessage}`, 'Error Occurred');
        }
      });
    }
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
