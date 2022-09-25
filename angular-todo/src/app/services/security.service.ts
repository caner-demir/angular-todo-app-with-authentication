import { Injectable } from '@angular/core';
import { AuthenticationResponse } from '../interfaces/authentication-response';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  private readonly tokenKey: string = 'token';
  private readonly expirationTokenKey: string = 'token-expiration';

  constructor() { }

  isAuthenticated(): boolean{
    const token = localStorage.getItem(this.tokenKey);

    if (!token){
      return false;
    }

    const expiration = localStorage.getItem(this.expirationTokenKey) || '';
    const expirationDate = new Date(expiration);

    if (expirationDate <= new Date()){
      this.logout();
      return false;
    }

    return true;
  }

  logout(): void{
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.expirationTokenKey);
  }

  saveToken(authenticationResponse: AuthenticationResponse): void{
    localStorage.setItem(this.tokenKey, authenticationResponse.token);
    localStorage.setItem(this.expirationTokenKey, authenticationResponse.expiration.toString());
  }

  getToken(){
    return localStorage.getItem(this.tokenKey);
  }
}
