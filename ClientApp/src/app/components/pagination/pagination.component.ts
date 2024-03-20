import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent {
  // @Input() totalItems = 0;
  // @Input() pageSize = 10;
  // @Input() currentPage = 1;


  @Output() pageChanged = new EventEmitter<number>();

  onPageChange(newPage: number): void {
    this.pageChanged.emit(newPage);
    console.log("newPage: ", newPage);
  }
}

