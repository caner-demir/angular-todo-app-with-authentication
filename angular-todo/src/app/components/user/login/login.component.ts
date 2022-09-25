import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SecurityService } from 'src/app/services/security.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isCorrectCredentials: boolean = true;

  constructor(private formBuilder: FormBuilder, private userService: UserService,
    private securityService: SecurityService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      userName: ['', {
        validators: [Validators.required]
      }],
      password: ['', {
        validators: [Validators.required, Validators.minLength(3)]
      }]
    })
  }

  login(): void {
    this.userService.login(this.loginForm.value).subscribe(authenticationResponse => {
      this.securityService.saveToken(authenticationResponse);
      this.router.navigate(['/']);
    }, (error: Response) => {
      if(error.status === 401){
        this.isCorrectCredentials = !this.isCorrectCredentials;
      }
    });
  }

}
