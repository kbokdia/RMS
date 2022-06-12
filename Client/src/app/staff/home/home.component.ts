import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  title: string = "STAFF PORTAL";
  constructor(private authSvc: AuthService) {

  }

  ngOnInit(): void {
  }

  onLogout() {
    this.authSvc.logout();
  }


}
