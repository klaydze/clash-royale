import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/auth.service';
import { finalize } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserRegistration } from 'src/app/models/user-registration';

@Component({
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  success: boolean;
  error: string;
  userRegistration: UserRegistration = { name: '', email: '', password: '', confirmpassword: '' };
  submitted: boolean = false;

  constructor(private authService: AuthService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
  }

  onSubmit() {

    this.spinner.show();

    this.authService.register(this.userRegistration)
      .pipe(finalize(() => {
        this.spinner.hide();
      }))
      .subscribe(
        result => {
          if (result) {
            this.success = true;
          }
        },
        error => {
          this.error = error;
        });
  }

}
