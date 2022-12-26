import { Component, OnInit,Injectable } from '@angular/core';
import { SharedService } from '../shared.service';
import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { Register } from './register';
import {Router} from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  title='AddUSER';
  user = new Register();
  register = new Register();

    constructor(private sharedService: SharedService, private router: Router, private _snackBar: MatSnackBar) { }

    ngOnInit(): void {
      var myDate = new Date();
      var varID = myDate.getHours() + "" + myDate.getMinutes() + "" + myDate.getSeconds() + "" + myDate.getMilliseconds();
                if (varID.length > 15) {
                    varID = varID.substr(0, 15);
                }
      this.user.SyncID= varID;

    }

    adduser(){
      this.sharedService.addUser(this.user).subscribe(data=>{
        console.log(data)
        this.register = data;
      })
	this.router.navigateByUrl('');
  this._snackBar.open('Submission Successful','Close',{horizontalPosition:'center', verticalPosition:'top'});
    }
    dobControl = new FormControl('', [Validators.required, this.maxDateValidator]);
    dobControl1 = new FormControl('', [Validators.required, this.maxDateValidator]);

  maxDateValidator(control: FormControl) {
    const currentDate = new Date();
    const controlDate = new Date(control.value);
    const maxDate = new Date(currentDate.setFullYear(currentDate.getFullYear() - 25));
    return controlDate <= maxDate ? null : { max: true };
  }
}



