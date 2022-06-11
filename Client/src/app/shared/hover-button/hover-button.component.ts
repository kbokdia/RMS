import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-hover-button',
  templateUrl: './hover-button.component.html',
  styleUrls: ['./hover-button.component.scss']
})
export class HoverButtonComponent implements OnInit {
  @Input() label: string = '';
  
  constructor() { }

  ngOnInit(): void {
  }

}
