import { Routes, RouterModule } from "@angular/router";
import { TemplateDrivenFormsComponent } from "./template-driven-forms/template-driven-forms.component";
import { ReactiveFormsComponent } from "./reactive-forms/reactive-forms.component";


const appRoutes: Routes = [
    {path:'TemplateDrivenForm', component:TemplateDrivenFormsComponent},
    {path:'ReactiveForm', component:ReactiveFormsComponent},
    {path:'**', redirectTo:'/TemplateDrivenForm', pathMatch:'full'}
];

export const Routing = RouterModule.forRoot(appRoutes);