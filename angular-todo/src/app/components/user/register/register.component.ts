import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SecurityService } from 'src/app/services/security.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  isPasswordsMatch: boolean = true;

  constructor(private formBuilder: FormBuilder, private userService: UserService,
    private securityService: SecurityService, private router: Router) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      userName: ['', {
        validators: [Validators.required]
      }],
      password: ['', {
        validators: [Validators.required]
      }],
      confirmPassword: ['', {
        validators: [Validators.required]
      }]
    }, { validator: this.checkPasswords })
  }

  register(): void {
    if(this.registerForm.hasError('notSame')){
      this.isPasswordsMatch = false;
      return;
    }
    this.userService.register(this.registerForm.value).subscribe(authenticationResponse => {
      this.securityService.saveToken(authenticationResponse);
      this.router.navigate(['/']);
    });
  }

  checkPasswords(group: FormGroup) {
    let pass = group.controls.password.value;
    let confirmPass = group.controls.confirmPassword.value;

    return pass === confirmPass ? null : { notSame: true }
  }

}
