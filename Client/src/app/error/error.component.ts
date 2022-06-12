import { Component, Inject, NgZone, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss']
})
export class ErrorComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: { message: string }, public dialogRef: MatDialogRef<any>, private ngZone: NgZone) { }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.ngZone.run(() => {
      this.dialogRef.close();
    });
  }

}
