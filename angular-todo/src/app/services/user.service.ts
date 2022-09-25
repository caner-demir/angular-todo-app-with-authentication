import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationResponse } from '../interfaces/authentication-response';
import { UserCredentials } from '../interfaces/user-credentials';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl: string = environment.apiUrl + '/users';

  constructor(private http: HttpClient) { }

  register(userRegister: UserCredentials): Observable<AuthenticationResponse>{
    return this.http.post<AuthenticationResponse>(this.apiUrl + "/register", userRegister);
  }

  login(userLogin: UserCredentials): Observable<AuthenticationResponse>{
    return this.http.post<AuthenticationResponse>(this.apiUrl + "/login", userLogin);
  }
}
