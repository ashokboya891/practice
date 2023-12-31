import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';

import { ReactiveFormsModule,FormsModule } from "@angular/forms";
import { RegisterComponent } from './register/register.component';
import { ListsComponent } from './lists/lists.component';
import { SharedModule } from "../app/_modules/shared.module";
import { MessagesComponent } from './messages/messages.component';
// import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberCardsComponent } from './members/member-cards/member-cards.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DatePickerComponent } from './_forms/date-picker/date-picker.component';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { PhotoManagementComponent } from './admin/photo-management/photo-management.component';
import { ConfirmDailogComponent } from './modals/confirm-dailog/confirm-dailog.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { RouteReuseStrategy } from '@angular/router';
import { CustomRouteReuseStartegy } from './_services/CustomRouteReuseStartegy';
@NgModule({
  declarations: [AppComponent, NavComponent, HomeComponent, RegisterComponent, ListsComponent, MessagesComponent, MemberListComponent, TestErrorComponent, NotFoundComponent, ServerErrorComponent, MemberCardsComponent, MemberEditComponent, PhotoEditorComponent, TextInputComponent, DatePickerComponent, AdminPanelComponent, HasRoleDirective, UserManagementComponent, PhotoManagementComponent, ConfirmDailogComponent, RolesModalComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule
  
  ],
  providers: [
    {
      provide:HTTP_INTERCEPTORS,useClass:ErrorInterceptor,multi:true
    },
    {
      provide:HTTP_INTERCEPTORS,useClass:JwtInterceptor,multi:true
    },
    {
      provide:HTTP_INTERCEPTORS,useClass:LoadingInterceptor,multi:true
    },
    {
      provide:RouteReuseStrategy,useClass:CustomRouteReuseStartegy
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

// import { NgModule } from '@angular/core';
// import { BrowserModule } from '@angular/platform-browser';
// import { HttpClientModule } from "@angular/common/http";
// import { AppRoutingModule } from './app-routing.module';
// import { AppComponent } from './app.component';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
// @NgModule({
//   declarations: [
//     AppComponent
//   ],
//   imports: [
//     BrowserModule,
//     AppRoutingModule,
//     HttpClientModule,
//     BrowserAnimationsModule,
//     BsDatepickerModule
//   ],
//   providers: [],
//   bootstrap: [AppComponent]
// })
// export class AppModule { }
