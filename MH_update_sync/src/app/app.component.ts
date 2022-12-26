import { Component, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Register, UserColumns } from './register';
import { SharedserviceService } from './sharedservice.service';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  private subs = new Subscription();
  displayedColumns: string[] = UserColumns.map((col) => col.key);
  columnsSchema: any = UserColumns;
  dataSource = new MatTableDataSource<Register>();
  valid: any = {};
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static:true }) sort: MatSort;

  constructor(public dialog: MatDialog, private userService:SharedserviceService, private _snackBar: MatSnackBar) {}

  ngOnInit() {
    this.subs.add(this.userService.getusers().subscribe((res: any) => {
      console.log(res);
      this.dataSource.data = res;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }));
  }

  editRow(row: Register) {
      console.log(row);
      this.userService.updateParticipation(row).subscribe(() => (row.isEdit = false));
      this._snackBar.open("Update Successful", "Close",{horizontalPosition:'center',verticalPosition:'top'});
  }

  inputHandler(e: any, id: number, key: string) {
    if (!this.valid[id]) {
      this.valid[id] = {};
    }
    this.valid[id][key] = e.target.validity.valid;
  }

  disableSubmit(id: number) {
    if (this.valid[id]) {
      return Object.values(this.valid[id]).some((item) => item === false);
    }
    return false;
  }
}
