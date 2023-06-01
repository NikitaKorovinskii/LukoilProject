import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {RouterModule, Routes} from "@angular/router";


import { AppComponent } from './app.component';
import { MnemoschemaComponent } from './components/mnemoschema/mnemoschema.component';
import { GraphComponent } from './graph/graph.component';



const appRoutes: Routes = [
  {path: '', component: MnemoschemaComponent},
  {path: 'graph', component: GraphComponent},
  ]

@NgModule({
  declarations: [

    AppComponent,
    MnemoschemaComponent,
    GraphComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes)

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
