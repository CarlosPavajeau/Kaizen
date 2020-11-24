import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '@modules/users/services/user.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: [ './confirm-email.component.scss' ]
})
export class ConfirmEmailComponent implements OnInit {
  emailConfirmed: boolean;
  confirmingEmail = true;
  constructor(private userService: UserService, private activatedRoute: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.activatedRoute.queryParamMap.subscribe((queryParams) => {
      const token = queryParams.get('token');
      const email = queryParams.get('email');

      if (token && email) {
        this.userService.confirmEmail(token, email).subscribe((user) => {
          this.confirmingEmail = false;
          if (user) {
            this.emailConfirmed = true;
            setTimeout(() => {
              this.router.navigateByUrl('/user/profile');
            }, 4000);
          } else {
            this.emailConfirmed = false;
          }
        });
      }
    });
  }
}
