import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { requiredFileType } from './requiredFileType';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  submitted = false;
  fileForm:FormGroup;
  uploadedFile: File;

  constructor(private formBuilder: FormBuilder) {
    this.fileForm = this.formBuilder.group({
      caption:['', Validators.required],
      file: ['', [Validators.required, requiredFileType('png')]]
    });
  }

  getFile(event) {
    this.uploadedFile = <File>event.target.files[0];
  }

  onSubmit(){
    this.submitted = true;
    if(!this.fileForm.invalid){
      console.log(this.fileForm.value);
      let fileName = this.fileForm.controls.caption.value + '.' + this.uploadedFile.type.split('/')[1].toLowerCase();
      console.log(fileName);
      saveAs(this.uploadedFile, `../assets/MyImages/${fileName}`);
      //console.log(this.uploadedFile);
    }
  }
}
