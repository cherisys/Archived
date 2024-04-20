import { Component, OnInit } from '@angular/core';
import { QuizService } from '../shared/quiz.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {

  constructor(private quizService: QuizService, private router: Router) { }

  ngOnInit() {
    if(parseInt(localStorage.getItem('qnProgress'))==10)
    {
      this.quizService.seconds = parseInt(localStorage.getItem('seconds'));
      this.quizService.qnProgress = parseInt(localStorage.getItem('qnProgress'));
      this.quizService.qns = JSON.parse(localStorage.getItem('qns'));
      this.quizService.getAnswers().subscribe(
        (data:any)=>{
          this.quizService.correctAnswersCount = 0;
          this.quizService.qns.forEach((e,i) =>{
              if(e.answer == data[i]) this.quizService.correctAnswersCount++;
              e.correct = data[i];
          });
        }
      );
    }
    else
      this.router.navigate(['/quiz']);
  }

  OnSubmit(){
    this.quizService.updateParticipant().subscribe(
      (data:any) => {
          this.Restart();
      }
    );
  }

  Restart(){
    localStorage.setItem('seconds','0');
    localStorage.setItem('qnProgress','0');
    localStorage.setItem('qns','');
    this.router.navigate(['/quiz']);
  }
}
