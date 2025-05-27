import {AfterViewInit, Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ToasterService, UsersService} from "../../../services";
import {Router} from "@angular/router";
import {MenuService} from "../../../services/menu.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements AfterViewInit, OnInit {
  loginForm: FormGroup;
  email: string = '';
  password: string = '';
  rememberMe: boolean = true;
  errorMessage: string = '';
  successMessage: string = '';
  user: any | null;

  constructor(private fb: FormBuilder, private _userService: UsersService, private router: Router,
              private toast: ToasterService, private renderer: Renderer2, private el: ElementRef,
              private menuService: MenuService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      rememberMe: [true],
    });
  }
ngOnInit() {
    this.moveSectionUp();
}

  login(): void {
    if(!this.loginForm.invalid) {
      const credentials = this.loginForm.value;
      this._userService.login(credentials).subscribe({
        next: (response) => {
          this._userService.saveToken(response.token);
          localStorage.setItem('token', response.token);
          this.user = this._userService.getDecodedToken();
          console.log(this.user);
          this.successMessage = 'User logged in successfully.';
          this.toast.showSuccessToast(this.successMessage, 'Successfully logged in!');
          this.loginForm.reset();
          this.menuService.refreshMenu();
          this.router.navigate(['/users']);
        },
        error: (error) => {
          this.errorMessage = 'Login failed. Please check your credentials.' + error.message;
          this.toast.showErrorToast(this.errorMessage, 'Error Occurred.');
        }
      });
    }
  }


  moveSectionUp() {
    const section = this.el.nativeElement.querySelector('#login-section');
    this.renderer.addClass(section, 'active');
  }

  resetSectionPosition() {
    const section = this.el.nativeElement.querySelector('#login-section');
    this.renderer.removeClass(section, 'active');
  }

  ngAfterViewInit(): void {
    const togglePassword = document.getElementById('togglePasswordLogin');
    if (togglePassword) {
      togglePassword.addEventListener('click', e => {
        const passwordInput = document.getElementById('passwordLogin') as HTMLInputElement;
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
  }
}
