import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  h1Style:boolean;
  users:object;

  constructor(private data: DataService) { }

  ngOnInit() {
  }

  firstClick(){
    this.h1Style = true;
    this.data.firstClick().subscribe(udata=> {
      this.users = udata['data'];
    
//  Returned Data Object
//       {data: Array(3) [ {…}, {…}, {…} ],
// ​       page: 1,
// ​       per_page: 3,
// ​       total: 12,
// ​       total_pages: 4}
      
    });
  }
}
