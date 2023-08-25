import { Component, OnInit, Renderer2, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { Token } from '../shared/models/token';

@Component({
  selector: 'app-authorization',
  templateUrl: './authorization.component.html',
  styleUrls: ['./authorization.component.scss']
})
export class AuthorizationComponent implements OnInit {
  myForm!: FormGroup;

  usernameForm = new FormControl("");
  passwordForm = new FormControl("");

  constructor(private as: AuthService, private router: Router, private formBuilder: FormBuilder) 
  { 
  }

  ngOnInit(): void {
    if (this.isLoggedIn)
    {
      this.as.logout(false);
    }

    this.myForm = this.formBuilder.group({
      email: [''],
      password: ['']
    });
  }

  public errorMessage: string = '';

  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated()
  }

  register() {
    let username = this.usernameForm.value;
    let password = this.passwordForm.value;

    this.as.register(username, password).subscribe((res: Token) => {
        this.router.navigate(['']);
      }, error => {
        this.errorMessage = 'Some of the fields have incorrect form';
      })
  }

  login() {
    let username = this.usernameForm.value;
    let password = this.passwordForm.value;

    this.as.login(username, password).subscribe(res => {
      this.router.navigate(['']);
    }, error => {
      this.errorMessage = 'Username or password is incorrect';
    })
  }
}