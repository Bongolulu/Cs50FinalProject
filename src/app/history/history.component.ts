import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [],
  templateUrl: './history.component.html',
  styleUrl: './history.component.scss'
})
export class HistoryComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  public history = [] 

  ngOnInit(): void {
    this.apiService.History().subscribe({
      next: (antwort) => {
        console.log(antwort)
        this.history=antwort
      },
      error: (fehler) => {
        console.log(fehler);
      }
    })
  }

}
