import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  //---------------PROPERTIES------------------
  readonly rootURL = "http://localhost:39479";
  reqHeader = new HttpHeaders({'user-key':'UserKey1'});
  qns: any[];
  seconds: number;
  timer;
  qnProgress: number;
  correctAnswersCount: number = 0;

  //---------------HELPER METHODS--------------
  constructor(private http: HttpClient) { }
  displayTimeElapsed(){
    return Math.floor(this.seconds/3600) + ':' + Math.floor(this.seconds/60) + ':' + Math.floor(this.seconds%60);
  }

  getParticipantName(){
    var participant = JSON.parse(localStorage.getItem('participant'));
    return participant.Name;
  }

  //---------------HTTP METHODS----------------

  insertParticipant(name:string,email:string){
    var body = {Name:name,Email:email};
    return this.http.post(this.rootURL + '/api/Participants',body,{headers:this.reqHeader});
  }

  updateParticipant(){
    var body = JSON.parse(localStorage.getItem('participant'));
    body.score = this.correctAnswersCount;
    body.timeSpent = this.seconds;
    return this.http.put(this.rootURL + '/api/Participants',body,{headers:this.reqHeader});
  }

  getQuestions(){
    return this.http.get(this.rootURL + "/api/Questions",{headers:this.reqHeader});
  }

  getAnswers(){
    var body = this.qns.map(x=>x.id);
    return this.http.post(this.rootURL + "/api/Questions/Answers",body,{headers:this.reqHeader});
  }
  
}
